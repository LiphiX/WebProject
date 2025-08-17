using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace PostOffice.ViewModels;
public class SubscriptionViewModel
{
	[ValidateNever]
	public int PublicationId { get; set; }
	[ValidateNever]
	public string Title { get; set; }
	[ValidateNever]
	public string? Author { get; set; }
	[ValidateNever]
	public int Cost { get; set; }
	[ValidateNever]
	public string TypeOfPublication { get; set; }
	[ValidateNever]
	public string? ImageAddress { get; set; }
	[ValidateNever]
	public SelectList Streets { get; set; }

	[Required(ErrorMessage = "Требуется указать наименование улицы.")]
	public string Street { get; set; }

	[Required(ErrorMessage = "Требуется указать номер дома.")]
	public string Home { get; set; }

	[Range(1, 24, ErrorMessage = "Длительность подписки не может быть менее 1 или более 24 месяцев.")]
	[Required(ErrorMessage = "Требуется указать длительность подписки!")]
	public int DurationOfSubscription { get; set; }


}
