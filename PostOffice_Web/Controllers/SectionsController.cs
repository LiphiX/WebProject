using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PostOffice.Models.Database;
using PostOffice.Models.Entities.Sections;
using PostOffice.Models.Services;
using PostOffice.ViewModels;

namespace PostOffice.Controllers;
public class SectionsController(PostOfficeContext postOfficeContext) : Controller
{
	private QueriesService _queriesService = new(postOfficeContext);

	[Authorize(Roles="Director")]
	public async Task<IActionResult> List()
	{
		var sections = TakeSections(0);

		return View("List", sections);
	}

	[HttpGet]
	[Route("/Sections/UploadData/{numberOfDownload}")]
	public async Task<IActionResult> UploadData(int numberOfDownload)
	{
		if (numberOfDownload < 0)
			return NotFound();

		//Сохранение очередные 10 строк участков для последующего отображения в табличном формате.
		var sections = TakeSections(numberOfDownload);

		//Отправка частичного представления набора карт.
		return PartialView("~/Views/Shared/PartialView/SectionTableRows.cshtml", sections);
	}

	private List<SectionViewModel> TakeSections(int numberOfDownload) =>
		postOfficeContext.Sections.Skip(numberOfDownload*10).Take(10).Select(item => new SectionViewModel
		{
			Id = item.Id,
			RowNumber = numberOfDownload*10,
			AddressCount = _queriesService.AddressCountOfSection(item.Id),
			SubscriberCount = _queriesService.SubscriberCountOfSection(item.Id),
			SubscriptionCount = _queriesService.SubscriptionCountOfSection(item.Id),
			AppointmentPostman = item.Postman!,
			Postmans = postOfficeContext.People.Where(item => item.Role == Models.Entities.Users.Roles.Postman).ToList()
		}).OrderBy(item => item.Id).ToList();

	[HttpPost]
	[Route("/Sections/Appointment/")]
	public async Task<IActionResult> Appointment([FromBody] AppointmentVewModel viewModel)
	{
		var section = await postOfficeContext.Sections.FirstOrDefaultAsync(item => item.Id == viewModel.sectionId);
		if (section == null)
			return NotFound();

		var postman = await postOfficeContext.People.Where(item => item.Role == Models.Entities.Users.Roles.Postman).FirstOrDefaultAsync(item => item.Id == viewModel.postmanId);
		if (postman == null)
			return NotFound();

		section.PostmanId = postman.Id;
		await postOfficeContext.SaveChangesAsync();

		return Ok();
	}
}
