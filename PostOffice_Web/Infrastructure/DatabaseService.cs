using Microsoft.EntityFrameworkCore;
using PostOffice.Models;
using PostOffice.Models.Database;
using PostOffice.Models.Entities;
using PostOffice.Models.Entities.Selections;
using PostOffice.Models.Entities.User;
using PostOffice.Models.Entities.Users;

namespace PostOffice.Infrastructure;
public class DatabaseService(PostOfficeContext postOfficeContext)
{
	private PostOfficeContext postOfficeContext = postOfficeContext;

	//Начальное формирование данных всех таблиц.
	public void SourceInitialization()
	{
		postOfficeContext.People.AddRange(DataGeneration.PeopleGeneration(100, 200, 100, 100, 0));
		postOfficeContext.SaveChanges();
		postOfficeContext.UserAccounts.AddRange(DataGeneration.UserAccountGeneration(GetPeopleTable()));
		postOfficeContext.SaveChanges();
		var list = DataGeneration.PublicationGeneration(500);
		postOfficeContext.Publications.AddRange(list);
		postOfficeContext.SaveChanges();
		postOfficeContext.Selections.AddRange(DataGeneration.SelectionGeneration(8, GetPeopleTable().Where(item => item.Role == Roles.Postman).ToList()));
		postOfficeContext.SaveChanges();

		//list = postOfficeContext.Publications.ToList();
		postOfficeContext.Addresses.AddRange(DataGeneration.AddressGeneration(45, GetSelections()));
		postOfficeContext.SaveChanges();
		postOfficeContext.Subscribers.AddRange(DataGeneration.SubscriberGeneration(GetPeopleTable(), GetAddressesTable()));
		postOfficeContext.SaveChanges();
		postOfficeContext.Subscriptions.AddRange(DataGeneration.SubscriptionsGeneration(GetSubscribersTable(), GetPublicationsTable()));
		postOfficeContext.SaveChanges();
	}

	public List<Person> GetPeopleTable() =>
		postOfficeContext
		.People
		.ToList();

	public async Task<List<Person>> GetPeopleTableAsync() =>
		await postOfficeContext
		.People
		.ToListAsync();

	//public List<TypesOfPublication> GetTypesOfPublicationable() =>
	//	postOfficeContext
	//	.TypesOfPublications
	//	.ToList();

	//public async Task<List<TypesOfPublication>> GetTypesOfPublicationableAsync() =>
	//	await postOfficeContext
	//	.TypesOfPublications
	//	.ToListAsync();
	public List<Address> GetAddressesTable() =>
		postOfficeContext
		.Addresses
		.ToList();

	public async Task<List<Address>> GetAddressesTableAsync() =>
		await postOfficeContext
		.Addresses
		.ToListAsync();

	public List<Publication> GetPublicationsTable() =>
		postOfficeContext
		.Publications
		.ToList();

	public async Task<List<Publication>> GetPublicationsTableAsync() =>
		await postOfficeContext
		.Publications
		.ToListAsync();

	//public List<Role> GetRolesTable() =>
	//    postOfficeContext
	//    .Roles
	//    .ToList();
	//
	//public async Task<List<Role>> GetRolesTableAsync() =>
	//    await postOfficeContext
	//    .Roles
	//    .ToListAsync();

	public List<Subscription> GetSubscriptionTable() =>
		postOfficeContext
		.Subscriptions
		.ToList();

	public async Task<List<Subscription>> GetSubscriptionTableAsync() =>
		await postOfficeContext
		.Subscriptions
		.ToListAsync();

	public List<Subscriber> GetSubscribersTable() =>
		postOfficeContext
		.Subscribers
		.ToList();

	public async Task<List<Subscriber>> GetSubscribersTableAsync() =>
		await postOfficeContext
		.Subscribers
		.ToListAsync();

	public List<Selection> GetSelections() =>
		postOfficeContext
		.Selections
		.ToList();

	public async Task<List<Selection>> GetSelectionsTableAsync() =>
		await postOfficeContext
		.Selections
		.ToListAsync();

	public List<UserAccount> GetUserAccounts() =>
		postOfficeContext
		.UserAccounts
		.ToList();

	public async Task<List<UserAccount>> GetUserAccountsAsync() =>
		await postOfficeContext
		.UserAccounts
		.ToListAsync();

	public async Task SourceInitializationAsync()
	{
		await postOfficeContext.People.AddRangeAsync(DataGeneration.PeopleGeneration(100, 200, 100, 100, 0));
		await postOfficeContext.SaveChangesAsync();
	}
}
