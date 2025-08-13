using Microsoft.EntityFrameworkCore;
using PostOffice.Models.Database.Configurations;
using PostOffice.Models.Entities.Selections;

namespace PostOffice.Models.Entities.User;
//Класс, который описывает структуру данных для подписчика.
[EntityTypeConfiguration(typeof(SubscriberConfiguration))]
public class Subscriber
{
    public int Id { get; set; }

    public int PersonId { get; set; }
    public virtual Person Person { get; set; }

    public int AddressId { get; set; }
    public virtual Address Address { get; set; }

	//Список подписок определяемого человека.
	public virtual List<Subscription> Subscriptions { get; set; }
}
