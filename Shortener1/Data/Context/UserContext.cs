using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shortener1.Entities;

namespace Shortener1.Data.Context;

public class UserContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
    }
}