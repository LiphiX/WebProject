using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostOffice.Models.Entities.Positions;

namespace PostOffice.Models.Database.Configurations;
public class SubscriberConfiguration : IEntityTypeConfiguration<Subscriber>
{
	public void Configure(EntityTypeBuilder<Subscriber> builder)
	{
		builder
			.HasOne(item => item.Person)
			.WithOne(item => item.Subscriber);

		//builder
		//	.HasOne(item => item.Address)
		//	.WithMany(item => item.Subscribers);
	}
}
