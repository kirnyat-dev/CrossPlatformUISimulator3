using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformUISimulator3
{
    public class UICenarioRunner
    {
        public static void Run()
        {
            Console.WriteLine("Сквозная интеграция (Builder + Prototype + Adapter + Singleton)\n");

            var telemetry = ApplicationTelemetrySingleton.Instance;
            var factory = new StandardWidgetFactory();
            var builder = new DialogBuilder(factory);
            var director = new ErrorDialogDirector();

            var dialog = director.Construct(builder);
            Console.WriteLine($"Создан диалог: {dialog.Title}");

            var renderer = factory.CreateLegacyRenderer();
            renderer.Render(dialog);

            var clone = dialog.Clone();
            Console.WriteLine($"Клон успешно создан: {clone.Title}");

            Console.WriteLine("\nИтоговые метрики");
            foreach (var metric in telemetry.GetOperationCounts())
            {
                Console.WriteLine($"{metric.Key}: {metric.Value}");
            }

            RunBenchmark(factory);
        }

        private static void RunBenchmark(IWidgetFactory factory)
        {
            int iterations = 1000;
            var telemetry = ApplicationTelemetrySingleton.Instance;
            telemetry.ResetForTesting();

            for (int i = 0; i < iterations; i++)
            {
                var builder = new DialogBuilder(factory);
                builder.SetTitle("Test").ConfigureTheme(new DefaultThemeFactory()).AddButton(new ButtonConfig());
                builder.Build();
            }
            int factoryCalls = telemetry.GetOperationCounts()["Factory:CreateButton"];

            telemetry.ResetForTesting();
            var b = new DialogBuilder(factory);
            b.SetTitle("Test").ConfigureTheme(new DefaultThemeFactory()).AddButton(new ButtonConfig());
            var prototype = b.Build();
            for (int i = 0; i < iterations - 1; i++) prototype.Clone();

            int prototypeCalls = telemetry.GetOperationCounts().ContainsKey("Factory:CreateButton")
                                 ? telemetry.GetOperationCounts()["Factory:CreateButton"] : 0;

            Console.WriteLine($"\nБенчмарк: вызовы фабрики сокращены с {factoryCalls} до {prototypeCalls}");
        }
    }
}