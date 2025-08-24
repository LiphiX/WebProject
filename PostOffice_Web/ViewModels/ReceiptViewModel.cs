using System.Diagnostics.Contracts;

namespace PostOffice.ViewModels;
public class ReceiptViewModel
{
	public string SubscriberFullName { get; set; }

	public string Title { get; set; }

	public string? Author { get; set; }

	public int Cost { get; set; }

	public string TypeOfPublication { get; set; }

	public int SubscriptionDuration { get; set; }

	public string Street { get; set; }

	public string Home { get; set; }
}
