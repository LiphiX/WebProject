using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostOffice.Models.Entities;

namespace PostOffice.Models.Database.Configurations;
public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
	public void Configure(EntityTypeBuilder<Session> builder)
	{
		//Задание ограничений для свойств класса Session.
		builder
			.HasIndex(item => item.SessionId)
			.IsUnique();

		//Организация отношений между сущностями.
		builder
			.HasOne(item => item.UserAccount)
			.WithOne(item => item.Session);
	}
}
