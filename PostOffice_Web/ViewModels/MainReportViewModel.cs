using PostOffice.Models.Entities;
using PostOffice.Models.Entities.Sections;

namespace PostOffice.ViewModels;
public class MainReportViewModel
{
	public IEnumerable<Section> Sections { get; set; }

	public IEnumerable<Publication> Publications { get; set; }

	public int PostmanCount { get; set; }

	public int ServicedSectionsCount { get; set; }

	public int DeliveredPublicationsCount { get; set; }
}
