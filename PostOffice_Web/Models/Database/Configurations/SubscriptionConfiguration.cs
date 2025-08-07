using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostOffice.Models.Entities;

namespace PostOffice.Models.Database.Configurations
{
    class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            //Организация отношений между таблицами.
            builder
                .HasOne(item => item.Subscriber)
                .WithMany(item => item.Subscriptions);

            builder
                .HasOne(item => item.Publication)
                .WithMany(item => item.Subscriptions);
        }
    }
}
