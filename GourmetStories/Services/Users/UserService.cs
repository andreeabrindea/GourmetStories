using ErrorOr;
using GourmetStories.Models;
using GourmetStories.ServiceErrors;
using GourmetStories.Services.Recipes;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Net.Mail;

namespace GourmetStories.Services;

public class UserService : IUserService
{
    private readonly IMongoCollection<User> _usersCollection;
    public UserService(IOptions<MongoDbSettings> mongoDbSettings)
    {
        var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _usersCollection = mongoDatabase.GetCollection<User>(mongoDbSettings.Value.UsersCollectionName);
    }

    public ErrorOr<Created> CreateUser(User user)
    {
        if (string.IsNullOrEmpty(user.Username))
        {
            return Errors.User.EmptyUsername;
        }
        
        bool existsUsername = _usersCollection.Find(u => u.Username == user.Username).FirstOrDefault() != null;
        if (existsUsername)
        {
            return Errors.User.UsernameAlreadyExists;
        }

        bool existsEmail = _usersCollection.Find(u => u.Email == user.Email).FirstOrDefault() != null;

        if (existsEmail)
        {
            return Errors.User.EmailAlreadyRegisterd;
        }

        if (!IsEmailValid(user.Email))
        {
            return Errors.User.InvalidEmail;
        }

        _usersCollection.InsertOne(user);
        return Result.Created;
    }

    public ErrorOr<User> GetUser(Guid id)
    {
        User user = _usersCollection.Find(u => u.Id == id).FirstOrDefault();
        if (user == null)
        {
            return Errors.User.NotFound;
        }

        return user;
    }

    public ErrorOr<UpsertUserResult> UpsertUser(User user)
    {
        User existingUser = _usersCollection.Find(u => u.Id == user.Id).FirstOrDefault();
        bool isNewlyCreated = existingUser == null;
        if (isNewlyCreated)
        {
            _usersCollection.InsertOne(existingUser);
        }
        else
        {
            _usersCollection.ReplaceOne(u => u.Id == user.Id, user);
        }

        return new UpsertUserResult(isNewlyCreated);
    }

    public ErrorOr<Deleted> DeleteUser(Guid id)
    {
        User existingUser = _usersCollection.Find(u => u.Id == id).FirstOrDefault();
        if (existingUser == null)
        {
            return Errors.User.NotFound;
        }

        _usersCollection.DeleteOne(u => u.Id == id);
        return Result.Deleted;
    }
    private static bool IsEmailValid(string email)
    {

        try
        {
            MailAddress address = new MailAddress(email);
            return address.Address == email;

        }
        catch (FormatException)
        {
            return false;
        }
    }
}