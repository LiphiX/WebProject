using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
		var sections = postOfficeContext.Sections.Select(item => new SectionViewModel
		{
			Id = item.Id,
			AddressCount = _queriesService.AddressCountOfSection(item.Id),
			SubscriberCount = _queriesService.SubscriberCountOfSection(item.Id),
			SubscriptionCount = _queriesService.SubscriptionCountOfSection(item.Id),
			AppointmentPostman = item.Postman!,
			Postmans = postOfficeContext.People.Where(item => item.Role == Models.Entities.User.Roles.Postman).ToList()
		}).ToList();
			
		return View("List", sections);
	}
}
