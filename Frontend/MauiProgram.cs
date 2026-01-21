using Microsoft.Extensions.Logging;
using SharedData;

namespace Frontend;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		string dbPath = Path.Combine(FileSystem.AppDataDirectory, "store.db");
        
        builder.Services.AddTransient<AppDbContext>(s => new AppDbContext(dbPath));

        // Register Services (Now verified to be in SharedData)
        builder.Services.AddTransient<ProductServices>();
        builder.Services.AddTransient<CategoryServices>();
		builder.Services.AddTransient<SaleServices>();	
		builder.Services.AddTransient<UserServices>();
		builder.Services.AddTransient<MainPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
