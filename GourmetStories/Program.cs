using GourmetStories.Models;
using GourmetStories.Services;
using GourmetStories.Services.Recipes;

var builder = WebApplication.CreateBuilder(args);
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://*:{port}");
builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.Configure<MongoDbSettings>(
        builder.Configuration.GetSection("MongoDB"));
    builder.Services.AddScoped<IRecipeService, RecipeService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddSingleton<TokenProvider>();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });

        options.AddPolicy("ProductionPolicy", builder =>
        {
            builder.WithOrigins("https://localhost", "https://localhost")
                .WithMethods("GET", "POST", "PUT", "DELETE")
                .AllowAnyHeader();
        });
    });
}

var app = builder.Build();
{
    app.UseCors("AllowAll");
    app.UseExceptionHandler("/error");
    if (app.Environment.IsDevelopment())
    {
        app.UseHttpsRedirection();
    }
    app.MapControllers();
    app.Run();
}