using System.ComponentModel.DataAnnotations;

namespace WochenberichtManagement.Models.BindingModel
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}