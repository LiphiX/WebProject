using Microsoft.EntityFrameworkCore;
using PostOffice.Models.Database.Configurations;
using PostOffice.Models.Entities.User;

namespace PostOffice.Models.Entities.Selections;
[EntityTypeConfiguration(typeof(AddressConfiguration))]
public class Address
{
    public int Id { get; set; }

    public string Street { get; set; }

    public string Home { get; set; }

    public int SelectionId { get; set; }
    public virtual Selection Selection { get; set; }

    public virtual List<Subscriber> Subscribers { get; set; }
}
