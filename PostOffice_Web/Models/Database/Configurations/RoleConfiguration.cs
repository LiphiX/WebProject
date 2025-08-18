using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostOffice.Models.Entities.Users;

namespace PostOffice.Models.Database.Configurations;
public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder
            .Property(item => item.Name)
            .HasMaxLength(30)
            .IsRequired();

        builder.HasData(new Role[]
        {
            new() { Id = 1, Name = "Гость" },
            new() { Id = 2, Name = "Подписчик" },
            new() { Id = 3, Name = "Почтальон" },
            new() { Id = 4, Name = "Оператор" },
            new() { Id = 5, Name = "Директор" }
        });
    }
}
