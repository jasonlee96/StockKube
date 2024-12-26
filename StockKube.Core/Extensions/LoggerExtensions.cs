using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StockKube.Core.Extensions
{
    public static class LoggerExtensions
    {
        public static void Log(this ILogger logger, Exception ex)
        {
            var methodInfo = new StackTrace()?.GetFrame(1)?.GetMethod();
            var className = methodInfo?.ReflectedType?.Name ?? "UNKNOWN";
            var stringBuilder = $"[{className}][{ex.Message}][{ex.StackTrace}]";
            logger.LogError(ex, stringBuilder);
        }
        public static void Log(this ILogger logger, string message)
        {
            var methodInfo = new StackTrace()?.GetFrame(1)?.GetMethod();
            var className = methodInfo?.ReflectedType?.Name ?? "UNKNOWN";
            var stringBuilder = $"[{className}][{message}]";
            logger.LogInformation(stringBuilder);
        }
    }
}
