using System.Diagnostics;

namespace Blazor9;

public class MyScopedService : IDisposable
{
    public MyScopedService()
    {
        Console.WriteLine($"Blazor9 {Activity.Current?.Id} MyScopedService.ctor()");
    }

    public void Dispose()
    {
        Console.WriteLine($"Blazor9 {Activity.Current?.Id} MyScopedService.Dispose()");
    }
}