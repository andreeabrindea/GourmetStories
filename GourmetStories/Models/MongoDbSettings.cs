namespace GourmetStories.Models;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string RecipesCollectionName { get; set; } = null!;
    public string UsersCollectionName { get; set; } = null!;
}