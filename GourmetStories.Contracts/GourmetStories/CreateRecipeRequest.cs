 namespace GourmetStories.Contracts.GourmetStories;
 
 public record CreateRecipeRequest(
    string Name,
    string Author,
    string Description,
    string[] Ingredients,
    string Instructions
 );