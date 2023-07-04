StaticLogger.EnsureInitialized();
Log.Information("Server Booting Up...");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.EnableLogger();
    builder.ConfigureHasherOptions();

    builder.Services.AddControllersWithViews();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Services.RegisterServices();

    var app = builder.Build();

    await app.Services.InitializeDatabase();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseRouting();
    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapDefaultControllerRoute();
    app.MapControllers();

    app.Run();
}
catch (Exception ex) when (!ex.GetType().Name.Equals("StopTheHostException", StringComparison.Ordinal))
{
    StaticLogger.EnsureInitialized();
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    StaticLogger.EnsureInitialized();
    Log.Information("Server Shutting down...");
    Log.CloseAndFlush();
}
