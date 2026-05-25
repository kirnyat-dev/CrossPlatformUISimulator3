using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public sealed class ApplicationTelemetrySingleton : IApplicationTelemetry
    {
        private static readonly Lazy<ApplicationTelemetrySingleton> _instance =
            new Lazy<ApplicationTelemetrySingleton>(() => new ApplicationTelemetrySingleton());

        private readonly ConcurrentDictionary<string, int> _counts = new();

        public static ApplicationTelemetrySingleton Instance => _instance.Value;

        internal ApplicationTelemetrySingleton() { }

        public void LogOperation(string category, string action, TimeSpan duration, string? metadata = null)
        {
            _counts.AddOrUpdate($"{category}:{action}", 1, (key, val) => val + 1);
        }

        public IReadOnlyDictionary<string, int> GetOperationCounts() => _counts;

        public GlobalUiSettings GetCurrentSettings() => new GlobalUiSettings("Default", true);

        public void ResetForTesting() => _counts.Clear();
    }
}
