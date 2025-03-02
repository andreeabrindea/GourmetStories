using ErrorOr;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GourmetStories.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    internal Guid Id { get; }
    [BsonElement("Name")]
    internal string Name { get; }
    [BsonElement("Username")]
    internal string Username { get; }
    [BsonElement("Password")]
    internal string Password { get; }
    [BsonElement("Email")]
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