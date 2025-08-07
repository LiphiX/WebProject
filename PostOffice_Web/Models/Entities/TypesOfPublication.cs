using Microsoft.EntityFrameworkCore;
using PostOffice.Models.Database.Configurations;

namespace PostOffice.Models.Entities;

[EntityTypeConfiguration(typeof(TypeOfPublicationConfiguration))]
public class TypesOfPublication
{
    public int Id { get; set; }

    public string Name { get; set; }

    //Отношение 1:M.
    //Одного типа могут быть несколько изданий.
    public virtual List<Publication> Publications { get; set; }
}
