using Microsoft.EntityFrameworkCore;
using PostOffice.Models.Database.Configurations;
using PostOffice.Models.Entities.User;

namespace PostOffice.Models.Entities;
//Класс, который описывает структуру данных для подписки.
[EntityTypeConfiguration(typeof(SubscriptionConfiguration))]
public class Subscription
{
    public int Id { get; set; }

    //Дата начала подписки на издание.
    public DateTime StartDate { get; set; }

    //Длительность действия подписки на издания.
    public int SubscriptionDuration { get; set; }

    //Завершено ли оформление подписки оператором почтовой связи.
    public bool SubscriptionCompleted { get; set; }

    //Данные о подписчике.
    public int SubscriberId { get; set; }
    public virtual Subscriber Subscriber { get; set; }

    public int PublicationId { get; set; }
    public virtual Publication Publication { get; set; }
}
