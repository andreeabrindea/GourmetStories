namespace GourmetStories.Contracts.GourmetStories;

public record CreateUserRequest
(
    string Username,
    string Password,
    string Email
);