using NexaStore.Domain.Enums;

namespace NexaStore.Domain.Entities;

public class Order : BaseEntity
{
    private readonly List<OrderItem> _items = new();

    public Guid CustomerId { get; private set; }

    public DateTime OrderDate { get; private set; }

    public OrderStatus Status { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public decimal TotalAmount =>
        _items.Sum(item => item.LineTotal);

    // Constructor
    public Order(Guid customerId)
    {
        if (customerId == Guid.Empty)
            throw new ArgumentException("Customer ID is required.");

        CustomerId = customerId;
        OrderDate = DateTime.UtcNow;
        Status = OrderStatus.Pending;
    }

    // Private constructor for EF Core
    private Order()
    {
    }

    // Add product to order
    public void AddItem(Product product, int quantity)
    {
        EnsurePending();

        if (product == null)
            throw new ArgumentNullException(nameof(product));

        if (product.IsDeleted)
            throw new InvalidOperationException(
                "Deleted product cannot be ordered.");

        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity));

        if (quantity > product.StockQuantity)
            throw new InvalidOperationException(
                "Insufficient stock.");

        if (_items.Any(item => item.ProductId == product.Id))
            throw new InvalidOperationException(
                "Product already exists in this order.");

        var orderItem = new OrderItem(
            product.Id,
            product.Name,
            product.Price,
            quantity
        );

        _items.Add(orderItem);

        MarkAsUpdated();
    }

    // Mark order as paid
    public void MarkAsPaid()
    {
        EnsurePending();

        if (_items.Count == 0)
            throw new InvalidOperationException(
                "Cannot pay for an empty order.");

        Status = OrderStatus.Paid;
        MarkAsUpdated();
    }

    // Start processing
    public void StartProcessing()
    {
        EnsureStatus(OrderStatus.Paid);

        Status = OrderStatus.Processing;
        MarkAsUpdated();
    }

    // Ship order
    public void Ship()
    {
        EnsureStatus(OrderStatus.Processing);

        Status = OrderStatus.Shipped;
        MarkAsUpdated();
    }

    // Deliver order
    public void Deliver()
    {
        EnsureStatus(OrderStatus.Shipped);

        Status = OrderStatus.Delivered;
        MarkAsUpdated();
    }

    // Cancel order
    public void Cancel()
    {
        if (Status != OrderStatus.Pending &&
            Status != OrderStatus.Paid &&
            Status != OrderStatus.Processing)
        {
            throw new InvalidOperationException(
                "Order cannot be cancelled in its current status.");
        }

        Status = OrderStatus.Cancelled;
        MarkAsUpdated();
    }

    private void EnsurePending()
    {
        EnsureStatus(OrderStatus.Pending);
    }

    private void EnsureStatus(OrderStatus expectedStatus)
    {
        if (Status != expectedStatus)
        {
            throw new InvalidOperationException(
                $"Order must be in {expectedStatus} status.");
        }
    }
}