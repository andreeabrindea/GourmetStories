using GourmetStories.Models;
using ErrorOr;
using GourmetStories.ServiceErrors;

namespace GourmetStories.Services.Recipes;

public interface IRecipeService 
{
    ErrorOr<Created> CreateRecipe(Recipe recipe);
    ErrorOr<Recipe> GetRecipe(Guid id);
    ErrorOr<UpsertRecipeResult> UpsertRecipe(Recipe recipe);
    ErrorOr<Deleted> DeleteRecipe(Guid id);
    List<Recipe> GetAllRecipes();
}