using Microsoft.EntityFrameworkCore;
using PostOffice.Models.Database.Configurations;

namespace PostOffice.Models.Entities.Positions;
//Класс, который описывает структуру данных для подписчика.
[EntityTypeConfiguration(typeof(SubscriberConfiguration))]
public class Subscriber
{
    public int Id { get; set; }

    public int PersonId { get; set; }
    public virtual Person Person { get; set; }

	//Список подписок определяемого человека.
	public virtual List<Subscription> Subscriptions { get; set; }
}
