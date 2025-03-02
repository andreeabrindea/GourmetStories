 namespace GourmetStories.Contracts.GourmetStories;
 
 public record RecipeResponse(
    Guid Id,
    string Name,
    string Author,
    string Description,
    string[] Ingredients,
    string Instructions
 );