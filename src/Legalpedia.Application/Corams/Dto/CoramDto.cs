using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.Corams.Dto
{
    [AutoMapTo(typeof(Coram))]
    public class CoramDto:EntityDto
    {
        public string Name { get; set; }

        public string EmailAddr { get; set; }


        public string PhoneNo { get; set; }
    }
}
