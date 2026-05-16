using ModernBusinessWpfSample.Application.Abstractions;
using ModernBusinessWpfSample.Domain.Customers;

namespace ModernBusinessWpfSample.Infrastructure.Persistence;

internal sealed class JsonCustomerRepository(JsonFileStore store) : ICustomerRepository
{
    private const string FileName = "customers.json";

    public async Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken cancellationToken)
        => await store.ReadAsync(FileName, Seed, cancellationToken);

    public async Task AddAsync(Customer customer, CancellationToken cancellationToken)
    {
        var customers = await store.ReadAsync(FileName, Seed, cancellationToken);
        customers.Add(customer);
        await store.WriteAsync(FileName, customers, cancellationToken);
    }

    private static List<Customer> Seed() =>
    [
        new() { Id = Guid.NewGuid(), Code = "C0001", Name = "Northwind Trading", Email = "buyer@northwind.example", Segment = "Enterprise", IsActive = true },
        new() { Id = Guid.NewGuid(), Code = "C0002", Name = "Contoso Retail", Email = "sales@contoso.example", Segment = "Standard", IsActive = true },
        new() { Id = Guid.NewGuid(), Code = "C0003", Name = "Fabrikam Works", Email = "ops@fabrikam.example", Segment = "Premium", IsActive = true }
    ];
}
