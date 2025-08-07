using ModelsLibrary.Infrastructure;
using PostOffice.Models.Entities;

namespace PostOffice.Infrastructure.Fabrics;
public static class AddressFabric
{
    public static string[] Streets =
    {
        "ул. Трактовая      ",
        "ул. Колхозная      ",  
        "ул. Кирова         ",
        "ул. Майская        ",
        "ул. Клубная        ",
        "ул. Родниковая     ",
        "ул. Труда          ",
        "ул. Комсомольская  ",
        "ул. Чкалова        ",
        "ул. Подгорная      ",
        "ул. Светлая        ",
        "ул. Больничная     ",
        "ул. Березовая      ",
        "ул. Кооперативная  ",
        "ул. Советская      ",
        "ул. Победы         ",
        "ул. Верхняя        ",
        "ул. Жукова         ",
        "ул. Рокоссовского  ",
        "ул. Тухачевского,  ",
        "ул. Жукова         ",
        "ул. Василевского,  ",
        "ул. Рокоссовского  ",
        "ул. Конева         ",
        "ул. Жукова         ",
        "ул. Василевского   ",
        "ул. Рокоссовского  ",
        "ул. Жукова         ",
        "ул. Рокоссовского  "
    };

    public static string[] Letters =
    {
        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
    };

    public static Address Fabric(Selection selection)
    {
        string streetName = Streets[UtilsMethods.RandomValue(0, Streets.Length - 1)];
        string character = Letters[UtilsMethods.RandomValue(0, Letters.Length - 1)];

        return new() { AddressName = $"{streetName}, {UtilsMethods.RandomValue(10, 99)}{character}, {UtilsMethods.RandomValue(1, 352)}", SelectionId = selection.Id};
    }
}
