using ConsoleApp2205.Entities;
using Shortener1.Entities;

namespace Shortener1.Repositories;

public interface IMyUserRepository 
{
    Task <List<MyUser>> GetAll();
   Task <MyUser> GetById(long id);
    Task<long> Add(MyUser entity);
    Task<bool> CheckIfEmailExists(MyUser myUserForCheck);

    Task<bool> CheckIfUserNameExist(MyUser myUser);
}