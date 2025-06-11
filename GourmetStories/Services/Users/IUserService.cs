using GourmetStories.Models;
using ErrorOr;
namespace GourmetStories.Services;

public interface IUserService
{
    ErrorOr<Created> CreateUser(User user);
    ErrorOr<User> GetUser(Guid id);
    ErrorOr<User> GetUserByEmail(string email);
    ErrorOr<UpsertUserResult> UpsertUser(User user);
    ErrorOr<Deleted> DeleteUser(Guid id);
    List<User> GetAllUsers();

}