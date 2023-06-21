using Microsoft.EntityFrameworkCore;
using Shortener1.Cofigs;
using Shortener1.Entities;

namespace Shortener1.Repositories;

public class MyUserRepositoryImpl : IMyUserRepository
{
    private readonly DataContext _context;

    public MyUserRepositoryImpl(DataContext context)
    {
        _context = context;
    }

    public async Task<List<MyUser>> GetAll()
    {
        return await _context.Set<MyUser>().ToListAsync();
    }

    public async Task<MyUser> GetById(long id)
    {
        var result = await _context.MyUser.FirstOrDefaultAsync(x => x.Id == id);

        if (result == null)
        {
            return null;
        }

        return result;
    }

    public async Task<long> Add(MyUser entity)
    {
        var result = await _context.Set<MyUser>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return result.Entity.Id;
    }

    public async Task<bool> CheckIfEmailExists(MyUser myUserForCheck)
    {
        return await _context.MyUser.AnyAsync(myUser => myUser.Email == myUserForCheck.Email);
    }

    public Task<bool> CheckIfUserNameExist(MyUser myUserForCheck)
    {
        return _context.MyUser.AnyAsync(myUser => myUser.Username == myUserForCheck.Username);
    }
}