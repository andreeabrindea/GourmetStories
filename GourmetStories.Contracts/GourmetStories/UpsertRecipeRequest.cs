 namespace GourmetStories.Contracts.GourmetStories;
 
 public record UpsertRecipeRequest(
    string Name,
    string Author,
    string Description,
    string[] Ingredients,
    string Instructions
 );