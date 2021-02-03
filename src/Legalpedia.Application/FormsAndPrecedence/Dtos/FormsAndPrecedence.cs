﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legalpedia.FormsAndPrecedence.Dtos
{
    public class FormsAndPrecedenceDto:EntityDto
    {
        public string Uuid { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
