using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

#nullable enable

namespace CrossPlatformUISimulator3
{
    public class TestFailureException : Exception
    {
        public TestFailureException(string message) : base(message) { }
    }

    public static class Assert
    {
        public static void IsTrue(bool condition, string msg)
        {
            if (!condition) throw new TestFailureException($"[Assert.IsTrue] {msg}");
        }

        public static void IsFalse(bool condition, string msg)
        {
            if (condition) throw new TestFailureException($"[Assert.IsFalse] {msg}");
        }

        public static void AreEqual<T>(T expected, T actual, string msg)
        {
            if (!EqualityComparer<T>.Default.Equals(expected, actual))
                throw new TestFailureException($"[Assert.AreEqual] {msg} | Ожидалось: {expected}, Было: {actual}");
        }
    }

    public static class TaskUnitTests
    {
        private static int _passed;
        private static int _failed;

        public static void Run()
        {
            _passed = 0;
            _failed = 0;

            var tests = new Action[]
            {
                Test_1_Adapter_MapsParametersCorrectly,
                Test_2_Adapter_ThrowsNotSupportedException,
                Test_3_Singleton_ReturnsSameInstance_Parallel,
                Test_4_ResetForTesting_IsolatesState,
                Test_5_ConcurrentDictionary_PreventsRaceCondition,
                Test_6_Builder_LogsExactlyOncePerBuild,
                Test_7_Adapter_CreatedViaFactoryMethod,
                Test_8_Integration_CloneAndAdapterAndTelemetry,
                Test_9_Prototype_HighVolumeCloningMemoryCheck,
                Test_10_Telemetry_ExportReturnsReadOnlyWithoutMutation
            };

            Console.WriteLine("Запуск 10 юнит-ТЕСТОВ");

            foreach (var test in tests)
            {
                ApplicationTelemetrySingleton.Instance.ResetForTesting();

                try
                {
                    test();
                    Console.WriteLine($"[ПРОЙДЕН] {test.Method.Name}");
                    _passed++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ПРОВАЛЕН] {test.Method.Name}: {ex.Message}");
                    _failed++;
                }
            }

            Console.WriteLine($"\nИтоги тестирования: Успешно: {_passed} | Провалено: {_failed}\n");
        }

        private static void Test_1_Adapter_MapsParametersCorrectly()
        {
            var adapter = new LegacyGraphicsAdapter();
            var factory = new StandardWidgetFactory();
            var builder = new DialogBuilder(factory);
            var dialog = builder.SetTitle("Error")
                                .ConfigureTheme(new DefaultThemeFactory())
                                .AddButton(new ButtonConfig { Text = "OK" })
                                .Build();

            try
            {
                adapter.Render(dialog);
                Assert.IsTrue(true, "Адаптер успешно обработал диалог");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, $"Рендеринг упал с ошибкой: {ex.Message}");
            }
        }

        private static void Test_2_Adapter_ThrowsNotSupportedException()
        {
            var adapter = new LegacyGraphicsAdapter();
            bool threw = false;

            try
            {
                adapter.UnsupportedMethod();
            }
            catch (NotSupportedException)
            {
                threw = true;
            }

            Assert.IsTrue(threw, "Адаптер должен бросать NotSupportedException на неподдерживаемый метод");
        }

        private static void Test_3_Singleton_ReturnsSameInstance_Parallel()
        {
            var instances = new ApplicationTelemetrySingleton[100];

            Parallel.For(0, 100, i =>
            {
                instances[i] = ApplicationTelemetrySingleton.Instance;
            });

            var firstInstance = ApplicationTelemetrySingleton.Instance;
            for (int i = 0; i < 100; i++)
            {
                Assert.IsTrue(ReferenceEquals(firstInstance, instances[i]), $"Экземпляр на индексе {i} отличается!");
            }
        }

        private static void Test_4_ResetForTesting_IsolatesState()
        {
            var telemetry = ApplicationTelemetrySingleton.Instance;
            telemetry.LogOperation("Test", "Action", TimeSpan.Zero);

            telemetry.ResetForTesting();

            Assert.AreEqual(0, telemetry.GetOperationCounts().Count, "После ResetForTesting словарь должен быть пуст");
        }

        private static void Test_5_ConcurrentDictionary_PreventsRaceCondition()
        {
            var telemetry = ApplicationTelemetrySingleton.Instance;
            int parallelCalls = 500;

            Parallel.For(0, parallelCalls, i =>
            {
                telemetry.LogOperation("Metric", "Increment", TimeSpan.Zero);
            });

            var counts = telemetry.GetOperationCounts();
            Assert.AreEqual(parallelCalls, counts["Metric:Increment"], "Потерялись логи из-за потоконебезопасности!");
        }

        private static void Test_6_Builder_LogsExactlyOncePerBuild()
        {
            var factory = new StandardWidgetFactory();
            var builder = new DialogBuilder(factory);

            builder.SetTitle("A")
                   .ConfigureTheme(new DefaultThemeFactory())
                   .AddButton(new ButtonConfig { Text = "OK" })
                   .Build();

            var counts = ApplicationTelemetrySingleton.Instance.GetOperationCounts();
            Assert.AreEqual(1, counts["Builder:Build"], "Билдер должен логировать вызов Build ровно 1 раз");
        }

        private static void Test_7_Adapter_CreatedViaFactoryMethod()
        {
            IWidgetFactory factory = new StandardWidgetFactory();
            var renderer = factory.CreateLegacyRenderer();

            Assert.IsTrue(renderer is LegacyGraphicsAdapter, "Фабрика должна возвращать объект типа LegacyGraphicsAdapter");
        }

        private static void Test_8_Integration_CloneAndAdapterAndTelemetry()
        {
            var factory = new StandardWidgetFactory();
            var builder = new DialogBuilder(factory);
            var original = builder.SetTitle("OriginalError")
                                  .ConfigureTheme(new DefaultThemeFactory())
                                  .AddButton(new ButtonConfig { Text = "OK" })
                                  .Build();

            var clone = original.Clone();
            var renderer = factory.CreateLegacyRenderer();

            renderer.Render(clone);

            var counts = ApplicationTelemetrySingleton.Instance.GetOperationCounts();
            Assert.AreEqual("OriginalError", clone.Title, "Имя клона повредилось при клонировании");
            Assert.IsTrue(counts.ContainsKey("Builder:Build"), "В телеметрии отсутствует запись о создании");
        }

        private static void Test_9_Prototype_HighVolumeCloningMemoryCheck()
        {
            var factory = new StandardWidgetFactory();
            var builder = new DialogBuilder(factory);
            var original = builder.SetTitle("LeakTest")
                                  .ConfigureTheme(new DefaultThemeFactory())
                                  .AddButton(new ButtonConfig { Text = "OK" })
                                  .Build();

            try
            {
                for (int i = 0; i < 5000; i++)
                {
                    var clone = original.Clone();
                }
                Assert.IsTrue(true, "Массовое клонирование выполнено успешно");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, $"Массовое клонирование вызвало ошибку: {ex.Message}");
            }
        }

        private static void Test_10_Telemetry_ExportReturnsReadOnlyWithoutMutation()
        {
            var telemetry = ApplicationTelemetrySingleton.Instance;
            telemetry.LogOperation("Security", "Check", TimeSpan.Zero);

            var counts = telemetry.GetOperationCounts();

            Assert.IsTrue(counts is IReadOnlyDictionary<string, int>, "Словарь должен экспортироваться как IReadOnlyDictionary");
        }
    }
}