using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.Categories.Dto
{
    [AutoMapFrom(typeof(Category))]
    public class CategoryDto:EntityDto
    {
        public string Name { get; set; }
    }
}
