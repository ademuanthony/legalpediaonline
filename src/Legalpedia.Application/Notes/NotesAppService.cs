using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using Castle.MicroKernel.Registration;
using Legalpedia.Models;
using Legalpedia.Notes.Dto;
using Microsoft.AspNetCore.Hosting;

namespace Legalpedia.Notes
{
    [AbpAuthorize]
    public class NotesAppService: LegalpediaAppServiceBase
    {
        private readonly IRepository<Note, string> _noteRepository;
        private readonly IRepository<FavouriteNote, string> _favouriteNoteRepository;
        private readonly IRepository<NoteComment, string> _commentRepository;
        private readonly IRepository<SharedNote, string> _sharedNoteRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;
        
        public NotesAppService(IRepository<Note, string> noteRepository, 
            IWebHostEnvironment hostingEnvironment, 
            IRepository<FavouriteNote, string> favouriteNoteRepository, 
            IRepository<NoteComment, string> commentRepository, 
            IRepository<SharedNote, string> sharedNoteRepository)
        {
            _noteRepository = noteRepository;
            _hostingEnvironment = hostingEnvironment;
            _favouriteNoteRepository = favouriteNoteRepository;
            _commentRepository = commentRepository;
            _sharedNoteRepository = sharedNoteRepository;
        }

        public async Task<Note> CreateNote(CreateNoteDto input)
        {
            var note = ObjectMapper.Map<Note>(input);
            
            if (AbpSession.UserId != null) note.CreatorId = AbpSession.UserId.Value;
            var noteId = Guid.NewGuid();
            note.Id = noteId.ToString();
            note.CreatedAt = DateTime.Now;
            var uniqueFileName = $"{noteId}:N.png";
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, LegalpediaConsts.NotesImageFolder);
            if(!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }
            var filePath = Path.Combine(uploads, uniqueFileName);
            try
            {
                var bytes = Convert.FromBase64String(input.ImageContent);
                await using var ms = new MemoryStream(bytes);
                using var image = Image.FromStream(ms);
                image.Save(filePath, ImageFormat.Png);
            }
            catch(Exception ex)
            {
                throw new UserFriendlyException((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            note.Image = uniqueFileName;
            await _noteRepository.InsertAsync(note);
            return note;
        }

        public async Task<Note> UpdateNote(UpdateNoteDto input)
        {
            var note = await _noteRepository.FirstOrDefaultAsync(n => n.Id == input.Id);
            if (note == null)
            {
                throw new UserFriendlyException("Note not found");
            }

            if (note.CreatorId != AbpSession.UserId.Value)
            {
                throw new UserFriendlyException("Access denied; you cannot update a note that you didn't create");
            }

            if (!input.Title.IsNullOrEmpty())
            {
                note.Title = input.Title;
            }
            if (!input.Body.IsNullOrEmpty())
            {
                note.Body = input.Body;
            }
            if (!input.Labels.IsNullOrEmpty())
            {
                note.Labels = input.Labels;
            }
            
            if (!input.Summary.IsNullOrEmpty())
            {
                note.Summary = input.Summary;
            }

            if (!input.ImageContent.IsNullOrEmpty())
            {
                var uniqueFileName = !note.Image.IsNullOrEmpty() ? note.Image : $"{input.Id}.png";
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, LegalpediaConsts.NotesImageFolder);
                if(!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }
                var filePath = Path.Combine(uploads, uniqueFileName);
                try
                {
                    var bytes = Convert.FromBase64String(input.ImageContent);
                    await using var ms = new MemoryStream(bytes);
                    using var image = Image.FromStream(ms);
                    image.Save(filePath, ImageFormat.Png);
                }
                catch(Exception ex)
                {
                    throw new UserFriendlyException((int)HttpStatusCode.InternalServerError, ex.Message);
                }
                note.Image = uniqueFileName;
            }

            note.AccessType = input.AccessType;

            await _noteRepository.UpdateAsync(note);
            return note;
        }

        public async Task DeleteNote(EntityDto<string> input)
        {
            await _favouriteNoteRepository.DeleteAsync(fn=>fn.NoteId == input.Id);
            await _commentRepository.DeleteAsync(fn=>fn.NoteId == input.Id);
            await _sharedNoteRepository.DeleteAsync(fn=>fn.NoteId == input.Id);

            await _noteRepository.DeleteAsync(n => n.Id == input.Id);
        }

        public PagedResultDto<Note> MyNotes(NotesRequest input)
        {
            var query = _noteRepository.GetAll()
                .Where(n => n.CreatorId == AbpSession.UserId.Value);
            if (!input.SearchTerm.IsNullOrEmpty())
            {
                query = query.Where(n => n.Title.ToLower().Contains(input.SearchTerm.ToLower()));
            }
            var totalCount = query.Count();
            var notes = query.OrderBy(n => n.CreatedAt)
                .Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return new PagedResultDto<Note>(totalCount, notes);
        }
        
        public PagedResultDto<Note> PublicNotes(NotesRequest input)
        {
            var query = _noteRepository.GetAll()
                .Where(n => n.AccessType == NoteAccessType.Public);
            if (!input.SearchTerm.IsNullOrEmpty())
            {
                query = query.Where(n => n.Title.ToLower().Contains(input.SearchTerm.ToLower()));
            }
            var totalCount = query.Count();
            var notes = query.OrderBy(n => n.CreatedAt)
                .Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return new PagedResultDto<Note>(totalCount, notes);
        }
        
        public PagedResultDto<Note> TeamNotes(TeamNotesRequest input)
        {
            var query = _noteRepository.GetAll()
                .Where(n => n.TeamId == input.TeamId);
            if (!input.SearchTerm.IsNullOrEmpty())
            {
                query = query.Where(n => n.Title.ToLower().Contains(input.SearchTerm.ToLower()));
            }
            var totalCount = query.Count();
            var notes = query.OrderBy(n => n.CreatedAt)
                .Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return new PagedResultDto<Note>(totalCount, notes);
        }

        public async Task ToggleFavourite(EntityDto<string> input)
        {
            var oldRec =
                await _favouriteNoteRepository.FirstOrDefaultAsync(
                    f => f.NoteId == input.Id && f.UserId == AbpSession.UserId.Value);
            if (oldRec != null)
            {
                await _favouriteNoteRepository.DeleteAsync(oldRec);
            }
            else
            {
                await _favouriteNoteRepository.InsertAsync(new FavouriteNote
                {
                    UserId = AbpSession.UserId.Value,
                    NoteId = input.Id,
                    CreatedAt = DateTime.Now,
                    Id = Guid.NewGuid().ToString()
                });
            }
        }
        
        public PagedResultDto<Note> FavouriteNotes(NotesRequest input)
        {
            var query = from n in _noteRepository.GetAll()
                join fn in _favouriteNoteRepository.GetAll() on n.Id equals fn.NoteId
                select n;
            if (!input.SearchTerm.IsNullOrEmpty())
            {
                query = query.Where(n => n.Title.ToLower().Contains(input.SearchTerm.ToLower()));
            }
            var totalCount = query.Count();
            var notes = query.OrderBy(n => n.CreatedAt)
                .Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return new PagedResultDto<Note>(totalCount, notes);
        }

        public async Task<NoteComment> AddComment(NoteComment comment)
        {
            comment.CreatorId = AbpSession.UserId.Value;
            comment.CreatedAt = DateTime.Now;
            comment.Id = Guid.NewGuid().ToString();
            await _commentRepository.InsertAsync(comment);
            return comment;
        }

        public async Task<NoteComment> UpdateComment(NoteComment comment)
        {
            var oldComment = _commentRepository.FirstOrDefault(c => c.Id == comment.Id);
            if (oldComment == null)
            {
                throw new UserFriendlyException("Comment not found");
            }

            if (oldComment.CreatorId != AbpSession.UserId.Value)
            {
                throw new UserFriendlyException("Access denied. You didn't add this comment");
            }

            oldComment.Body = comment.Body;
            await _commentRepository.UpdateAsync(oldComment);
            return oldComment;
        }

        public async Task DeleteComment(EntityDto<string> input)
        {
            var oldComment = await _commentRepository.FirstOrDefaultAsync(c => c.Id == input.Id);
            if (oldComment == null)
            {
                throw new UserFriendlyException("Comment not found");
            }

            if (oldComment.CreatorId != AbpSession.UserId.Value)
            {
                throw new UserFriendlyException("Access denied. You didn't add this comment");
            }
            await _commentRepository.DeleteAsync(oldComment);
        }

        public List<NoteComment> GetComments(EntityDto<string> input)
        {
            return _commentRepository.GetAll().Where(c => c.NoteId == input.Id).ToList();
        }
    }
}