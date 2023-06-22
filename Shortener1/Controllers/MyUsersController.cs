using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shortener1.DTO;
using Shortener1.Entities;
using Shortener1.Helpers;

namespace Shortener1.Controllers;

[ApiController]
[Route("[controller]")]
public class MyUsersController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;

    public MyUsersController(UserManager<ApplicationUser> userManager,
                             SignInManager<ApplicationUser> signInManager,
                             IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(AuthenticateRequest model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user == null)
        {
            return BadRequest(new { message = ResponseMessages.AuthenticateErrorIncorrectCredentials });
        }
        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            return BadRequest(new { message = ResponseMessages.AuthenticateErrorIncorrectCredentials });
        }
        var token = _configuration.GenerateJwtToken(user);
        return Ok(token);
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register(MyUserDtO userModel)
    {
        var user = new ApplicationUser
        {
            UserName = userModel.Username,
            Email = userModel.Email,
            FirstName = userModel.FirstName,
            LastName = userModel.LastName,
            Address = userModel.Address
        };
        var result = await _userManager.CreateAsync(user, userModel.Password);
        if (!result.Succeeded)
        {
            return BadRequest(new { message = ResponseMessages.RegisterErrorRegistrationFailed });
        }

        var token = _configuration.GenerateJwtToken(user);
        return Ok(token);
    }
}