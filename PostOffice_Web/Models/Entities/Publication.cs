using Microsoft.EntityFrameworkCore;
using PostOffice.Models.Database.Configurations;

namespace PostOffice.Models.Entities;

[EntityTypeConfiguration(typeof(PublicationConfiguration))]
//Класс, который описывает структуру данных для подписных изданий.
public class Publication
{
    public int Id { get; set; }

    //Наименование издания.
    public string Title { get; set; }

    //Автор издания.
    public string? Author { get; set; }

    //Стоимость подписки.
    public int Cost { get; set; }

    public string? ImageAddress { get; set; }

    //Тип издания.
    public int TypeOfPublicationID { get; set; }
    public virtual TypesOfPublication TypeOfPublication { get; set; }

    //Список подписок на определяемое издание.
    public virtual List<Subscription> Subscriptions { get; set; }
}
