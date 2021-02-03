using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.Categories.Dto
{
    [AutoMapTo(typeof(Category))]
    public class CreateCategoryDto
    {
        public string Name { get; set; }
    }
}
