using GourmetStories.Models;
using GourmetStories.Services;
using GourmetStories.Services.Recipes;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.Configure<MongoDbSettings>(
        builder.Configuration.GetSection("MongoDB"));
    builder.Services.AddScoped<IRecipeService, RecipeService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
        {
            builder.AllowAnyOrigin()   // Allow requests from any origin
                .AllowAnyMethod()    // Allow any HTTP method
                .AllowAnyHeader();   // Allow any headers
        });

        // Alternatively, for more restricted access:
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
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}