using System;
using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.Laws.LawsOfFederations.Dtos
{
    [AutoMapTo(typeof(LawOfFederation))]
    public class CreateLawOfFederationDto
    {
        public int? CatId { get; set; }

        public string LawNo { get; set; }

        public string Title { get; set; }

        public DateTime LawDate { get; set; }

        public string Descr { get; set; }

        public string SubsidiaryLegislation { get; set; }

        public string Tags { get; set; }
    }
}
