using PostOffice.Models.Entities.Sections;
using PostOffice.Models.Reports;

namespace PostOffice.ViewModels;
public class RequestsViewModel
{
	public List<Query01> NumberOfEachPublications { get; set; }

	public int PostmansCount { get; set; }

	public Section SectionWithMaxPublicationsNumber { get; set; }

	public List<Query06> AverageDurationOfSubscription { get; set; }
}
