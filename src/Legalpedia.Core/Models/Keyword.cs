using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Legalpedia.Models
{
    [Table("keywords")]
    public class Keyword: Entity
    {
        public Keyword()
        {
            Version = DateTime.Now;
        }

        [Column("text")]
        public string Text { get; set; }

        [Column("ratio_count")]
        public int RatioCount { get; set; }

        [Column("summary_count")]
        public int SummaryCount { get; set; }

        [Column("result_count")]
        public int ResultCount { get; set; }

        [Column("last_indexing_date")]
        public DateTime LastIndexingDate { get; set; }

        [Column("version")]
        public DateTime Version { get; set; }

        /*
            Or SearchString = "any" Or SearchString = "are" Or SearchString = "as" Or SearchString = "at" Or SearchString = "be" Or SearchString = "" _
           Or SearchString = "but" Or SearchString = "by" Or SearchString = "can" Or SearchString = "did" Or SearchString = "do" Or SearchString = "for" _
           Or SearchString = "get" Or SearchString = "got" Or SearchString = "has" Or SearchString = "had" Or SearchString = "he" Or SearchString = "have" Or SearchString = "her" _
            Or SearchString = "him" Or SearchString = "his" Or SearchString = "how" Or SearchString = "if" Or SearchString = "in" Or SearchString = "is" Or 
            SearchString = "it" Or SearchString = "me" _
           Or SearchString = "my" Or SearchString = "now" Or SearchString = "of" Or SearchString = "on" Or SearchString = "or" Or SearchString = "other" Or SearchString = "our" Or
           SearchString = "out" _
           Or SearchString = "see" Or SearchString = "the" Or SearchString = "to" Or SearchString = "too" Or SearchString = "under" Or SearchString = "up" Or SearchString = "was" Or SearchString = "way" _
           Or SearchString = "we" Or SearchString = "who" Or SearchString = "you" Or SearchString = "your" Or SearchString = "a" Or SearchString = "b" Or SearchString = "c" _
           Or SearchString = "d" Or SearchString = "e" Or SearchString = "f" Or SearchString = "g" Or SearchString = "h" Or SearchString = "i" Or SearchString = "j" Or SearchString = "k" _
           Or SearchString = "l" Or SearchString = "m" Or SearchString = "n" Or SearchString = "o" Or SearchString = "p" Or SearchString = "q" Or SearchString = "r" Or SearchString = "s" Or SearchString = "t" _
           Or SearchString = "u" Or SearchString = "v" Or SearchString = "w" Or SearchString = "x" Or SearchString = "y" Or SearchString = "z" Or SearchString = "$" Or SearchString = "1" Or SearchString = "2" _
           Or SearchString = "3" Or SearchString = "4" Or SearchString = "5" Or SearchString = "6" Or SearchString = "7" Or SearchString = "8" Or SearchString = "9" Or SearchString = "0" Or SearchString = "<h1>" Or SearchString = "_" _

            */
            public static  List<string> NoiseWords = new List<string>
            {
                "any", "are", "as", "at", "be", "", "but", "by", "can", "did", "do", "for", "get", "got", "has", "had", "his", "how", "if", "in", "is", "it",
                "me", "my", "now", "of", "on", "or", "our", "out", "see", "the", "to", "too", "under", "up", "was", "way", "we", "who", "you", "your", "a", "b", "c",
                "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "$", "1", "2", "3", "4", "5", "6", "7",
                "8", "9", "0", "<h1>", "_"
            };

        public bool IsNoiseWord(string word)
        {
            return NoiseWords.Contains(word);
        }
    }
}
