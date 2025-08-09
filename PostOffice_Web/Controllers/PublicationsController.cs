using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PostOffice.Models.Database;
using PostOffice.Models.Entities;

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
	[Route("/Publications/Subscribe/{id}")]
	public IActionResult Subscribe(int id)
	{
		var publication = postOfficeContext.Publications.FirstOrDefault(item => item.Id == id);
		if(publication == null)
			return NotFound();

		return View("Subscribe", publication);
	}
}
