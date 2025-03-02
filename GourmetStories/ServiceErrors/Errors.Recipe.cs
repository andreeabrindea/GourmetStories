using ErrorOr;

namespace GourmetStories.ServiceErrors;

public static class Errors
{
    public static class Recipe
    {
        public static Error NotFound => Error.NotFound(
            code: "Recipe.NotFound",
            description: "Recipe not found");
        
        public static Error InvalidAuthorNameFormat => Error.Validation(
            code: "Recipe.InvalidAuthorName",
            description: "Invalid Author Name");
    }

    public static class User
    {
        public static Error NotFound => Error.NotFound(
            code: "User.NotFound",
            description: "User not found");
    }
}