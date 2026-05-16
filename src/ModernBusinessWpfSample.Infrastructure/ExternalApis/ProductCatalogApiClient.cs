using ModernBusinessWpfSample.Application.Abstractions;

namespace ModernBusinessWpfSample.Infrastructure.ExternalApis;

internal sealed class ProductCatalogApiClient(HttpClient httpClient) : IProductCatalogApiClient
{
    public async Task<string> GetApiHealthTextAsync(CancellationToken cancellationToken)
    {
        try
        {
            using var response = await httpClient.GetAsync("todos/1", cancellationToken);
            return response.IsSuccessStatusCode ? "External API: OK" : $"External API: {(int)response.StatusCode}";
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException)
        {
            return "External API: Offline";
        }
    }
}
