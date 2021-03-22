﻿using Abp.Application.Services.Dto;

namespace Legalpedia.ForeignLegalResources.Dtos
{
    public class UpdateForeignLegalResourceDto: EntityDto
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}