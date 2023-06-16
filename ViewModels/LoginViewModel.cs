using System.ComponentModel.DataAnnotations;

namespace MeetUp.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
