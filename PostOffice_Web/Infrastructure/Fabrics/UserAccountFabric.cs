using System.Text;

using ModelsLibrary.Infrastructure;
using PostOffice.Models.Entities.Users;

namespace PostOffice.Infrastructure.Fabrics;
public static class UserAccountFabric
{
    public static char[] Letters =
    {
        'A', 'B', 'C', 'D',
        'E', 'F', 'G', 'H',
        'I', 'J', 'K', 'L',
        'M', 'N', 'O', 'P',
        'Q', 'R', 'S', 'T',
        'U', 'V', 'W', 'X',
        'Y', 'Z',
    };

    public static string Generate()
    {
        int number = UtilsMethods.RandomValue(1, 50);

        StringBuilder stringBuilder = new StringBuilder();
        for(int i = 0; i < number; i++)
            stringBuilder.Append(Letters[UtilsMethods.RandomValue(0, Letters.Length - 1)]);

        return stringBuilder.ToString();
    }
    
    public static UserAccount Fabric(int personId)
    {
        var login = Generate();
        var password = Generate();

        return new() { Login = login, Password = password, PersonId = personId };
    }
}
