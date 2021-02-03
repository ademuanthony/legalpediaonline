using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Legalpedia.Models;

//using Legalbackend.Legalpedia.Version2;

namespace Legalpedia.Laws.LawsOfFederations.Dtos
{
    [AutoMapFrom(typeof(LawOfFederation))]
    public class LawOfFederationDto:EntityDto
    {
        public int? CatId { get; set; } 

        public string LawNo { get; set; }

        public string Title { get; set; }

        public DateTime LawDate { get; set; }

        public string Descr { get; set; }

        public string SubsidiaryLegislation { get; set; }

        public string Tags { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
