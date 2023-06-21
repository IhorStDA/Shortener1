using ConsoleApp2205.Entities;
using Shortener1.Entities;

namespace Shortener1.DTO;

public class AuthenticateResponse
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }

    public AuthenticateResponse(MyUser  user, string token)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Patronymic = user.Patronymic;
        Username = user.Username;
        Email = user.Email;
        Token = token;
    }
}