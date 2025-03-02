using ErrorOr;
using GourmetStories.Models;
using GourmetStories.ServiceErrors;

namespace GourmetStories.Services.Recipes;

public class RecipeService : IRecipeService
{
    private static readonly Dictionary<Guid, Recipe> _recipes = new();
    public ErrorOr<Created> CreateRecipe(Recipe recipe)
    {
        _recipes.Add(recipe.Id, recipe);
        return Result.Created;
    }

    public ErrorOr<Recipe> GetRecipe(Guid id)
    {
        if (_recipes.TryGetValue(id, out var recipe))
        {
            return recipe;
        }

        return Errors.Recipe.NotFound;
    }

    public ErrorOr<UpsertRecipeResult> UpsertRecipe(Recipe recipe)
    {
        bool isNewlyCreated = !_recipes.ContainsKey(recipe.Id);
        _recipes[recipe.Id] = recipe;
        return new UpsertRecipeResult(isNewlyCreated);
    }

    public ErrorOr<Deleted> DeleteRecipe(Guid id)
    {
        _recipes.Remove(id);
        return Result.Deleted;
    }
}