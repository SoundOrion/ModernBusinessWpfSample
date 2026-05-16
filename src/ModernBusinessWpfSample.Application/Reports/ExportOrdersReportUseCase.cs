using ModernBusinessWpfSample.Application.Abstractions;

namespace ModernBusinessWpfSample.Application.Reports;

public sealed class ExportOrdersReportUseCase(IReportExporter exporter)
{
    public Task<string> ExecuteAsync(CancellationToken cancellationToken)
        => exporter.ExportOrdersCsvAsync(cancellationToken);
}
