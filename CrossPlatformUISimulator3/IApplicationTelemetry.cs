using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public record GlobalUiSettings(string ThemeName, bool EnableAnimations);

    public interface IApplicationTelemetry
    {
        void LogOperation(string category, string action, TimeSpan duration, string? metadata = null);
        IReadOnlyDictionary<string, int> GetOperationCounts();
        GlobalUiSettings GetCurrentSettings();
        void ResetForTesting();
    }
}