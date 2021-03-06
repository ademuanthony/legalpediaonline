﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalpedia.DAL.Entities.Version2
{
    [Table("UpdateMetas")]
    public class UpdateMeta
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string DownloadLink { get; set; }
        public string Description { get; set; }
        public int Size { get; set; }
    }
}
