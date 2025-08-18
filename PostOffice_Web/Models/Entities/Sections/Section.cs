using Microsoft.EntityFrameworkCore;
using PostOffice.Models.Database.Configurations;
using PostOffice.Models.Entities.Users;

namespace PostOffice.Models.Entities.Sections;
//Класс, который описывает структуру данных для участков, которые обслуживаются назначенными почтальонами.
[EntityTypeConfiguration(typeof(SectionConfiguration))]
public class Section
{
    public int Id { get; set; }

    //Почтальон, назначенный на определяемый участок.
    public int? PostmanId { get; set; }
    public virtual Person? Postman { get; set; }

    //Адреса, которые входят в определяемый участок.
    public virtual List<Address> Addresses { get; set; }
}
