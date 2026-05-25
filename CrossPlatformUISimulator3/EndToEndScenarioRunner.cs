using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public class EndToEndScenarioRunner
    {
        public void Run()
        {
            var telemetry = ApplicationTelemetrySingleton.Instance;
            var factory = new StandardWidgetFactory();
            var builder = new DialogBuilder(factory);
            var director = new ErrorDialogDirector();

            var dialog = director.Construct(builder);
            var renderer = factory.CreateLegacyRenderer();
            renderer.Render(dialog);

            Console.WriteLine($"Операций: {telemetry.GetOperationCounts().Count}");
        }
    }
}