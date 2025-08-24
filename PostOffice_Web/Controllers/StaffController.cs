using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostOffice.Models.Database;
using System.Threading.Tasks;

namespace PostOffice.Controllers;
public class StaffController(PostOfficeContext postOfficeContext) : Controller
{
	[HttpGet]
	[Authorize(Roles = "Director")]
	public IActionResult Applicants()
	{
		return View("Applicants", postOfficeContext.People.Where(item => item.Role == Models.Entities.Users.Roles.Guest || item.Role == Models.Entities.Users.Roles.Subscriber).ToList());
	}

	public async Task<IActionResult> Accept(int id)
	{
		var person = await postOfficeContext.People.FirstOrDefaultAsync(item => item.Id == id);
		if (person == null)
			return NotFound();

		person.Role = Models.Entities.Users.Roles.Postman;
		await postOfficeContext.SaveChangesAsync();

		return Ok();
	}

	[HttpGet]
	[Authorize(Roles = "Director")]
	public IActionResult Postmans()
	{
		var sections = postOfficeContext.Sections.ToList();
		var list = postOfficeContext.People.Where(item => item.Role == Models.Entities.Users.Roles.Postman).ToList();
		return View("Postmans", list);
	}

	[HttpGet]
	[Authorize(Roles = "Director")]
	public async Task<IActionResult> DismissPostman(int id)
	{
		var person = await postOfficeContext.People.FirstOrDefaultAsync(item => item.Id == id);
		if (person == null)
			return NotFound();

		person.Role = Models.Entities.Users.Roles.Guest;

		var list = person.Selections.Where(item => item.Postman != null).ToList();
		foreach (var item in list)
			item.PostmanId = postOfficeContext.People.Where(item => item.Role == Models.Entities.Users.Roles.Postman).First().Id;

		await postOfficeContext.SaveChangesAsync();

		return Ok();
	}

	[HttpGet]
	[Authorize(Roles = "Director")]
	public IActionResult PostalOperators()
	{
		return View("PostalOperators", postOfficeContext.People.Where(item => item.Role == Models.Entities.Users.Roles.PostalOperator).ToList());
	}

	[HttpGet]
	[Authorize(Roles = "Director")]
	public IActionResult Directors()
	{
		return View("Directors", postOfficeContext.People.Where(item => item.Role == Models.Entities.Users.Roles.Director).ToList());
	}
}
