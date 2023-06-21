using System.ComponentModel.DataAnnotations;

namespace Shortener1.DTO;

public class AuthenticateRequest
{
    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; set; }
}