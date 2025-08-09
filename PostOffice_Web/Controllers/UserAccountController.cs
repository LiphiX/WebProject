using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using PostOffice.Models;
using PostOffice.Models.Database;
using PostOffice.Models.Entities;
using PostOffice.ViewModels;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PostOffice.Controllers;
public class UserAccountController(PostOfficeContext postOfficeContext) : Controller
{
	private UserService _userService = new(postOfficeContext);

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
		await _userService.AddAsync(userAccount);

		//HttpContext.Session.SetString("SessionId", Guid.NewGuid().ToString());

		await UserAuthenticate(userAccount);

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

		UserAccount? userAccount = await _userService.FindUserAccountAsync(viewModel.Login, viewModel.Password);
		if(userAccount == null)
		{
			ModelState.AddModelError("", "Не удалось войти в учётную запись.");
			return View("Authorization", viewModel);
		}
		
		await UserAuthenticate(userAccount);

		return Redirect("/Home/Index");
	}

	public async Task UserAuthenticate(UserAccount userAccount)
	{
		//Запись данных о пользователя для его идентификации.
		var claims = new List<Claim>()
		{
			new Claim(ClaimTypes.Name, userAccount.Login),
			new Claim(ClaimsIdentity.DefaultRoleClaimType, userAccount.Person.Role.ToString()),
			new Claim(ClaimTypes.NameIdentifier, userAccount.Id.ToString()),
		};

		//Сохранение данных о пользователя в объекте "удостоверения".
		ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie");

		//Установка в локальное хранилище пользователя зашифрованных Cookie для аунтентификации последующих запросов пользователя.
		await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

		//var temp = User.Claims.Where(item => item.Type == ClaimTypes.NameIdentifier).ToList();
		//var data = temp[0].Value;
	}

	public async Task<IActionResult> LogOut()
	{
		await HttpContext.SignOutAsync();

		return Redirect("/Home/Index");
	}

	[HttpGet]
	[Authorize]
	public IActionResult PersonalAccount()
	{
		return View("PersonalAccount");
	}
}
