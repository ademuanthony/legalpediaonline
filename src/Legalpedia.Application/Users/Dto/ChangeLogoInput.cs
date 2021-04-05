using System.ComponentModel.DataAnnotations;

namespace Legalpedia.Users.Dto
{
    public class ChangeLogoInput
    {
        [Required]
        public string Image { get; set; }
    }
    
    public class ChangePictureInput
    {
        [Required]
        public string Image { get; set; }
    }
}