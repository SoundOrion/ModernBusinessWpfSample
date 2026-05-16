namespace ModernBusinessWpfSample.Application.Abstractions;

public interface IProductCatalogApiClient
{
    Task<string> GetApiHealthTextAsync(CancellationToken cancellationToken);
}
