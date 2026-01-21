using SharedData;   
using Microsoft.EntityFrameworkCore;

namespace Frontend;

public partial class MainPage : ContentPage
{
    private readonly ProductServices _productServices;
    private readonly AppDbContext _db;

    
    public MainPage(ProductServices productServices, AppDbContext db)
    {
        InitializeComponent();
        _productServices = productServices;
        _db = db;

        _db.Database.EnsureCreated();

        LoadData();
    }

    // defined as 'async void' so we can await inside it without blocking the UI
    private async void LoadData()
    {
        // 1. Ask the service for all products (awaiting the Task)
        var products = await _productServices.GetAllAsync(null, null);
        
        // 2. Put the list into the CollectionView so the user sees it
        ProductsList.ItemsSource = products;
    }

    private async void OnLoadProductsClicked(object sender, EventArgs e)
    {
        int count = await _db.Products.CountAsync();
        if (count == 0)
        {
            var category = new Category { Name = "Demo Category" };
            _db.Categories.Add(category);
            
            await _productServices.AddProductAsync(new Product 
            { 
                Name = "Maui Product", 
                Price = 10.99f, 
                StockQuantity = 100,
                Category = category,
                CategoryId = category.Id
            });
        }


        LoadData();
    }
}
