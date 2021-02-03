using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.Courts.Dto
{
    [AutoMapTo(typeof(Court))]
    public class CreateCourtDto
    {
        public string Name { get; set; }
    }
}
