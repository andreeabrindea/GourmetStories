using ErrorOr;
using GourmetStories.Models;
using GourmetStories.ServiceErrors;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GourmetStories.Services.Recipes;

public class RecipeService : IRecipeService
{
    private readonly IMongoCollection<Recipe> _recipesCollection;
    public RecipeService(IOptions<MongoDbSettings> mongoDbSettings)
    {
        var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _recipesCollection = mongoDatabase.GetCollection<Recipe>(mongoDbSettings.Value.RecipesCollectionName);
    }

    public ErrorOr<Created> CreateRecipe(Recipe recipe)
    {
        _recipesCollection.InsertOne(recipe);
        return Result.Created;
    }

    public ErrorOr<Recipe> GetRecipe(Guid id)
    {
        var recipe = _recipesCollection.Find(r => r.Id == id).FirstOrDefault();
        if (recipe is null)
        {
            return Errors.Recipe.NotFound;
        }
        return recipe;
    }

    public ErrorOr<UpsertRecipeResult> UpsertRecipe(Recipe recipe)
    {
        var existingRecipe = _recipesCollection.Find(r => r.Id == recipe.Id).FirstOrDefault();
        bool isNewlyCreated = existingRecipe == null;
        
        if (isNewlyCreated)
        {
            _recipesCollection.InsertOne(recipe);
        }
        else
        {
            _recipesCollection.ReplaceOne(r => r.Id == recipe.Id, recipe);
        }
        
        return new UpsertRecipeResult(isNewlyCreated);
    }

    public ErrorOr<Deleted> DeleteRecipe(Guid id)
    {
        var recipe = _recipesCollection.Find(r => r.Id == id).FirstOrDefault();
        if (recipe == null)
        {
            return Errors.Recipe.NotFound;
        }
        
        _recipesCollection.DeleteOne(r => r.Id == id);
        return Result.Deleted;
    }
}