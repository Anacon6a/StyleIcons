using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
	public class RegisterViewModel
	{
        [Required (ErrorMessage = "Поле \"Имя\" обязательно для заполнения")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле \"Фамилия\" обязательно для заполнения")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Поле \"Номер телефона\" обязательно для заполнения")]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        //[Required(ErrorMessage = "Поле \"Email\" обязательно для заполнения")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле \"Пароль\" обязательно для заполнения")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле \"Подтверждение пароля\" обязательно для заполнения")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        public string PasswordConfirm { get; set; }
    }
}
