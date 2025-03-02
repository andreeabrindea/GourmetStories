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
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}