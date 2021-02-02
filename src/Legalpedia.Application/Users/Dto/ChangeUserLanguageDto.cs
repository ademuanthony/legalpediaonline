using System.ComponentModel.DataAnnotations;

namespace Legalpedia.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}