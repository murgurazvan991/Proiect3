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
    }

    private void OnLoadProductsClicked(object sender, EventArgs e)
    {
        if (!_db.Products.Any())
        {
            var category = new Category { Name = "Demo Category" };
            _db.Categories.Add(category);
            
            _productServices.AddProduct(new Product 
            { 
                Name = "Maui Product", 
                Price = 10.99f, 
                StockQuantity = 100,
                Category = category,
                CategoryId = category.Id
            });
        }


        var products = _db.Products.ToList();
        
        ProductsList.ItemsSource = products;
    }
}
