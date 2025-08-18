using Microsoft.EntityFrameworkCore;
using PostOffice.Models.Database.Configurations;


namespace PostOffice.Models.Entities.Users;
[EntityTypeConfiguration(typeof(RoleConfiguration))]
public class Role
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual List<Person> People { get; set; }
}
