using PortalAboutEverything.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace PortalAboutEverything.Models.Auth
{
    public class RegisrationViewModel
    {
        [Required(ErrorMessage = "Имя пользователя обязательно")]
        public string Login { get; set; }
        
        [Required(ErrorMessage = "Вы забыли пароль")]
        [PasswordSmileRule]
        public string Password { get; set; }

        [RepeatPassword]
        public string RepeatPassword { get; set; }
    }
}
