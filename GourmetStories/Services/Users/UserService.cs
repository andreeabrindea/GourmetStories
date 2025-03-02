using ErrorOr;
using GourmetStories.Models;
using GourmetStories.ServiceErrors;

namespace GourmetStories.Services;

public class UserService : IUserService
{
    private static readonly Dictionary<Guid, User> _users = new();
    public ErrorOr<Created> CreateUser(User user)
    {
        _users.Add(user.Id, user);
        return Result.Created;
    }

    public ErrorOr<User> GetUser(Guid id)
    {
        if (_users.TryGetValue(id, out var user))
        {
            return user;
        }

        return Errors.User.NotFound;
    }

    public ErrorOr<Updated> UpsertUser(User user)
    {
        _users[user.Id] = user;
        return Result.Updated;
    }

    public ErrorOr<Deleted> DeleteUser(Guid id)
    {
        _users.Remove(id);
        return Result.Deleted;
    }
}