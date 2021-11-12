using Serilog;
using Serilog.Events;

var webAppBuilder = WebApplication.CreateBuilder(args);

// Add services to the container.
webAppBuilder.Services.AddControllersWithViews();

var configBuilder = new ConfigurationBuilder()
                .SetBasePath(webAppBuilder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{webAppBuilder.Environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(configBuilder)
    .CreateLogger();

try
{
    Log.Information("Application starting up");

    webAppBuilder.Host.UseSerilog();

    // Add services to the container.
    webAppBuilder.Services.AddRazorPages();

    var app = webAppBuilder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application startup failed");
}
finally
{
    Log.CloseAndFlush();
}