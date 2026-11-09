namespace NexaStore.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    // Constructor
    public Category(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name is required.");

        Name = name.Trim();
        Description = description?.Trim() ?? string.Empty;
    }

    // Private constructor for EF Core
    private Category()
    {
    }

    // Update category information
    public void UpdateDetails(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name is required.");

        Name = name.Trim();
        Description = description?.Trim() ?? string.Empty;

        MarkAsUpdated();
    }
}