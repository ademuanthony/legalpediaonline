using System.Collections.Generic;
using Legalpedia.Models;

namespace Legalpedia.Annotations
{
    public interface IAnnotationAppService
    {
        Annotation Create(Annotation input);
        void Clear(string id);
        List<Annotation> GetAll(string caseId);
        List<Annotation> GetByCollectionId(string collectionId);
        List<Annotation> Search(string term);
    }
}