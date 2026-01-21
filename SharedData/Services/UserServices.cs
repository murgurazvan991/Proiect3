using Microsoft.EntityFrameworkCore;

namespace SharedData; 

public class UserServices
{
    private readonly AppDbContext _db;

    public UserServices(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _db.Users.ToListAsync();
    }

    public async Task AddUserAsync(User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
    }

    public async Task RemoveUserAsync(int userId)
    {
        var user = await _db.Users.FindAsync(userId);
        if (user != null)
        {
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }
    }

    public async Task ChangePasswordAsync(int userId, string newPasswordHash)
    {
        var user = await _db.Users.FindAsync(userId);
        if (user != null)
        {
            user.PasswordHash = newPasswordHash;
            await _db.SaveChangesAsync();
        }
    }
}