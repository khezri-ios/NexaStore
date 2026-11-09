namespace NexaStore.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public decimal Price { get; private set; }

    public int StockQuantity { get; private set; }

    public Guid CategoryId { get; private set; }

    public bool IsDeleted { get; private set; }

    // Constructor
    public Product(
        string name,
        string description,
        decimal price,
        int stockQuantity,
        Guid categoryId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name is required.");

        if (price <= 0)
            throw new ArgumentOutOfRangeException(nameof(price));

        if (stockQuantity < 0)
            throw new ArgumentOutOfRangeException(nameof(stockQuantity));

        if (categoryId == Guid.Empty)
            throw new ArgumentException("Category ID is required.");

        Name = name.Trim();
        Description = description?.Trim() ?? string.Empty;
        Price = price;
        StockQuantity = stockQuantity;
        CategoryId = categoryId;
    }

    // Private constructor for EF Core
    private Product()
    {
    }

    // Update product price
    public void UpdatePrice(decimal newPrice)
    {
        EnsureNotDeleted();

        if (newPrice <= 0)
            throw new ArgumentOutOfRangeException(nameof(newPrice));

        Price = newPrice;
        MarkAsUpdated();
    }

    // Increase stock
    public void IncreaseStock(int quantity)
    {
        EnsureNotDeleted();

        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity));

        StockQuantity = checked(StockQuantity + quantity);
        MarkAsUpdated();
    }

    // Decrease stock
    public void DecreaseStock(int quantity)
    {
        EnsureNotDeleted();

        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity));

        if (quantity > StockQuantity)
            throw new InvalidOperationException(
                "Insufficient stock.");

        StockQuantity -= quantity;
        MarkAsUpdated();
    }

    // Soft delete
    public void SoftDelete()
    {
        if (IsDeleted)
            return;

        IsDeleted = true;
        MarkAsUpdated();
    }

    // Check product status
    private void EnsureNotDeleted()
    {
        if (IsDeleted)
            throw new InvalidOperationException(
                "Deleted product cannot be modified.");
    }
}