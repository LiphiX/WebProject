using Microsoft.EntityFrameworkCore;
using PostOffice.Infrastructure;
using PostOffice.Models.Entities;
using PostOffice.Models.Entities.Sections;
using PostOffice.Models.Entities.Users;
using PostOffice.Models.Entities.Users;
using PostOffice.Models.Services;

namespace PostOffice.Models.Database;
public class PostOfficeContext : DbContext
{
    public DbSet<Subscription> Subscriptions => Set<Subscription>();

    public DbSet<UserAccount> UserAccounts => Set<UserAccount>();

    //public DbSet<Role> Roles => Set<Role>();

    public DbSet<Person> People => Set<Person>();

    public DbSet<Section> Sections => Set<Section>();
    
    public DbSet<Address> Addresses => Set<Address>();

    public DbSet<Subscriber> Subscribers => Set<Subscriber>();

    public DbSet<Publication> Publications => Set<Publication>();

    public DbSet<TypesOfPublication> TypesOfPublications => Set<TypesOfPublication>();

    public DbSet<Session> Sessions => Set<Session>();


    //Метод, который устанавливает соединение с базой данных.
    public PostOfficeContext(DbContextOptions<PostOfficeContext> options) : base(options)
    {
        //Database.EnsureDeleted();
        //Database.EnsureCreated();

        //var service = new DatabaseService(this);
        //service.SourceInitialization();
    }
}
