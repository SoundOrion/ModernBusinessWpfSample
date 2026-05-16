namespace ModernBusinessWpfSample.Domain.Orders;

public sealed class Order
{
    public Guid Id { get; init; }
    public string OrderNo { get; init; } = string.Empty;
    public Guid CustomerId { get; init; }
    public string CustomerName { get; init; } = string.Empty;
    public DateTime OrderedAt { get; init; }
    public decimal Amount { get; init; }
    public OrderStatus Status { get; init; }
}
