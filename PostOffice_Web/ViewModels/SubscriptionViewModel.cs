using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace PostOffice.ViewModels;
public class SubscriptionViewModel
{
	public int PublicationId { get; set; }
	public string Title { get; set; }
	public string? Author { get; set; }
	public int Cost { get; set; }
	public string TypeOfPublication { get; set; }
	public string? ImageAddress { get; set; }

	public SelectList Streets { get; set; }

	[Required(ErrorMessage = "Требуется указать наименование улицы.")]
	public string Street { get; set; }

	[Required(ErrorMessage = "Требуется указать номер дома.")]
	public string Home { get; set; }

	[Range(1, 24, ErrorMessage = "Длительность подписки не может быть менее 1 или более 24 месяцев.")]
	[Required(ErrorMessage = "Требуется указать длительность подписки!")]
	public int DurationOfSubscription { get; set; }


}
