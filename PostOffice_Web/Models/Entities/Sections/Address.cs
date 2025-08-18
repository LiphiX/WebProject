using Microsoft.EntityFrameworkCore;
using PostOffice.Models.Database.Configurations;
using PostOffice.Models.Entities.Users;

namespace PostOffice.Models.Entities.Sections;
[EntityTypeConfiguration(typeof(AddressConfiguration))]
public class Address
{
    public int Id { get; set; }

    public string Street { get; set; }

    public string Home { get; set; }

    public int SectionId { get; set; }
    public virtual Section Section { get; set; }

    public virtual List<Subscriber> Subscribers { get; set; }
}
