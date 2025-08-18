using Microsoft.AspNetCore.Mvc.Rendering;
using PostOffice.Models.Entities.Users;

namespace PostOffice.ViewModels;
public class SectionViewModel
{
	public int Id { get; set; }
	public int RowNumber { get; set; }

	public int AddressCount { get; set; }

	public int SubscriptionCount { get; set; }

	public int SubscriberCount { get; set; }

	public Person AppointmentPostman { get; set; }

	public List<Person> Postmans { get; set; }
}
