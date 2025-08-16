using Microsoft.EntityFrameworkCore;
using PostOffice.Models.Database.Configurations;
using PostOffice.Models.Entities.Sections;
using PostOffice.Models.Entities.Users;

namespace PostOffice.Models.Entities.User;
//Класс, который описывает структуру данных для зарегистрированных в приложении людей.
[EntityTypeConfiguration(typeof(PersonConfiguration))]
public class Person
{
    public int Id { get; set; }

    //Фамилия человека.
    public string Surname { get; set; }

    //Имя человека.
    public string Name { get; set; }

    //Отчество человека.
    public string Patronymic { get; set; }

    public string FullName => $"{Surname} {Name[0]}. {Patronymic[0]}.";

    //Текущая роль определяемого человека.
    //public int RoleId { get; set; }
    public virtual Roles Role { get; set; } = Roles.Guest;

    //Коллекция учётных записей, 1:M.
    public virtual List<UserAccount> Accounts { get; set; }

    //Список подписок определяемого человека.
    //public virtual List<Subscription> Subscriptions { get; set; }

    //public int SubscriberId { get; set; }
    public virtual Subscriber Subscriber { get; set; }

    public virtual List<Section> Selections { get; set; }
}
