using System.Collections.Generic;
using Legalpedia.Models;

namespace Legalpedia.Annotations
{
    public interface IAnnotationAppService
    {
        Annotation Create(Annotation input);
        void Clear(string id);
        List<Annotation> GetAll(string caseId, ContentType contentType);
        List<Annotation> Search(string term);
        List<string> Tags();
    }
}