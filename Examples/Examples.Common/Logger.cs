using Microsoft.Extensions.Logging;

namespace Examples.Common;

public static class Logger
{
    public static ILogger New<T>(LogLevel level=LogLevel.Information)
    {
        return LoggerFactory.Create(c =>
            {
                c.SetMinimumLevel(level);
                c.AddConsole();
            })
            .CreateLogger<T>();
    }
}