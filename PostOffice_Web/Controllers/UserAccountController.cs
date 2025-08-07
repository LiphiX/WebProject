using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using PostOffice.Models.Database;
using PostOffice.Models.Entities;
using PostOffice.ViewModels;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PostOffice.Controllers;
public class UserAccountController(PostOfficeContext postOfficeContext) : Controller
{
	[HttpGet]
	public IActionResult Registration()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Registration(RegistrationViewModel viewModel)
	{
		if (!ModelState.IsValid)
			return View("Registration", viewModel);


		Person person = new() { Surname = viewModel.Surname, Name = viewModel.Name, Patronymic = viewModel.Patronymic, Role = Roles.Guest };
		await postOfficeContext.People.AddAsync(person);

		UserAccount userAccount = new() { Login = viewModel.Login, Password = viewModel.Password, PersonId = person.Id };
		await postOfficeContext.UserAccounts.AddAsync(userAccount);

		//HttpContext.Session.SetString("SessionId", Guid.NewGuid().ToString());

		UserAuthenticate(userAccount);

		return Redirect("/Home/Index");
	}

	[HttpGet]
	public IActionResult Authorization()
	{
		return View("Authorization");
	}

	[HttpPost]
	public async Task<IActionResult> Authorization(LoginViewModel viewModel)
	{
		if (!ModelState.IsValid)
			return View("Authorization", viewModel);

		UserAccount? userAccount = await postOfficeContext.UserAccounts.FirstOrDefaultAsync(item => item.Login == viewModel.Login && item.Password == viewModel.Password);
		if(userAccount == null)
		{
			ModelState.AddModelError("", "Не удалось войти в учётную запись.");
			return View("Authorization", viewModel);
		}
		
		UserAuthenticate(userAccount);

		return Redirect("/Home/Index");
	}

	public async Task UserAuthenticate(UserAccount userAccount)
	{
		//Запись данных о пользователя для его идентификации.
		var claims = new List<Claim>()
		{
			new Claim(ClaimsIdentity.DefaultNameClaimType, userAccount.Id.ToString()),
			new Claim(ClaimsIdentity.DefaultRoleClaimType, userAccount.Person.Role.ToString()),
		};

		//Сохранение данных о пользователя в объекте "удостоверения".
		ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie");

		//Установка в локальное хранилище пользователя зашифрованных Cookie для аунтентификации последующих запросов пользователя.
		await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
	}
}
