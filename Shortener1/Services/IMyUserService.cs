using Shortener1.DTO;
using Shortener1.Entities;

namespace Shortener1.Services;

public interface IMyUserService
{
    Task <AuthenticateResponse> Authenticate(AuthenticateRequest model);
    Task<AuthenticateResponse> Register(MyUserDtO userModel);

    Task <MyUser> GetById(int id);
    Task<(bool IsDuplicateUserName, bool IsDuplicateEmail)> CheckForDuplicates(MyUserDtO userModel);
}