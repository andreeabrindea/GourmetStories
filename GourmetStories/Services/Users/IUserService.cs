using GourmetStories.Models;
using ErrorOr;
namespace GourmetStories.Services;

public interface IUserService
{
    ErrorOr<Created> CreateUser(User user);
    ErrorOr<User> GetUser(Guid id);
    ErrorOr<Updated> UpsertUser(User user);
    ErrorOr<Deleted> DeleteUser(Guid id);
}