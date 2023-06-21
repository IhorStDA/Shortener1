using Microsoft.AspNetCore.Mvc;
using Shortener1.DTO;
using Shortener1.Services;

namespace Shortener1.Controllers;
[ApiController]
[Route("[controller]")]

public class MyUsersController : ControllerBase
{
    private readonly IMyUserService _userService;
    
    public MyUsersController(IMyUserService userService)
    {
        _userService = userService;
    }
    
    
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(AuthenticateRequest model)
    {
        var response = await _userService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = ResponseMessages.AuthenticateErrorIncorrectCredentials });

        return Ok(response);
    }
    
    
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(MyUserDtO userModel)
    {
        if (_userService.CheckForDuplicates(userModel).Result.IsDuplicateEmail)
        {
            return BadRequest(new {message = ResponseMessages.RegisterErrorEmailAlreadyExists});
        }
        if (_userService.CheckForDuplicates(userModel).Result.IsDuplicateUserName)
        {
            return BadRequest(new {message = ResponseMessages.RegisterErrorUserNameAlreadyExists});
        }
        
        var response = await _userService.Register(userModel);

        if (response == null)
        {
            return BadRequest(new {message = ResponseMessages.RegisterErrorRegistrationFailed});
        }

        return Ok(response);
    }
 
    
}