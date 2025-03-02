using ErrorOr;
namespace GourmetStories.Models;

public class User
{
    internal Guid Id { get; }
    internal string Name { get; }
    internal string Username { get; }
    internal string Password { get; }
    internal string Email { get; }

    public static ErrorOr<User> Create(string name, string username, string password, string email, Guid? id = null)
    => new User(id ?? Guid.NewGuid(), name, username, password, email);
    private User(Guid id, string name, string username, string password, string email)
    {
        Id = id;
        Name = name;
        Username = username;
        Password = password;
        Email = email;
    }
}