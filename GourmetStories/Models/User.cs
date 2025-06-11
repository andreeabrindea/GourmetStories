using ErrorOr;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GourmetStories.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    [BsonElement("Username")]
    public string Username { get; set;}
    [BsonElement("Password")]
    public string Password { get; set; }
    [BsonElement("Email")]
    public string Email { get; set; }

    public static ErrorOr<User> Create(string username, string password, string email, Guid? id = null)
    => new User(id ?? Guid.NewGuid(), username, password, email);
    private User(Guid id, string username, string password, string email)
    {
        Id = id;
        Username = username;
        Password = password;
        Email = email;
    }
}