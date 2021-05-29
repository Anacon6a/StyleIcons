using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
	public class LoginViewModel
	{
        [Required(ErrorMessage = "Поле \"Логин\" обязательно для заполнения")]
        [Display(Name = "Номер телефона или Email")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Поле \"Пароль\" обязательно для заполнения")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
