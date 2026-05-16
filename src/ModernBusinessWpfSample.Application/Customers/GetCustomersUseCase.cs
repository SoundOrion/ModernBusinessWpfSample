using ModernBusinessWpfSample.Application.Abstractions;

namespace ModernBusinessWpfSample.Application.Customers;

public sealed class GetCustomersUseCase(ICustomerRepository repository)
{
    public async Task<IReadOnlyList<CustomerDto>> ExecuteAsync(CancellationToken cancellationToken)
    {
        var customers = await repository.GetAllAsync(cancellationToken);
        return customers
            .OrderBy(x => x.Code)
            .Select(x => new CustomerDto(x.Id, x.Code, x.Name, x.Email, x.Segment, x.IsActive))
            .ToList();
    }
}
