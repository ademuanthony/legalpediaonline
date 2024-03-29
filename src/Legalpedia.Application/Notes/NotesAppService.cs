using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using Legalpedia.Authorization.Users;
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
        private readonly IRepository<NoteRating, string> _noteRatingRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;
        
        public NotesAppService(IRepository<Note, string> noteRepository, 
            IWebHostEnvironment hostingEnvironment, 
            IRepository<FavouriteNote, string> favouriteNoteRepository, 
            IRepository<NoteComment, string> commentRepository, 
            IRepository<SharedNote, string> sharedNoteRepository, 
            IRepository<NoteRating, string> noteRatingRepository, 
            IRepository<User, long> userRepository)
        {
            _noteRepository = noteRepository;
            _hostingEnvironment = hostingEnvironment;
            _favouriteNoteRepository = favouriteNoteRepository;
            _commentRepository = commentRepository;
            _sharedNoteRepository = sharedNoteRepository;
            _noteRatingRepository = noteRatingRepository;
            _userRepository = userRepository;
        }

        public async Task<Note> CreateNote(CreateNoteDto input)
        {
            var note = ObjectMapper.Map<Note>(input);
            
            if (AbpSession.UserId != null) note.CreatorId = AbpSession.UserId.Value;
            var noteId = Guid.NewGuid();
            note.Id = noteId.ToString();
            note.CreatedAt = DateTime.Now;
            note.Image = input.ImageContent;
            await _noteRepository.InsertAsync(note);
            return note;
        }

        private NoteDto MarkFavourite(NoteDto note)
        {
            var fn = _favouriteNoteRepository.FirstOrDefault(f => f.NoteId == note.Id &&
                                                                  f.UserId == AbpSession.UserId.Value);
            note.Favourite = fn != null;
            return note;
        }

        private List<string> FavouriteNoteIds()
        {
            return _favouriteNoteRepository.GetAll().Where(f => f.UserId == AbpSession.UserId.Value)
                .Select(n => n.NoteId).ToList();
        }

        private List<NoteDto> MarkFavourites(List<NoteDto> notes)
        {
            var ids = FavouriteNoteIds();
            foreach (var note in notes)
            {
                note.Favourite = ids.Contains(note.Id);
            }

            return notes;
        }
        
        public async Task<NoteDto> GetNote(EntityDto<string> input)
        {
            var note = await _noteRepository.FirstOrDefaultAsync(n => n.Id == input.Id);
            return MarkFavourite(ObjectMapper.Map<NoteDto>(note));
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

            note.Image = input.ImageContent;
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

        public PagedResultDto<NoteDto> MyNotes(NotesRequest input)
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
            var fNotes = MarkFavourites(ObjectMapper.Map<List<NoteDto>>(notes));
            return new PagedResultDto<NoteDto>(totalCount, fNotes);
        }
        
        public PagedResultDto<NoteDto> PublicNotes(NotesRequest input)
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
            
            var fNotes = MarkFavourites(ObjectMapper.Map<List<NoteDto>>(notes));
            return new PagedResultDto<NoteDto>(totalCount, fNotes);
        }
         
        public PagedResultDto<NoteDto> TeamNotes(TeamNotesRequest input)
        {
            var sharedNotes = _sharedNoteRepository.GetAll().ToList();
            var sharedIds = sharedNotes.Where(s => s.TeamId == input.TeamId).Select(s => s.NoteId);

            var query = _noteRepository.GetAll()
                .Where(n => n.TeamId == input.TeamId || sharedIds.Contains(n.Id));
            if (!input.SearchTerm.IsNullOrEmpty())
            {
                query = query.Where(n => n.Title.ToLower().Contains(input.SearchTerm.ToLower()));
            }
            var totalCount = query.Count();
            var notes = query.OrderByDescending(n => n.CreatedAt)
                .Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            
            var fNotes = MarkFavourites(ObjectMapper.Map<List<NoteDto>>(notes));
            return new PagedResultDto<NoteDto>(totalCount, fNotes);
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
        
        public PagedResultDto<NoteDto> FavouriteNotes(NotesRequest input)
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
            
            var fNotes = MarkFavourites(ObjectMapper.Map<List<NoteDto>>(notes));
            return new PagedResultDto<NoteDto>(totalCount, fNotes);
        }

        public async Task<NoteCommentDto> AddComment(AddCommentInput input)
        {
            var comment = new NoteComment
            {
                NoteId = input.NoteId,
                Body = input.Body
            };
            comment.CreatorId = AbpSession.UserId.Value;
            comment.CreatedAt = DateTime.Now;
            comment.Id = Guid.NewGuid().ToString();
            await _commentRepository.InsertAsync(comment);
            var dto = ObjectMapper.Map<NoteCommentDto>(comment);
            var user = await _userRepository.FirstOrDefaultAsync(u => u.Id == AbpSession.UserId.Value);
            dto.CreatorName = user.FullName;
            return dto;
        }

        public async Task<NoteCommentDto> UpdateComment(UpdateCommentInput input)
        {
            var oldComment = _commentRepository.FirstOrDefault(c => c.Id == input.Id);
            if (oldComment == null)
            {
                throw new UserFriendlyException("Comment not found");
            }

            if (oldComment.CreatorId != AbpSession.UserId.Value)
            {
                throw new UserFriendlyException("Access denied. You didn't add this comment");
            }

            oldComment.Body = input.Body;
            await _commentRepository.UpdateAsync(oldComment);
            var dto = ObjectMapper.Map<NoteCommentDto>(oldComment);
            var user = await _userRepository.FirstOrDefaultAsync(u => u.Id == AbpSession.UserId.Value);
            dto.CreatorName = user.FullName;
            return dto;
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

        public List<NoteCommentDto> GetComments(EntityDto<string> input)
        {
            var comments = from c in _commentRepository.GetAll()
                join u in _userRepository.GetAll() on c.CreatorId equals u.Id
                select new NoteCommentDto
                {
                    Id = c.Id,
                    Body = c.Body,
                    CreatedAt = c.CreatedAt,
                    CreatorId = c.CreatorId,
                    CreatorName = u.FullName,
                    NoteId = c.NoteId,
                    AllowEdit = c.CreatorId == u.Id
                };
            return comments.ToList();
        }

        public async Task<NoteRating> AddRating(NoteRating input)
        {
            var rating =
                await _noteRatingRepository.FirstOrDefaultAsync(
                    n => n.NoteId == input.NoteId && n.UserId == AbpSession.UserId.Value);
            if (rating != null)
            {
                rating.Rating = input.Rating;
                await _noteRatingRepository.UpdateAsync(rating);
                return rating;
            }
            input.Id = Guid.NewGuid().ToString();
            input.UserId = AbpSession.UserId.Value;
            await _noteRatingRepository.InsertAsync(input);
            return input;
        }

        public NoteRatingResult NoteRating(EntityDto<string> input)
        {
            var query = _noteRatingRepository.GetAll().Where(n => n.NoteId == input.Id);
            var count = query.Count();
            var total = query.Sum(n => n.Rating);
            return new NoteRatingResult(total, count);
        }

        public async Task<bool> Share(ShareInput input)
        {
            
            if(input.TeamIds.Count() == 0) {
                var oldRec = await _sharedNoteRepository
                .FirstOrDefaultAsync(n => n.UserId == AbpSession.UserId.Value && n.NoteId == input.NoteId);
                if (oldRec != null) return true;

                var sn = new SharedNote
                {
                    NoteId = input.NoteId,
                    UserId = AbpSession.UserId.Value,
                    CreatedAt = DateTime.Now,
                    Id = Guid.NewGuid().ToString()
                };
                await _sharedNoteRepository.InsertAsync(sn);
            } else {
                foreach(var id in input.TeamIds) {

                    var oldRec = await _sharedNoteRepository
                        .FirstOrDefaultAsync(n => n.TeamId == id && n.NoteId == input.NoteId);
                    if (oldRec != null) continue;

                    var sn = new SharedNote
                    {
                        NoteId = input.NoteId,
                        TeamId = id,
                        UserId = AbpSession.UserId.Value,
                        CreatedAt = DateTime.Now,
                        Id = Guid.NewGuid().ToString()
                    };
                    await _sharedNoteRepository.InsertAsync(sn);
                }
            }
            return true;
        }

        public List<Note> SharedNotesByUser(EntityDto<long> input)
        {
            var notes = (from n in _noteRepository.GetAll()
                join sn in _sharedNoteRepository.GetAll() on n.Id equals sn.NoteId
                orderby n.CreatedAt descending 
                where sn.UserId == input.Id
                select n);
            return notes.ToList();
        }
    }
}