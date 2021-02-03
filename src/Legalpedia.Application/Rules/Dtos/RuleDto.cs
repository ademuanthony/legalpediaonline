using Abp.AutoMapper;
using Legalpedia.Models;

namespace Legalpedia.Rules.Dtos
{
    [AutoMapFrom(typeof(Rule))]
    public class RuleDto
    {
        public string Uuid { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Section { get; set; }

        public string Type { get; set; }

        public string Content { get; set; }
    }
}
