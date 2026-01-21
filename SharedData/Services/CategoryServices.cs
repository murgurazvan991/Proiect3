namespace SharedData;
public class CategoryServices
{
    private readonly AppDbContext _db;

    public CategoryServices(AppDbContext db)
    {
        _db = db;
    }

    public void AddCategory(Category category)
    {
        _db.Categories.Add(category);
        _db.SaveChanges();
        Console.WriteLine($"Category with id {category.Id} added successfully.");
    }

    public void RemoveCategory(int categoryId)
    {
        var category = _db.Categories.Find(categoryId);
        if (category != null)
        {
            _db.Categories.Remove(category);
            _db.SaveChanges();
            Console.WriteLine($"Category with id {categoryId} removed successfully.");
        }
        else
        {
            Console.WriteLine($"Category with id {categoryId} not found.");
        }
    }

    public void UpdateCategory(int id, string newName)
    {
        var category = _db.Categories.Find(id);
        if (category != null)
        {
            category.Name = newName;

            _db.SaveChanges();
            Console.WriteLine($"Category with id {id} updated successfully.");
        }
        else
        {
            Console.WriteLine($"Category with id {id} not found.");
        }
    }

}