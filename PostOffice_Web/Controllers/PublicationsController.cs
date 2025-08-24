using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostOffice.Infrastructure.Fabrics;
using PostOffice.Models.Database;
using PostOffice.Models.Entities;
using PostOffice.Models.Entities.Sections;
using PostOffice.Models.Entities.Users;
using PostOffice.Models.Services;
using PostOffice.ViewModels;
using System.Security.Claims;
using System.Security.Cryptography.Xml;

namespace PostOffice.Controllers;
public class PublicationsController(PostOfficeContext postOfficeContext) : Controller
{

	private QueriesService _queriesService = new(postOfficeContext);

	[HttpGet]
	public IActionResult PublicationsList()
	{
		return View("PublicationsList", postOfficeContext.Publications.Take(8));
	}

	[HttpGet]
	[Route("/Publications/UploadData/{numberOfDownload}")]
	public async Task<IActionResult> UploadData(int numberOfDownload)
	{
		if (numberOfDownload < 0)
			throw new Exception("Количество загрузок страницы меньше 0!");

		//Сохранение очередных пяти объектов класса изданий для их последующего отображения.
		var collection = postOfficeContext.Publications.Skip(numberOfDownload * 8).Take(8);

		//Отправка частичного представления набора карт.
		return PartialView("~/Views/Shared/PartialView/Cards.cshtml", collection);
	}

	public IActionResult CardPartialView(Publication publication) =>
		PartialView("/PartialView/Card");

	[Authorize]
	[HttpGet]
	//[Route("/Publications/Subscribe/{id}")]
	public IActionResult Subscribe(int id)
	{
		var publication = postOfficeContext.Publications.FirstOrDefault(item => item.Id == id);
		if(publication == null)
			return NotFound();

		var viewModel = new SubscriptionViewModel()
		{
			PublicationId = publication.Id,
			Title = publication.Title,
			Author = publication.Author,
			Cost = publication.Cost,
			TypeOfPublication = publication.TypeOfPublication.Name,
			ImageAddress = publication.ImageAddress,
			Streets = new(postOfficeContext.Addresses.Select(item => item.Street).AsEnumerable()),
		};

		HttpContext.Session.SetInt32("id", viewModel.PublicationId);

		return View("Subscribe", viewModel);
	}

	[Authorize]
	[HttpPost]
	//[Route("/Publications/Subscribe")]
	public async Task<IActionResult> Subscribe(SubscriptionViewModel subscriptionViewModel)
	{
		subscriptionViewModel.PublicationId = HttpContext.Session.GetInt32("id")!.Value;
		var publication = await postOfficeContext.Publications.FirstOrDefaultAsync(item => item.Id == subscriptionViewModel.PublicationId);
		if (publication == null)
			return NotFound();

		subscriptionViewModel.Title = publication.Title;
		subscriptionViewModel.Author = publication.Author;
		subscriptionViewModel.Cost = publication.Cost;
		subscriptionViewModel.TypeOfPublication = publication.TypeOfPublication.Name;
		subscriptionViewModel.ImageAddress = publication.ImageAddress;
		subscriptionViewModel.Streets = new(postOfficeContext.Addresses.Select(item => item.Street).AsEnumerable());

		if (!ModelState.IsValid)
			return View("Subscribe", subscriptionViewModel);

		var userAccountId = int.Parse(User.Claims.First(item => item.Type == ClaimTypes.NameIdentifier).Value);
		var userAccount = await postOfficeContext.UserAccounts.FirstOrDefaultAsync(item => item.Id == userAccountId);

		if (userAccount == null)
			return Unauthorized();

		if (!(await _queriesService.SubscriptionRegistration(userAccount.Person, publication, subscriptionViewModel.DurationOfSubscription, subscriptionViewModel.Street, subscriptionViewModel.Home)))
			return NotFound();

		return RedirectToAction("Receipt", new ReceiptViewModel() {
			Title = subscriptionViewModel.Title,
			SubscriberFullName = userAccount.Person.FullName,
			Author = subscriptionViewModel.Author,
			Cost = subscriptionViewModel.Cost,
			TypeOfPublication = subscriptionViewModel.TypeOfPublication,
			SubscriptionDuration = subscriptionViewModel.DurationOfSubscription,
			Street = subscriptionViewModel.Street,
			Home = subscriptionViewModel.Home,
		});
	}

	[Authorize]
	[HttpGet]
	[Route("/Publications/Receipt")]
	public IActionResult Receipt(ReceiptViewModel viewModel)
	{
		return View("Receipt", viewModel);
	}
}
