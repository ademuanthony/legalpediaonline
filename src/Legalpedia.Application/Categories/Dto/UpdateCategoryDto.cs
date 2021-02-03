using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.Categories.Dto
{
    [AutoMapTo(typeof(Category))]
    public class UpdateCategoryDto:EntityDto
    {
        public string Name { get; set; }
    }
}
