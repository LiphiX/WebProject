using Microsoft.EntityFrameworkCore;
using PostOffice.Models.Database.Configurations;

namespace PostOffice.Models.Entities;
[EntityTypeConfiguration(typeof(AddressConfiguration))]
public class Address
{
    public int Id { get; set; }

    public string AddressName { get; set; }

    public int SelectionId { get; set; }
    public virtual Selection Selection { get; set; }
}
