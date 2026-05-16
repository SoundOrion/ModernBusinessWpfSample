using System.Text.Json;

namespace ModernBusinessWpfSample.Infrastructure.Persistence;

internal sealed class JsonFileStore
{
    private readonly string _dataDirectory;
    private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

    public JsonFileStore()
    {
        _dataDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "ModernBusinessWpfSample");
        Directory.CreateDirectory(_dataDirectory);
    }

    public async Task<List<T>> ReadAsync<T>(string fileName, Func<List<T>> seedFactory, CancellationToken cancellationToken)
    {
        var path = Path.Combine(_dataDirectory, fileName);
        if (!File.Exists(path))
        {
            var seed = seedFactory();
            await WriteAsync(fileName, seed, cancellationToken);
            return seed;
        }

        await using var stream = File.OpenRead(path);
        return await JsonSerializer.DeserializeAsync<List<T>>(stream, _options, cancellationToken) ?? [];
    }

    public async Task WriteAsync<T>(string fileName, List<T> data, CancellationToken cancellationToken)
    {
        var path = Path.Combine(_dataDirectory, fileName);
        await using var stream = File.Create(path);
        await JsonSerializer.SerializeAsync(stream, data, _options, cancellationToken);
    }

    public string DataDirectory => _dataDirectory;
}
