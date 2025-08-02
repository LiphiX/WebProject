using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostOffice.Models.Entities;

namespace PostOffice.Models.Database.Configurations;
public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        //Задание ограничений для свойств сущности класса Person.
        builder
            .Property(item => item.Surname)
            .HasMaxLength(20)
            .IsRequired();

        builder
            .Property(item => item.Name)
            .HasMaxLength(20)
            .IsRequired();

        builder
            .Property(item => item.Patronymic)
            .HasMaxLength(20)
            .IsRequired();

        //Организация отношений между свойствами таблиц.
        //builder
        //    .HasOne(item => item.Role)
        //    .WithMany(item => item.People);

        builder
            .HasMany(item => item.Accounts)
            .WithOne(item => item.Person);

        builder
            .HasData(new Person[]
            {
                new() {Id = 1, Surname = "Термилилкая",     Name = "Елена",     Patronymic = "Павловна", Role = Roles.PostalOperator},
                new() {Id = 2, Surname = "Геоникововский",  Name = "Павел",     Patronymic = "Петрович", Role = Roles.PostalOperator},
                new() {Id = 3, Surname = "Фелимовскиов",    Name = "Александр", Patronymic = "Павлович", Role = Roles.PostalOperator},
                new() {Id = 4, Surname = "Ниоленовоивская", Name = "Елена",     Patronymic = "Петровна", Role = Roles.PostalOperator},
                new() {Id = 5, Surname = "Гриневивец",      Name = "Пётр",      Patronymic = "Петрович", Role = Roles.Director},
                new() {Id = 6, Surname = "Екнивнаовская",   Name = "Елена",     Patronymic = "Павловна", Role = Roles.PostalOperator},
                new() {Id = 7, Surname = "Енивсаякова",     Name = "Анна",      Patronymic = "Викторовна", Role = Roles.PostalOperator},
                new() {Id = 8, Surname = "Мавоновикский",   Name = "Пётр",      Patronymic = "Александрович", Role = Roles.PostalOperator},
                new() {Id = 9, Surname = "Оникивовна",      Name = "Елена",     Patronymic = "Викторовна", Role = Roles.PostalOperator},
                new() {Id = 10, Surname = "Неовонсовский",   Name = "Виктор",    Patronymic = "Павлович", Role = Roles.PostalOperator},
                new() {Id = 11, Surname = "Еивиновская",     Name = "Елена",     Patronymic = "Петровна", Role = Roles.PostalOperator},
                new() {Id = 12, Surname = "Елеиковский",     Name = "Павел",     Patronymic = "Павлович", Role = Roles.PostalOperator},
            });
    }
}
