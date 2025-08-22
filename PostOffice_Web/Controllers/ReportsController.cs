using Microsoft.AspNetCore.Mvc;
using PostOffice.Models.Database;
using PostOffice.Models.Entities;
using PostOffice.Models.Services;
using PostOffice.ViewModels;

namespace PostOffice.Controllers;
public class ReportsController(PostOfficeContext postOfficeContext) : Controller
{

	private QueriesService _queriesService { get; set; } = new(postOfficeContext);

	public IActionResult Requests()
	{
		var viewModel = new RequestsViewModel()
		{
			NumberOfEachPublications = _queriesService.NumberPublications(),
			PostmansCount = _queriesService.PostmansCount(),
			SectionWithMaxPublicationsNumber = _queriesService.SectionWithMaxSubscriptions(),
			AverageDurationOfSubscription = _queriesService.AverageSubscriptionDuration()
		};

		return View("Requests", viewModel);
	}

	public IActionResult Information()
	{
		var viewModel = new InformationViewModel()
		{
			SubscribersCount = postOfficeContext.Subscribers.Count(),
			PublicationsCount = _queriesService.IssuedPublicationsCount()
		};

		return View("Information", viewModel);
	}

	public IActionResult MainReport()
	{
		var publication = postOfficeContext.Publications.DefaultIfEmpty();
		if (publication == null)
			new EmptyResult();

		var viewModel = new MainReportViewModel()
		{
			Sections = postOfficeContext.Sections,
			Publications = publication,
			PostmanCount = _queriesService.PostmansCount(),
			ServicedSectionsCount = _queriesService.ServicedSectionsCount(),
			DeliveredPublicationsCount = _queriesService.IssuedPublicationsCount()
		};

		return View("MainReport", viewModel);
	}
}
