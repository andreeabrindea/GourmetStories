namespace GourmetStories.Contracts.GourmetStories;

public record CreateUserRequest
(
    string Name,
    string Username,
    string Password,
    string Email
);