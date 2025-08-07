using System.ComponentModel.DataAnnotations;

namespace PostOffice.ViewModels;
public class LoginViewModel
{
	[Required(ErrorMessage = "Логин для входа в учётную запись не был указан.")]
	public string Login { get; set; }

	[Required(ErrorMessage = "Пароль для входа в учётную запись не был указан.")]
	[StringLength(50, ErrorMessage = "Пароль не может содержать более 50 символов.")]
	public string Password { get; set; }

	[Compare("Password", ErrorMessage = "Введённые пароли не совпадают.")]
	[Required(ErrorMessage = "Подтвердите пароль.")]
	[StringLength(50, ErrorMessage = "Пароль не может содержать более 50 символов.")]
	public string ConfirmPassword { get; set; }
}
