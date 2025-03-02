using ErrorOr;

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

    internal Guid Id { get; }
    public string Name { get; set; }
    public string Author { get; }
    public string Description { get; set; }
    public string[] Ingredients { get; set; }
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