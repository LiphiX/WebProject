using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostOffice.Models.Entities;

namespace PostOffice.Models.Database.Configurations;
public class PublicationConfiguration : IEntityTypeConfiguration<Publication>
{
    public void Configure(EntityTypeBuilder<Publication> builder)
    {
        //Задание ограничений для свойств сущности издание.
        builder
            .Property(item => item.Title)
            .HasMaxLength(50)
            .IsRequired();

        //Организация отношений между таблицами.
        builder
            .HasOne(item => item.TypeOfPublication)
            .WithMany(item => item.Publications);

        builder
            .HasMany(item => item.Subscriptions)
            .WithOne(item => item.Publication);

        builder.HasData(new Publication[]
        {
            new() { Id = 1,  Title = "Официальное издание",               TypeOfPublicationID = 5, Author="Термилилкая Е.П.",                                                                                       Cost=564},
            new() { Id = 2,  Title = "Вишнёвый сад",                      TypeOfPublicationID = 1, Author="Чехов А. П.",                   ImageAddress="/images/publications/Вишнёвый сад.jpg",                    Cost=7500},
            new() { Id = 3,  Title = "Большой шлем",                      TypeOfPublicationID = 1, Author="Андреев Л. Н.",                 ImageAddress="/images/publications/Большой Шлем.jpg",                    Cost=5560},
            new() { Id = 4,  Title = "Распротранение правды",             TypeOfPublicationID = 5, Author="Геоникововский П. П.",                                                                                   Cost=1200},
            new() { Id = 5,  Title = "Сборник художественной литературы", TypeOfPublicationID = 3, Author="Термилилкая Е. П.",                                                                                      Cost=10200},
            new() { Id = 6,  Title = "Сборник",                           TypeOfPublicationID = 3, Author="Геоникововский П. П.",                                                                                   Cost=8550},
            new() { Id = 7,  Title = "Песчаная учительница",              TypeOfPublicationID = 1, Author="Платонов А. П.",                ImageAddress="/images/publications/Песчаная учительница.png",            Cost=9400},       
            new() { Id = 8,  Title = "Герой Нашего Времени",              TypeOfPublicationID = 1, Author="Лермонтов М. Ю.",               ImageAddress="/images/publications/Герой Нашего Времени.png",            Cost=7800},
            new() { Id = 9,  Title = "Война и мир",                       TypeOfPublicationID = 1, Author="Толстой Л. Н.",                 ImageAddress="/images/publications/Война и Мир.jpg",                     Cost = 5200},
            new() { Id = 10, Title = "Преступление и наказание",          TypeOfPublicationID = 1, Author="Достоевский Ф. М.",             ImageAddress="/images/publications/Преступление и Наказание.png",        Cost = 5200},
            new() { Id = 11, Title = "Тихий Дон",                         TypeOfPublicationID = 1, Author="Шолохов М. А.",                 ImageAddress="/images/publications/Тихий Дон.png",                       Cost = 5300},
            new() { Id = 12, Title = "Доктор Живаго",                     TypeOfPublicationID = 1, Author="Пастернак Б. Л.",               ImageAddress="/images/publications/Доктор Живаго.jpg",                   Cost = 5300},
            new() { Id = 13, Title = "Отцы и дети",                       TypeOfPublicationID = 1, Author="Тургенеев И. С.",               ImageAddress="/images/publications/Отцы и дети.png",                     Cost = 5100},
            new() { Id = 14, Title = "Анна Каренина",                     TypeOfPublicationID = 1, Author="Толстой Л. Н.",                 ImageAddress="/images/publications/Анна Каренина.jpg",                   Cost = 5100},
            new() { Id = 15, Title = "Мёртвые души",                      TypeOfPublicationID = 1, Author="Гоголь Н. В.",                  ImageAddress="/images/publications/Мёртвые души.jpg",                    Cost = 5200},
            new() { Id = 16, Title = "Евгений Онегин",                    TypeOfPublicationID = 1, Author="Пушкин А. С.",                  ImageAddress="/images/publications/Евгений Онегин.jpg",                  Cost = 3200},
            new() { Id = 17, Title = "Двенадцать стульев",                TypeOfPublicationID = 1, Author="Илья Ильф, Евгения Петрова",    ImageAddress="/images/publications/Двенадцать стульев.jpg",              Cost = 5200},
            new() { Id = 18, Title = "Идиот",                             TypeOfPublicationID = 1, Author="Достоевский Ф. М.",             ImageAddress="/images/publications/Произведение Идиот.png",              Cost = 6300},
            new() { Id = 19, Title = "Капитанская дочка",                 TypeOfPublicationID = 1, Author="Каревин В. А.",                 ImageAddress="/images/publications/Два Капитана.jpg",                    Cost = 7300},
            new() { Id = 20, Title = "Ревизор",                           TypeOfPublicationID = 1, Author="Гоголь Н. В.",                  ImageAddress="/images/publications/Ревизор.jpg",                         Cost = 2300},
			new() { Id = 21, Title = "Революция и её причины",            TypeOfPublicationID = 2,                                                                                                                  Cost=5500},
			new() { Id = 22, Title = "Исследования",                      TypeOfPublicationID = 2,                                                                                                                  Cost=12000},
			new() { Id = 23, Title = "Рассказы о правде",                 TypeOfPublicationID = 5,                                                                                                                  Cost=7000},
			new() { Id = 24, Title = "Ложь и истинна",                    TypeOfPublicationID = 4,                                                                                                                  Cost=8540},
			new() { Id = 25, Title = "Убеждённость",                      TypeOfPublicationID = 4,                                                                                                                  Cost=3400},
			new() { Id = 26, Title = "Ведение предприятием",              TypeOfPublicationID = 2,                                                                                                                  Cost=7500},
		});
    }
}
