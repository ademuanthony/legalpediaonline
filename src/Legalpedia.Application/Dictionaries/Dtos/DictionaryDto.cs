﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.Dictionaries.Dtos
{
    public class DictionaryDto: EntityDto
    {
        public string Uuid { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int? VersionNo { get; set; }
    }
}