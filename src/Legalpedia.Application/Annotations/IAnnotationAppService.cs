using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Legalpedia.Models;

namespace Legalpedia.Annotations
{
    public interface IAnnotationAppService
    {
        Annotation Create(Annotation input);
        void Clear(string id);
        List<Annotation> GetAll(string caseId, ContentType contentType);
        PagedResultDto<Annotation> Search(SearchAnnotationInput input);
        List<string> Tags();
    }
}