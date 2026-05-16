namespace ModernBusinessWpfSample.Domain.Customers;

public sealed class Customer
{
    public Guid Id { get; init; }
    public string Code { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Segment { get; init; } = "Standard";
    public bool IsActive { get; init; } = true;
}
