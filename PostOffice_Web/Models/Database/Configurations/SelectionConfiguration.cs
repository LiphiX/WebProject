using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostOffice.Models.Entities;

namespace PostOffice.Models.Database.Configurations;
public class SelectionConfiguration : IEntityTypeConfiguration<Selection>
{
    public void Configure(EntityTypeBuilder<Selection> builder)
    {
        builder
            .HasOne(item => item.Postman)
            .WithMany(item => item.Selections);

        //builder
        //    .HasMany(item => item.Addresses)
        //    .WithOne(item => item.Selection);

        builder.HasData(new Selection[]
        {
            new() {Id = 1, PostmanId = 1},
            new() {Id = 2, PostmanId = 2},
            new() {Id = 3, PostmanId = 3},
            new() {Id = 4, PostmanId = 4},
            new() {Id = 5, PostmanId = 5},
            new() {Id = 6, PostmanId = 6},
            new() {Id = 7, PostmanId = 7},
            new() {Id = 8, PostmanId = 8},
            new() {Id = 9, PostmanId = 9},
            new() {Id = 10, PostmanId = 10},
            new() {Id = 11, PostmanId = 11},
            new() {Id = 12, PostmanId = 12},
        });
    }
}
