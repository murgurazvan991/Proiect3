using SharedData;
using Microsoft.EntityFrameworkCore;

namespace Frontend;

// Note: Inherit from TabbedPage now!
public partial class MainPage : TabbedPage
{
    private readonly ProductServices _productServices;
    private readonly UserServices _userServices;
    private readonly SaleServices _saleServices;
    private readonly AppDbContext _db;

    // Inject ALL services
    public MainPage(ProductServices productServices, UserServices userServices, SaleServices saleServices, AppDbContext db)
    {
        InitializeComponent();
        _productServices = productServices;
        _userServices = userServices;
        _saleServices = saleServices;
        _db = db;

        _db.Database.EnsureDeleted();

        _db.Database.EnsureCreated();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadProducts();
        await LoadUsers();
    }

    // --- PRODUCT LOGIC ---
    private async Task LoadProducts()
    {
        ProductsList.ItemsSource = await _productServices.GetAllAsync(null, null);
    }

    private async void OnAddProductClicked(object sender, EventArgs e)
    {
        // Add a demo product
        if (!_db.Categories.Any()) 
        {
            _db.Categories.Add(new Category { Name = "General" });
            await _db.SaveChangesAsync();
        }
        
        var cat = await _db.Categories.FirstAsync();
        await _productServices.AddProductAsync(new Product 
        { 
            Name = "New Item " + DateTime.Now.Second, 
            Price = 50, 
            Category = cat, 
            CategoryId = cat.Id 
        });

        await LoadProducts();
    }

    // --- USER LOGIC ---
    private async Task LoadUsers()
    {
        UsersList.ItemsSource = await _userServices.GetAllUsersAsync();
    }

    private async void OnAddUserClicked(object sender, EventArgs e)
    {
        await _userServices.AddUserAsync(new User
        {
            FullName = "Admin " + DateTime.Now.Second,
            Role = "Administrator",
            IsActive = true,
            PasswordHash = "secret",
            PasswordSalt = "salt"
        });
        
        await LoadUsers();
    }

    // --- SALES LOGIC ---
    private async void OnGenerateReportClicked(object sender, EventArgs e)
    {
        // Generate a text report using your SaleServices
        string report = await _saleServices.GetSalesReportAsync();
        ReportLabel.Text = report;
    }
}