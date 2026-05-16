using ModernBusinessWpfSample.Domain.Common;

namespace ModernBusinessWpfSample.Infrastructure;

internal sealed class SystemClock : IClock
{
    public DateTime Now => DateTime.Now;
}
