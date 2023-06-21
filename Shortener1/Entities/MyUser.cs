using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shortener1.Entities;

public class MyUser
{
    public MyUser(string firstName, string lastName, string patronymic, string username, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
        Username = username;
        Email = email;
        Password = password;
    }
    public MyUser()
    {
    }
    [Key] public int Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    [JsonIgnore] public string Password { get; set; }
}