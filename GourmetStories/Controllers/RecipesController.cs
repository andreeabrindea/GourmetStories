using Microsoft.AspNetCore.Mvc;
using GourmetStories.Contracts.GourmetStories;
using GourmetStories.Models;
using GourmetStories.Services.Recipes;
using ErrorOr;
using Microsoft.AspNetCore.Cors;

namespace GourmetStories.Controllers;

[EnableCors("AllowAll")] 
public class RecipesController : ApiController
{
    private readonly IRecipeService _recipeService;

    // dependency injection
    public RecipesController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    [HttpPost]
    public IActionResult CreateRecipe(CreateRecipeRequest request)
    {
        var recipe = Recipe.Create(
            request.Name,
            request.Author,
            request.Description,
            request.Ingredients,
            request.Instructions,
            Guid.NewGuid()
        );

        if (recipe.IsError)
        {
            return Problem(recipe.Errors);
        }
        var createRecipeResult = _recipeService.CreateRecipe(recipe.Value);
        return createRecipeResult.Match(
            _ => CreatedNewRecipe(recipe.Value),
            Problem);
    }
    
    [HttpGet("{id:guid}")]
    public IActionResult GetRecipe(Guid id)
    {
        ErrorOr<Recipe> getRecipeResult = _recipeService.GetRecipe(id);
        return getRecipeResult.Match(
            recipe => Ok(MapRecipeResponse(recipe)),
            Problem);
    }

    [HttpGet]
    public List<Recipe> GetAllRecipes() => _recipeService.GetAllRecipes();

    [HttpPut("{id:guid}")]
    public IActionResult UpsertRecipe(Guid id, UpsertRecipeRequest request)
    {
        var recipe = Recipe.Create(
            request.Name,
            request.Author,
            request.Description,
            request.Ingredients,
            request.Instructions,
            id
        );

        if (recipe.IsError)
        {
            return Problem(recipe.Errors);
        }
        ErrorOr<UpsertRecipeResult> updateRecipeResult = _recipeService.UpsertRecipe(recipe.Value);
        return updateRecipeResult.Match(
            updated => updated.isNewlyCreated ? CreatedNewRecipe(recipe.Value) : NoContent(),
            Problem);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteRecipe(Guid id)
    {
        ErrorOr<Deleted> deleteRecipeResult = _recipeService.DeleteRecipe(id);
        return deleteRecipeResult.Match(
            _ => NoContent(),
            Problem);
    }

    private static Recipe MapRecipeResponse(Recipe recipe)
    {
        return Recipe.Create(
            recipe.Name,
            recipe.Author,
            recipe.Description,
            recipe.Ingredients,
            recipe.Instructions,
            recipe.Id
        ).Value;
    }

    private CreatedAtActionResult CreatedNewRecipe(Recipe recipe)
    {
        return CreatedAtAction(
            actionName: nameof(GetRecipe),
            routeValues: new { id = recipe.Id },
            value: MapRecipeResponse(recipe)
        );
    }
}