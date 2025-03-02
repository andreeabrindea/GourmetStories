namespace GourmetStories.Contracts.GourmetStories;

public record UpsertUserRequest
(
    string Name,
    string Username,
    string Password,
    string Email
);