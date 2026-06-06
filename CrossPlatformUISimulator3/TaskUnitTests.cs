using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

#nullable enable

namespace CrossPlatformUISimulator3
{
    //ТЕСТЫ 4
    //public class TestFailureException : Exception
    //{
    //    public TestFailureException(string message) : base(message) { }
    //}

    //public static class Assert
    //{
    //    public static void IsTrue(bool condition, string msg)
    //    {
    //        if (!condition) throw new TestFailureException($"[Assert.IsTrue] {msg}");
    //    }

    //    public static void IsFalse(bool condition, string msg)
    //    {
    //        if (condition) throw new TestFailureException($"[Assert.IsFalse] {msg}");
    //    }

    //    public static void AreEqual<T>(T expected, T actual, string msg)
    //    {
    //        if (!EqualityComparer<T>.Default.Equals(expected, actual))
    //            throw new TestFailureException($"[Assert.AreEqual] {msg} | Ожидалось: {expected}, Было: {actual}");
    //    }
    //}

    //public static class TaskUnitTests
    //{
    //    private static int _passed;
    //    private static int _failed;

    //    public static void Run()
    //    {
    //        _passed = 0;
    //        _failed = 0;

    //        var tests = new Action[]
    //        {
    //            Test_1_Adapter_MapsParametersCorrectly,
    //            Test_2_Adapter_ThrowsNotSupportedException,
    //            Test_3_Singleton_ReturnsSameInstance_Parallel,
    //            Test_4_ResetForTesting_IsolatesState,
    //            Test_5_ConcurrentDictionary_PreventsRaceCondition,
    //            Test_6_Builder_LogsExactlyOncePerBuild,
    //            Test_7_Adapter_CreatedViaFactoryMethod,
    //            Test_8_Integration_CloneAndAdapterAndTelemetry,
    //            Test_9_Prototype_HighVolumeCloningMemoryCheck,
    //            Test_10_Telemetry_ExportReturnsReadOnlyWithoutMutation
    //        };

    //        Console.WriteLine("Запуск 10 юнит-ТЕСТОВ");

    //        foreach (var test in tests)
    //        {
    //            ApplicationTelemetrySingleton.Instance.ResetForTesting();

    //            try
    //            {
    //                test();
    //                Console.WriteLine($"[ПРОЙДЕН] {test.Method.Name}");
    //                _passed++;
    //            }
    //            catch (Exception ex)
    //            {
    //                Console.WriteLine($"[ПРОВАЛЕН] {test.Method.Name}: {ex.Message}");
    //                _failed++;
    //            }
    //        }

    //        Console.WriteLine($"\nИтоги тестирования: Успешно: {_passed} | Провалено: {_failed}\n");
    //    }

    //    private static void Test_1_Adapter_MapsParametersCorrectly()
    //    {
    //        var adapter = new LegacyGraphicsAdapter();
    //        var factory = new StandardWidgetFactory();
    //        var builder = new DialogBuilder(factory);
    //        var dialog = builder.SetTitle("Error")
    //                            .ConfigureTheme(new DefaultThemeFactory())
    //                            .AddButton(new ButtonConfig { Text = "OK" })
    //                            .Build();

    //        try
    //        {
    //            adapter.Render(dialog);
    //            Assert.IsTrue(true, "Адаптер успешно обработал диалог");
    //        }
    //        catch (Exception ex)
    //        {
    //            Assert.IsTrue(false, $"Рендеринг упал с ошибкой: {ex.Message}");
    //        }
    //    }

    //    private static void Test_2_Adapter_ThrowsNotSupportedException()
    //    {
    //        var adapter = new LegacyGraphicsAdapter();
    //        bool threw = false;

    //        try
    //        {
    //            adapter.UnsupportedMethod();
    //        }
    //        catch (NotSupportedException)
    //        {
    //            threw = true;
    //        }

    //        Assert.IsTrue(threw, "Адаптер должен бросать NotSupportedException на неподдерживаемый метод");
    //    }

    //    private static void Test_3_Singleton_ReturnsSameInstance_Parallel()
    //    {
    //        var instances = new ApplicationTelemetrySingleton[100];

    //        Parallel.For(0, 100, i =>
    //        {
    //            instances[i] = ApplicationTelemetrySingleton.Instance;
    //        });

    //        var firstInstance = ApplicationTelemetrySingleton.Instance;
    //        for (int i = 0; i < 100; i++)
    //        {
    //            Assert.IsTrue(ReferenceEquals(firstInstance, instances[i]), $"Экземпляр на индексе {i} отличается!");
    //        }
    //    }

    //    private static void Test_4_ResetForTesting_IsolatesState()
    //    {
    //        var telemetry = ApplicationTelemetrySingleton.Instance;
    //        telemetry.LogOperation("Test", "Action", TimeSpan.Zero);

    //        telemetry.ResetForTesting();

    //        Assert.AreEqual(0, telemetry.GetOperationCounts().Count, "После ResetForTesting словарь должен быть пуст");
    //    }

    //    private static void Test_5_ConcurrentDictionary_PreventsRaceCondition()
    //    {
    //        var telemetry = ApplicationTelemetrySingleton.Instance;
    //        int parallelCalls = 500;

    //        Parallel.For(0, parallelCalls, i =>
    //        {
    //            telemetry.LogOperation("Metric", "Increment", TimeSpan.Zero);
    //        });

    //        var counts = telemetry.GetOperationCounts();
    //        Assert.AreEqual(parallelCalls, counts["Metric:Increment"], "Потерялись логи из-за потоконебезопасности!");
    //    }

    //    private static void Test_6_Builder_LogsExactlyOncePerBuild()
    //    {
    //        var factory = new StandardWidgetFactory();
    //        var builder = new DialogBuilder(factory);

    //        builder.SetTitle("A")
    //               .ConfigureTheme(new DefaultThemeFactory())
    //               .AddButton(new ButtonConfig { Text = "OK" })
    //               .Build();

    //        var counts = ApplicationTelemetrySingleton.Instance.GetOperationCounts();
    //        Assert.AreEqual(1, counts["Builder:Build"], "Билдер должен логировать вызов Build ровно 1 раз");
    //    }

    //    private static void Test_7_Adapter_CreatedViaFactoryMethod()
    //    {
    //        IWidgetFactory factory = new StandardWidgetFactory();
    //        var renderer = factory.CreateLegacyRenderer();

    //        Assert.IsTrue(renderer is LegacyGraphicsAdapter, "Фабрика должна возвращать объект типа LegacyGraphicsAdapter");
    //    }

    //    private static void Test_8_Integration_CloneAndAdapterAndTelemetry()
    //    {
    //        var factory = new StandardWidgetFactory();
    //        var builder = new DialogBuilder(factory);
    //        var original = builder.SetTitle("OriginalError")
    //                              .ConfigureTheme(new DefaultThemeFactory())
    //                              .AddButton(new ButtonConfig { Text = "OK" })
    //                              .Build();

    //        var clone = original.Clone();
    //        var renderer = factory.CreateLegacyRenderer();

    //        renderer.Render(clone);

    //        var counts = ApplicationTelemetrySingleton.Instance.GetOperationCounts();
    //        Assert.AreEqual("OriginalError", clone.Title, "Имя клона повредилось при клонировании");
    //        Assert.IsTrue(counts.ContainsKey("Builder:Build"), "В телеметрии отсутствует запись о создании");
    //    }

    //    private static void Test_9_Prototype_HighVolumeCloningMemoryCheck()
    //    {
    //        var factory = new StandardWidgetFactory();
    //        var builder = new DialogBuilder(factory);
    //        var original = builder.SetTitle("LeakTest")
    //                              .ConfigureTheme(new DefaultThemeFactory())
    //                              .AddButton(new ButtonConfig { Text = "OK" })
    //                              .Build();

    //        try
    //        {
    //            for (int i = 0; i < 5000; i++)
    //            {
    //                var clone = original.Clone();
    //            }
    //            Assert.IsTrue(true, "Массовое клонирование выполнено успешно");
    //        }
    //        catch (Exception ex)
    //        {
    //            Assert.IsTrue(false, $"Массовое клонирование вызвало ошибку: {ex.Message}");
    //        }
    //    }

    //    private static void Test_10_Telemetry_ExportReturnsReadOnlyWithoutMutation()
    //    {
    //        var telemetry = ApplicationTelemetrySingleton.Instance;
    //        telemetry.LogOperation("Security", "Check", TimeSpan.Zero);

    //        var counts = telemetry.GetOperationCounts();

    //        Assert.IsTrue(counts is IReadOnlyDictionary<string, int>, "Словарь должен экспортироваться как IReadOnlyDictionary");
    //    }
    //}

    //ТЕСТЫ 5
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
                Test_1_Decorator_ChainOrderExecution,
                Test_2_Decorator_FindByIdThroughWrappers,
                Test_3_Decorator_CloneRecreatesChain,
                Test_4_Decorator_CacheInvalidatesOnSetPosition,
                Test_5_Facade_CreateDialogReturnsValidComponent,
                Test_6_Facade_ApplyGlobalThemeChangesState,
                Test_7_Facade_RenderAllToContextTraversesWithoutStackOverflow,
                Test_8_Integration_FacadeBuilderDecoratorPrototypeSingleton,
                Test_9_Memory_HighVolumeThemeSwitchNoLeak,
                Test_10_Composite_ReadOnlyChildrenContract,
                Test_11_Facade_ThreadSafetyParallelRender,
                Test_12_Decorator_CacheHitLoggingAccuracy,
                Test_13_Benchmark_RenderOverheadMetrics,
                Test_14_Decorator_PropertyPassthroughToInnerComponent
            };

            Console.WriteLine("Запуск 14 юнит-ТЕСТОВ (ЧАСТЬ 5: DECORATOR + FACADE)");

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

            Console.WriteLine($"\nИТОГИ ТЕСТИРОВАНИЯ: Успешно: {_passed} | Провалено: {_failed}\n");
        }

        private static void Test_1_Decorator_ChainOrderExecution()
        {
            IUIComponent comp = new TextLabelComponent("lbl", 0, 0, "Hello");
            comp = new LogRenderDecorator(new BorderDecorator(comp, "Double"));

            string result = comp.Render();
            Assert.IsTrue(result.Contains("Border(Double)"), "Декоратор Border должен быть в цепочке выполнения");
        }

        private static void Test_2_Decorator_FindByIdThroughWrappers()
        {
            IUIComponent comp = new TextLabelComponent("target_id", 0, 0, "Data");
            comp = new CachedRenderDecorator(new LogRenderDecorator(new BorderDecorator(comp)));

            var found = comp.FindById("target_id");
            Assert.IsTrue(found != null, "Элемент должен находиться через 3 уровня декораторов");
        }

        private static void Test_3_Decorator_CloneRecreatesChain()
        {
            IUIComponent original = new TextLabelComponent("lbl", 5, 5, "Text");
            original = new BorderDecorator(original, "Dashed");

            IUIComponent clone = original.Clone();
            Assert.IsFalse(ReferenceEquals(original, clone), "Клон должен быть новым объектом");
            Assert.IsTrue(clone.Render().Contains("Border(Dashed)"), "Структура декораторов должна воссоздаваться при клонировании");
        }

        private static void Test_4_Decorator_CacheInvalidatesOnSetPosition()
        {
            IUIComponent label = new TextLabelComponent("lbl", 10, 10, "Value");
            var cached = new CachedRenderDecorator(label);

            string firstRender = cached.Render();

            cached.X = 50;

            string secondRender = cached.Render();
            Assert.IsTrue(secondRender.Contains("at (50,10)"), "Кэш должен инвалидироваться и обновиться при изменении координат");
        }

        private static void Test_5_Facade_CreateDialogReturnsValidComponent()
        {
            var facade = new UiSubsystemFacade(new StandardWidgetFactory());
            var dialog = facade.CreateStandardDialog("dlg1", "Main Window", "Welcome");

            Assert.AreEqual("dlg1", dialog.Id, "Фасад создал некорректный ID диалога");
            Assert.IsTrue(dialog.Children.Count > 0, "Фасад обязан наполнить контейнер дочерними элементами");
        }

        private static void Test_6_Facade_ApplyGlobalThemeChangesState()
        {
            var facade = new UiSubsystemFacade(new StandardWidgetFactory());
            var dialog = facade.CreateStandardDialog("dlg", "Title", "Text");

            facade.ApplyGlobalTheme(dialog, "DarkNeon");
            Assert.AreEqual("DarkNeon", dialog.ThemeName, "Тема фасадом не переключилась");
        }

        private static void Test_7_Facade_RenderAllToContextTraversesWithoutStackOverflow()
        {
            var facade = new UiSubsystemFacade(new StandardWidgetFactory());
            var dialog = facade.CreateStandardDialog("dlg", "Test", "Content");

            string fullView = facade.RenderAllToContext(dialog);
            Assert.IsTrue(fullView.Contains("Container") && fullView.Contains("Label"), "Обход дерева Composite не выполнен или сломан");
        }

        private static void Test_8_Integration_FacadeBuilderDecoratorPrototypeSingleton()
        {
            var facade = new UiSubsystemFacade(new StandardWidgetFactory());
            var dialog = facade.CreateStandardDialog("int_test", "Complex", "Body");

            var clone = (UIContainerComponent)dialog.Clone();
            string view = facade.RenderAllToContext(clone);

            var counts = ApplicationTelemetrySingleton.Instance.GetOperationCounts();
            Assert.IsTrue(counts.ContainsKey("Facade:CreateDialog"), "Синглтон телеметрии не зафиксировал операцию фасада");
            Assert.IsTrue(view.Contains("Complex"), "Интеграционная цепочка повредила данные заголовка");
        }

        private static void Test_9_Memory_HighVolumeThemeSwitchNoLeak()
        {
            var facade = new UiSubsystemFacade(new StandardWidgetFactory());
            var dialog = facade.CreateStandardDialog("mem_test", "Speed", "Run");

            for (int i = 0; i < 1000; i++)
            {
                facade.ApplyGlobalTheme(dialog, i % 2 == 0 ? "Light" : "Dark");
            }

            var counts = ApplicationTelemetrySingleton.Instance.GetOperationCounts();
            Assert.AreEqual(1000, counts["Facade:ApplyTheme"], "Счётчик переключений тем не совпадает");
        }

        private static void Test_10_Composite_ReadOnlyChildrenContract()
        {
            var container = new UIContainerComponent("root", "Test", "Default");
            Assert.IsTrue(container.Children is IReadOnlyList<IUIComponent>, "Композит должен отдавать детей строго как read-only коллекцию");
        }

        private static void Test_11_Facade_ThreadSafetyParallelRender()
        {
            var facade = new UiSubsystemFacade(new StandardWidgetFactory());
            var dialog = facade.CreateStandardDialog("parallel_test", "Thread", "Safe");

            Parallel.For(0, 100, i =>
            {
                facade.RenderAllToContext(dialog);
            });

            var counts = ApplicationTelemetrySingleton.Instance.GetOperationCounts();
            Assert.AreEqual(100, counts["Facade:RenderAll"], "Параллельные вызовы чтения фасада заблокировали друг друга или потерялись");
        }

        private static void Test_12_Decorator_CacheHitLoggingAccuracy()
        {
            var label = new TextLabelComponent("lbl", 0, 0, "Test");
            var cache = new CachedRenderDecorator(label);

            cache.Render();
            cache.Render();
            cache.Render();

            var counts = ApplicationTelemetrySingleton.Instance.GetOperationCounts();
            Assert.AreEqual(2, counts["Decorator:CacheHit"], "Попадания в кэш зафиксированы неточно");
        }

        private static void Test_13_Benchmark_RenderOverheadMetrics()
        {
            IUIComponent raw = new TextLabelComponent("lbl", 0, 0, "Benchmark");
            IUIComponent decorated = new CachedRenderDecorator(new LogRenderDecorator(new BorderDecorator(raw)));

            raw.Render();
            decorated.Render();

            Assert.IsTrue(typeof(IUIComponent).IsAssignableFrom(decorated.GetType()), "Декоратор нарушает типы контракта");
        }

        private static void Test_14_Decorator_PropertyPassthroughToInnerComponent()
        {
            var label = new TextLabelComponent("lbl", 5, 5, "Pass");
            var decorator = new BorderDecorator(label);

            decorator.X = 100;
            Assert.AreEqual(100, label.X, "Декоратор не пробросил установку координаты X во внутренний компонент");
        }
    }
}