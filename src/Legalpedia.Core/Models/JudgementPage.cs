using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models {
    public class JudgementPage : Entity<string> {
        public string SuitNumber { get; set; }
        public string Content { get; set; }
        public int Number { get; set; }
    }
}