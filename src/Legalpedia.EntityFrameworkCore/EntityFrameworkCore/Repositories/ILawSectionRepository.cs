using Abp.Domain.Repositories;
using Legalpedia.Models;

namespace Legalpedia.EntityFrameworkCore.Repositories
{
    public interface ILawSectionRepository : IRepository<LawOfFedSection>
    {
    }
}