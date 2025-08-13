using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostOffice.Models.Entities.Selections;

namespace PostOffice.Models.Database.Configurations;
public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder
            .HasOne(item => item.Selection)
            .WithMany(item => item.Addresses);

        builder
            .HasMany(item => item.Subscribers)
            .WithOne(item => item.Address);
            //.IsRequired(false);

        //Задание ограничений для свойств сущности Address.
        builder
            .Property(item => item.Street)
            .HasMaxLength(30)
            .IsRequired();

        builder
            .Property(item => item.Home)
            .HasMaxLength(30)
            .IsRequired();

        builder.HasData(new Address[]
        {
            //new() {Id = 1, AddressName = "ул. Жукова, 34D, 23",         SelectionId = 1},
            //new() {Id = 2, AddressName = "ул. Рокоссовского, 25E, 87",  SelectionId = 2},
            //new() {Id = 3, AddressName = "ул. Тухачевского, 24B, 56",   SelectionId = 3},
            //new() {Id = 4, AddressName = "ул. Жукова, 56D, 75",         SelectionId = 4},
            //new() {Id = 5, AddressName = "ул. Василевского, 37С, 85",   SelectionId = 5},
            //new() {Id = 6, AddressName = "ул. Рокоссовского, 38D, 34",  SelectionId = 6},
            //new() {Id = 7, AddressName = "ул. Конева, 36D, 76",         SelectionId = 7},
            //new() {Id = 8, AddressName = "ул. Жукова, 78B, 84",         SelectionId = 8},
            //new() {Id = 9, AddressName = "ул. Василевского, 75D, 78",   SelectionId = 9},
            //new() {Id = 10, AddressName = "ул. Рокоссовского, 34D, 56", SelectionId = 10},
            //new() {Id = 11, AddressName = "ул. Жукова, 34D, 28",        SelectionId = 11},
            //new() {Id = 12, AddressName = "ул. Рокоссовского, 78D, 24", SelectionId = 12}
        });
    }
}
