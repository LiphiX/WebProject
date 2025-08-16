using ModelsLibrary.Infrastructure;
using PostOffice.Infrastructure.Fabrics;
using PostOffice.Models.Entities;
using PostOffice.Models.Entities.Sections;
using PostOffice.Models.Entities.User;
using PostOffice.Models.Entities.Users;

namespace PostOffice.Models.Services;
//Класс, в котором определены методы для формирование случайных данных для таблиц базы данных.
public static class DataGeneration
{
    public static List<Person> PeopleGeneration(int guestNumber, int subscriberNumber, int postmanNumber, int postalOperatorNumber, int directorNumber)
    {
        List<Person> people = new List<Person>();
        for (int i = 0; i < subscriberNumber; i++)
            people.Add(PeopleFabric.Fabric(Roles.Guest));

        for (int i = 0; i < subscriberNumber; i++)
            people.Add(PeopleFabric.Fabric(Roles.Subscriber));

        for (int i = 0; i < postmanNumber; i++)
            people.Add(PeopleFabric.Fabric(Roles.Postman));

        for (int i = 0; i < postalOperatorNumber; i++)
            people.Add(PeopleFabric.Fabric(Roles.PostalOperator));

        for (int i = 0; i < directorNumber; i++)
            people.Add(PeopleFabric.Fabric(Roles.PostalOperator));
        return people;
    }
    
    public static List<Publication> PublicationGeneration(int number)
    {
        List<Publication> publications = new List<Publication>();
        for (int i = 0; i < number; i++)
            publications.Add(PublicationFabric.Fabric());

        return publications;
    }

    public static List<Section> SelectionGeneration(int number, List<Person> postmans)
    {
        List<Section> selections = new();
        if (number > postmans.Count)
        {
            int appointsmentNumber = number / postmans.Count;
            for (int i = 0; i < number; i++)
                for (int j = 0; j < appointsmentNumber; j++)
                    selections.Add(new() { PostmanId = postmans[i].Id });
        }
        else
            for(int i = 0; i < number; i++)
                selections.Add(new() { PostmanId = postmans[i].Id });

        return selections;
    }

    public static List<Address> AddressGeneration(int number, List<Section> selections)
    {
        List<Address> addresses = new();

        //Определение количества адресов, которое может входить в один участок.
        //Если количество представляет из себя нецелое число, то из этого следует:
        //Результат < 1: один из участков не сможет содержать адреса.
        //Результат > 1: один (или несколько) участков смогут содержать более одного адреса.
        double addressesNumberInSelection = number / (double)selections.Count;
        
        //Количество нераспределённых адресов.
        int unallocatedAddresses = (int)((addressesNumberInSelection - (int)addressesNumberInSelection) * selections.Count);

        addresses.Clear();
        //Количество итераций распределения.
        //int iterationAllocation = UtilsMethods.RandomValue(unallocatedAddresses > 1 ? 1 : 0, unallocatedAddresses);
        //int addingInOneItaration = (int)Math.Ceiling((double)unallocatedAddresses / iterationAllocation);

        for (int i = 0; i < selections.Count; i++)
        {
            //addresses.Add(AddressFabric.Fabric(selections[UtilsMethods.RandomValue(0, selections.Count - 1)]));

            //if (iterationAllocation != 0)
                //for (int j = 0; j < iterationAllocation; j++)
                    //i++;
                    //for(int z = 0; z < addingInOneItaration; z++)
                        //addresses.Add(AddressFabric.Fabric(selections[i]));

            if(unallocatedAddresses != 0)
            {
                addresses.Add(AddressFabric.Fabric(selections[i]));
				unallocatedAddresses--;
			}

            for(int j = 0; j < (int)addressesNumberInSelection; j++)
                addresses.Add(AddressFabric.Fabric(selections[i]));
        }

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
