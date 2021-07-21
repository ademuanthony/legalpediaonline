using System.Collections.Generic;
using Abp.Application.Services;
using Legalpedia.Models;

namespace Legalpedia.Highlights
{
    public interface IHighlightAppService: IApplicationService
    {
        Highlight Create(Highlight input);
        void Clear(string id);
        List<Highlight> GetAll(string caseId, ContentType contentType);
        List<Highlight> GetByCollectionId(string collectionId);
    }
}