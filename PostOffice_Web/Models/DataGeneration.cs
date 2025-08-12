using ModelsLibrary.Infrastructure;
using PostOffice.Infrastructure.Fabrics;
using PostOffice.Models.Entities;
using PostOffice.Models.Entities.Positions;

namespace PostOffice.Models;
//Класс, в котором определены методы для формирование случайных данных для таблиц базы данных.
public static class DataGeneration
{
    public static List<Person> PeopleGeneration(List<Roles> roles, int number)
    {
        List<Person> people = new List<Person>();
        for (int i = 0; i < number; i++)
            people.Add(PeopleFabric.Fabric(roles));
        return people;
    }
    
    public static List<Publication> PublicationGeneration(int number)
    {
        List<Publication> publications = new List<Publication>();
        for (int i = 0; i < number; i++)
            publications.Add(PublicationFabric.Fabric());

        return publications;
    }

    public static List<Selection> SelectionGeneration(List<Person> people) =>
        people.Where(item => item.Role == Roles.Postman).Select(item => new Selection() { PostmanId = item.Id}).ToList();

    public static List<Address> AddressGeneration(int number, List<Selection> selections)
    {
        List<Address> addresses = new();
        for (int i = 0; i < number; i++)
            addresses.Add(AddressFabric.Fabric(selections[UtilsMethods.RandomValue(0, selections.Count - 1)]));

        return addresses;
    }

    public static List<UserAccount> UserAccountGeneration(List<Person> people)
    {
        people = people.Where(item => item.Role != Roles.Guest).ToList();

        List <UserAccount> userAccounts = new();
        for (int i = 0; i < people.Count; i++)
        {
            var account = UserAccountFabric.Fabric(people[i].Id);
            if (userAccounts.Select(item => item.Login).Contains(account.Login))
                continue;

            userAccounts.Add(account);
        }

        return userAccounts;
    }

    public static List<Subscriber> SubscriberGeneration(List<Person> people, List<Address> addresses) =>
        people.Where(item => item.Role == Roles.Subscriber).Select(item => new Subscriber() { PersonId = item.Id, AddressId = addresses[UtilsMethods.RandomValue(0, addresses.Count-1)].Id}).ToList();

    public static List<Subscription> SubscriptionsGeneration(List<Subscriber> subscribers, List<Publication> publications)
    {
        List<Subscription> subscriptions = new List<Subscription>();
        for (int i = 0; i < subscribers.Count; i++)
            subscriptions.Add(SubscriptionFabric.Fabric(subscribers[i], publications[UtilsMethods.RandomValue(0, publications.Count - 1)]));

        return subscriptions;
    }
}
