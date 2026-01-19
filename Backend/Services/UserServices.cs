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

    //DE VAZUT dupa ce aflam care-i treab cu autentificarea
    //public void ChangePassword()

    //Same
    //public void UpdateUserData(int userId)
}