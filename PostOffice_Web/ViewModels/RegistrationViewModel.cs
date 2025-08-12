using PostOffice.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace PostOffice.ViewModels;
public class RegistrationViewModel
{
	[Required(ErrorMessage = "Фамилия не была введена.")]
	public string Surname { get; set; }

	[Required(ErrorMessage = "Имя не было введено.")]
	public string Name { get; set; }

	[Required(ErrorMessage = "Отчество не было введено.")]
	public string Patronymic { get; set; }

	public List<Address> Addresses { get; set; }

	public int AddressId { get; set; }

	[StringLength(50, ErrorMessage = "Логин не может содержать более 50 символов.")]
	[Required(ErrorMessage = "Логин для учётной записи не был указан.")]
	public string Login { get; set; }

	[StringLength(50, ErrorMessage = "Пароль не может содержать более 50 символов.")]
	[Required(ErrorMessage = "Пароль доступа к учётной записи не был указан.")]
	public string Password { get; set; }
}
