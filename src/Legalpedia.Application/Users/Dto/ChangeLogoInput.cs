using System.ComponentModel.DataAnnotations;

namespace Legalpedia.Users.Dto
{
    public class ChangeLogoInput
    {
        [Required]
        public string Base64 { get; set; }
    }
}