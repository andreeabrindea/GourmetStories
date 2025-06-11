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

        public static Error EmptyUsername => Error.Validation(
            code: "User.EmptyUsername",
            description: "Username cannot be empty"
        );

        public static Error UsernameAlreadyExists => Error.Conflict(
        code: "User.UsernameAlreadyExists",
        description: "Username already exists");

        public static Error EmailAlreadyRegisterd => Error.Conflict(
        code: "User.EmailAlreadyRegisterd",
        description: "This email is already in use.");

        public static Error InvalidEmail => Error.Validation(
        code: "User.InvalidEmail",
        description: "This email is invalid");

        public static Error EmailNotFound => Error.Validation(
            code: "User.EmailNotFound",
            description: "This email is not registered."
        );

    }
}