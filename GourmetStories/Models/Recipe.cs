using ErrorOr;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GourmetStories.Models;

public class Recipe
{
    private Recipe(Guid id, string name, string author, string description, string[] ingredients, string instructions)
    {
        Id = id;
        Name = name;
        Author = author;
        Description = description;
        Ingredients = ingredients;
        Instructions = instructions;
    }

    [BsonId]
    [BsonRepresentation(BsonType.String)]
    internal Guid Id { get; }
    
    [BsonElement("Name")]
    public string Name { get; set; }
    
    [BsonElement("Author")]
    public string Author { get; }
    
    [BsonElement("Description")]
    public string Description { get; set; }
    
    [BsonElement("Ingredients")]
    public string[] Ingredients { get; set; }
    
    [BsonElement("Instructions")]
    public string Instructions { get; set; }

    public static ErrorOr<Recipe> Create(string name, string author, string description, string[] ingredients, string instructions, Guid? id)
    {
        return new Recipe(
            id ?? Guid.NewGuid(),
            name,
            author,
            description,
            ingredients,
            instructions);
    }
}