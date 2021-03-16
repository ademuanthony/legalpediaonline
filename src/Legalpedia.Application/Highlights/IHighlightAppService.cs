using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Legalpedia.Models;

namespace Legalpedia.Highlights
{
    public interface IHighlightAppService: IApplicationService
    {
        Highlight Create(Highlight input);
        void Clear(string id);
        List<Highlight> GetAll(string caseId);
        List<Highlight> GetByCollectionId(string collectionId);
    }
}