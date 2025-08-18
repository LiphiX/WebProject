using Microsoft.EntityFrameworkCore;
using PostOffice.Models.Configurations;
using PostOffice.Models.Entities.Users;

namespace PostOffice.Models.Entities.Users;
//Класс, который описывает структуру данных для учётной записи пользователя.
[EntityTypeConfiguration(typeof(UserAccountConfiguration))]
public class UserAccount
{
    public int Id { get; set; }

    //Имя пользователя, которое задаётся для входа в учётную запись.
    public string Login { get; set; }

    //Пароль опеределяемой учётной записи.
    public string Password { get; set; }

    //Данные о человеке, на которого зарегистрирована учётная запись.
    public int PersonId { get; set; }
    public virtual Person Person { get; set; }

    //Навигационное свойство сессии.
    public virtual Session Session { get; set; }
}
