using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostOffice.Models.Entities.Users;

namespace PostOffice.Models.Configurations;
class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
{
    public void Configure(EntityTypeBuilder<UserAccount> builder)
    {
        //Задание ограничений для сущности класса UserAccount.
        builder
            .Property(item => item.Password)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(item => item.Login)
            .HasMaxLength(50)
            .IsRequired();

        //Задание ограничения на уникальность логинов.
        builder
            .HasIndex(item => item.Login)
            .IsUnique();

        //Организация отношений между таблицами.
        builder
            .HasOne(item => item.Person)
            .WithMany(item => item.Accounts);

        builder.HasData(new UserAccount[]
        {
            new() { Id = 1, Login = "OperatorLoginFirst", Password = "Passowrd_Login_Par1", PersonId = 1},
            new() { Id = 2, Login = "OperatorLoginSecond", Password = "Passowrd_Login_Par2", PersonId = 2},
            new() { Id = 3, Login = "Operator3", Password = "Passowrd_Login_Par3", PersonId = 3},
            new() { Id = 4, Login = "ElenaLogin", Password = "Passowrd_Login_Par4", PersonId = 4},
            new() { Id = 5, Login = "DirectorLogin", Password = "DirectorPassword", PersonId = 5},
            new() { Id = 6, Login = "ElenaAccountLogin", Password = "PassowrdAccount_Login_Par6", PersonId = 6},
            new() { Id = 7, Login = "OperatorAnnaLogin", Password = "PassowrdOperator_Login_Par7", PersonId = 7},
            new() { Id = 8, Login = "OperatorLoginF", Password = "Passowrd_Login_Par8", PersonId = 8},
            new() { Id = 9, Login = "OperatorLoginS", Password = "Passowrd_Login_Par9", PersonId = 9},
            new() { Id = 10, Login = "OperatorLoginOther", Password = "Passowrd_Login_Par10", PersonId = 10},
            new() { Id = 11, Login = "OperatorLogin", Password = "Passowrd_Login_Par11", PersonId = 11},
            new() { Id = 12, Login = "OperatorLogin12", Password = "Passowrd_Login_Par12", PersonId = 12},
        });
    }
}
