using Shortener1.DTO;
using Shortener1.Entities;

namespace Shortener1.Helpers;

public static class UserMapper
{
    public static MyUser MapToMyUser(MyUserDtO userDtO)
    {
        return new MyUser()
        {
            FirstName = userDtO.FirstName,
            LastName = userDtO.LastName,
            Username = userDtO.Username,
            Email = userDtO.Email,
            Password = HashGenerator.GenerateHash(userDtO.Password).Password,
            Patronymic = userDtO.Patronymic
        };
    }
}