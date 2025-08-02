using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostOffice.Models.Entities;

namespace PostOffice.Models.Database.Configurations;
public class TypeOfPublicationConfiguration : IEntityTypeConfiguration<TypesOfPublication>
{
    public void Configure(EntityTypeBuilder<TypesOfPublication> builder)
    {
        builder
            .Property(item => item.Name)
            .HasMaxLength(20);

        builder.HasData(new TypesOfPublication[]
        {
            new() { Id = 1, Name = "Книга"},
            new() { Id = 2, Name = "Пособие"},
            new() { Id = 3, Name = "Альманах"},
            new() { Id = 4, Name = "Журнал"},
            new() { Id = 5, Name = "Газета"}
        });
    }
}
