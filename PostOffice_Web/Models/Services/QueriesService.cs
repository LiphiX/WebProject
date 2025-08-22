using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PostOffice.Models.Database;
using PostOffice.Models.Entities;
using PostOffice.Models.Entities.Sections;
using PostOffice.Models.Entities.Users;
using PostOffice.Models.Reports;

namespace PostOffice.Models.Services;
public class QueriesService(PostOfficeContext context)
{

	public async Task<bool> SubscriptionRegistration(Person person, Publication publication, int subcsriptionDuration, string street, string home)
	{
		if (person == null || publication == null)
			return false;

		Address address = (await context.Addresses.FirstOrDefaultAsync(item => item.Street == street && item.Home == home))!;
		if (address == null) {
			address = new() { Street = street, Home = home, SectionId = context.Addresses.Where(item => item.Street == street).First().SectionId };
			await context.Addresses.AddAsync(address);
			await context.SaveChangesAsync();
		}

		Subscriber subscriber = context.Subscribers.FirstOrDefault(item => item.PersonId == person.Id)!;
		if (subscriber == null)
		{
			subscriber = new() { PersonId = person.Id, AddressId = address.Id };
			await context.Subscribers.AddAsync(subscriber);
			await context.SaveChangesAsync();
		}

		Subscription subscription = new() { PublicationId = publication.Id, StartDate = DateTime.Now, SubscriberId = subscriber.Id, SubscriptionCompleted = false, SubscriptionDuration = subcsriptionDuration };
		await context.Subscriptions.AddAsync(subscription);

		if (person.Role == Roles.Guest)
			person.Role = Roles.Subscriber;

		await context.SaveChangesAsync();

		return true;
	}

	public int AddressCountOfSection(int sectionId)
	{
		var section = context.Sections.FirstOrDefault(item => item.Id == sectionId);
		if (section == null)
			throw new Exception("Почтовый участок не обнаружен");

		return section
			.Addresses
			.Count();
	}

	public async Task<int> AddressCountOfSectionAsync(int sectionId)
	{
		var section = await context.Sections.FirstOrDefaultAsync(item => item.Id == sectionId);
		if(section == null)
			throw new Exception("Почтовый участок не обнаружен");

		return section
			.Addresses
			.Count();
	}

	public int SubscriptionCountOfSection(int sectionId)
	{
		var section = context.Sections.FirstOrDefault(item => item.Id == sectionId);
		if (section == null)
			throw new Exception("Почтовый участок не обнаружен");

		return section
			.Addresses
			.Select(item => item.Subscribers.Select(item => item.Subscriptions.Count()).Sum())
			.Sum();
	}

	public async Task<int> SubscriptionCountOfSectionAsync(int sectionId)
	{
		var section = await context.Sections.FirstOrDefaultAsync(item => item.Id == sectionId);
		if (section == null)
			throw new Exception("Почтовый участок не обнаружен");

		return section
			.Addresses
			.Select(item => item.Subscribers.Select(item => item.Subscriptions.Count()).Sum())
			.Sum();
	}

	public int SubscriberCountOfSection(int sectionId)
	{
		var section = context.Sections.FirstOrDefault(item => item.Id == sectionId);
		if (section == null)
			throw new Exception("Почтовый участок не обнаружен");

		return section
			.Addresses
			.Select(item => item.Subscribers.Count())
			.Sum();
	}

	public async Task<int> SubscriberCountOfSectionAsync(int sectionId)
	{
		var section = await context.Sections.FirstOrDefaultAsync(item => item.Id == sectionId);
		if (section == null)
			throw new Exception("Почтовый участок не обнаружен");

		return section
			.Addresses
			.Select(item => item.Subscribers.Count())
			.Sum();
	}

	public List<Query01> NumberPublications() => context.Publications
		.GroupBy(item => item.Title)
		.Select(item => new Query01 {
			Title = item.Key,
			Count = item.Count(),
	}).ToList();

	public int PostmansCount() => context.People.Where(item => item.Role == Roles.Postman).Count();

	//public List<Publication> IssuedPublications(int id) { }

	public Section SectionWithMaxSubscriptions() => context.Sections.First();

	public List<Query06> AverageSubscriptionDuration()
	{
		var list = context.Publications.GroupBy(item => item.Title).Select(item => new Query06
		{
			Title = item.Key,
			Average = item.Average(item => item.Subscriptions.Sum(item => item.SubscriptionDuration))
		}).ToList();

		return list;
	}

	public int IssuedPublicationsCount() => context.Subscriptions.Where(item => item.SubscriptionCompleted).Select(item => item.Publication).Count();

	public int ServicedSectionsCount() => context.Sections.Where(item => item.Postman != null).Count();
}
