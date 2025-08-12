using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostOffice.Models.Database;
using PostOffice.Models.Entities;
using PostOffice.Models.Entities.Positions;
using PostOffice.ViewModels;
using System.Security.Claims;

namespace PostOffice.Controllers;
public class PublicationsController(PostOfficeContext postOfficeContext) : Controller
{
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

		return View("Subscribe", new SubscriptionViewModel() { PublicationId = publication.Id, Title = publication.Title, Author = publication.Author, Cost = publication.Cost, TypeOfPublication = publication.TypeOfPublication.Name, ImageAddress = publication.ImageAddress });
	}

	[Authorize]
	[HttpPost]
	//[Route("/Publications/Subscribe")]
	public async Task<IActionResult> Subscribe(SubscriptionViewModel subscriptionViewModel)
	{
		if (!ModelState.IsValid)
		{
			return View("Subscribe", subscriptionViewModel);
		}

		var publication = await postOfficeContext.Publications.FirstOrDefaultAsync(item => item.Id == subscriptionViewModel.PublicationId);
		if (publication == null)
			return NotFound();

		var userAccountId = int.Parse(User.Claims.First(item => item.Type == ClaimTypes.NameIdentifier).Value);
		var userAccount = await postOfficeContext.UserAccounts.FirstOrDefaultAsync(item => item.Id == userAccountId);

		if (userAccount == null)
			return Unauthorized();

		var person = await postOfficeContext.People.FirstOrDefaultAsync(item => item.Id == userAccount.PersonId);

		if (person == null)
			return Unauthorized();

		if (person.Subscriber == null)
		{
			var subscriber = new Subscriber() { PersonId = person.Id,  };
			await postOfficeContext.Subscribers.AddAsync(subscriber);

			if (person.Role == Roles.Guest)
				person.Role = Roles.Subscriber;

			await postOfficeContext.SaveChangesAsync();
		}
		
		var subscription = new Subscription()
		{ PublicationId = publication.Id,
			StartDate = DateTime.Now,
			SubscriberId = person.Subscriber!.Id,
			SubscriptionCompleted = false,
			SubscriptionDuration = subscriptionViewModel.DurationOfSubscription };

		await postOfficeContext.Subscriptions.AddAsync(subscription);
		await postOfficeContext.SaveChangesAsync();

		return View("PublicationsList", postOfficeContext.Publications.Take(8));
	}
}
