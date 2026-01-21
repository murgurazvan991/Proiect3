namespace SharedData;
public class UserServices
{
    private readonly AppDbContext _db;

    public UserServices(AppDbContext db)
    {
        _db = db;
    }

    public void AddUser(User user)
    {
        _db.Users.Add(user);
        _db.SaveChanges();
        Console.WriteLine($"User with id {user.Id} added successfully.");
    }

    public void RemoveUser(int userId)
    {
        var user = _db.Users.Find(userId);
        if (user != null)
        {
            _db.Users.Remove(user);
            _db.SaveChanges();
            Console.WriteLine($"User with id {userId} removed successfully.");
        }
        else
        {
            Console.WriteLine($"User with id {userId} not found.");
        }
    }

    public void ChangePassword(int userId, string newPasswordHash)
    {
        var user = _db.Users.Find(userId);
        if (user != null)
        {
            user.PasswordHash = newPasswordHash;
            _db.SaveChanges();
            Console.WriteLine($"Password for user with id {userId} changed successfully.");
        }
        else
        {
            Console.WriteLine($"User with id {userId} not found.");
        }
    }

    public void SetUserActiveStatus(int userId, bool isActive)
    {
        var user = _db.Users.Find(userId);
        if (user != null)
        {
            user.IsActive = isActive;
            _db.SaveChanges();
            Console.WriteLine($"User with id {userId} active status set to {isActive}.");
        }
        else
        {
            Console.WriteLine($"User with id {userId} not found.");
        }
    }

    public void GetPasswordHash(int userId)
    {
        var user = _db.Users.Find(userId);
        if (user != null)
        {
            Console.WriteLine($"Password hash for user with id {userId} is {user.PasswordHash}.");
        }
        else
        {
            Console.WriteLine($"User with id {userId} not found.");
        }
    }

    public void UpdateUserData(int userId, string fullName, string role)
    {
        var user = _db.Users.Find(userId);
        if (user != null)
        {
            user.FullName = fullName;
            user.Role = role;
            _db.SaveChanges();
            Console.WriteLine($"User data for user with id {userId} updated successfully.");
        }
        else
        {
            Console.WriteLine($"User with id {userId} not found.");
        }
    }
}