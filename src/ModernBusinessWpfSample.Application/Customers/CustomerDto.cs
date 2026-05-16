namespace ModernBusinessWpfSample.Application.Customers;

public sealed record CustomerDto(Guid Id, string Code, string Name, string Email, string Segment, bool IsActive);
