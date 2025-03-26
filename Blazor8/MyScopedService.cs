using System.Diagnostics;

namespace Blazor8;

public class MyScopedService : IDisposable
{
    public MyScopedService()
    {
        Console.WriteLine($"Blazor8 {Activity.Current?.Id} MyScopedService.ctor()");
    }

    public void Dispose()
    {
        Console.WriteLine($"Blazor8 {Activity.Current?.Id} MyScopedService.Dispose()");
    }
}