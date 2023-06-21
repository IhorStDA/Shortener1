using Shortener1.DTO;
using Shortener1.Entities;
using Shortener1.Helpers;
using Shortener1.Repositories;

namespace Shortener1.Services;

public class MyUserServiceImpl : IMyUserService
{
    private readonly IMyUserRepository _userRepository;
    private readonly IConfiguration _configuration;


    public MyUserServiceImpl(IMyUserRepository userRepository,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }


    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
    {
        var user = _userRepository
            .GetAll()
            .Result
            .FirstOrDefault(x =>
                x.Username == model.Username && x.Password == HashGenerator.GenerateHash(model.Password).Password);

        if (user == null)
        {
            return null;
        }

        var token = _configuration.GenerateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }


    public async Task<AuthenticateResponse> Register(MyUserDtO userModel)
    {
        var userEntity = UserMapper.MapToMyUser(userModel);
        await _userRepository.Add(userEntity);
        var response = await Authenticate(new AuthenticateRequest
        {
            Username = userModel.Username,
            Password = userModel.Password
        });
        return response;
    }


    public async Task<MyUser> GetById(int id)
    {
        return await _userRepository.GetById(id);
    }

    public async Task<(bool IsDuplicateUserName, bool IsDuplicateEmail)> CheckForDuplicates(MyUserDtO userModel)
    {
        return (await _userRepository.CheckIfUserNameExist(UserMapper.MapToMyUser(userModel)),
            await _userRepository.CheckIfEmailExists(UserMapper.MapToMyUser(userModel)));
    }
}