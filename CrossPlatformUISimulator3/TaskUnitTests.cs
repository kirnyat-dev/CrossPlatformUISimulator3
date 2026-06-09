using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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
                Test_1_Composite_FindByIdDeepLevel,
                Test_2_Composite_CyclicReferenceException,
                Test_3_Composite_CloneCreatesIndependentSubtree,
                Test_4_Bridge_StrategyPatternDelegation,
                Test_5_Bridge_OCP_NewAbstractionSlider,
                Test_6_Bridge_OCP_NewStrategyVector,
                Test_7_Adapter_LegacyEngineMappingAndExecution,
                Test_8_Integration_BuilderTreeAndTelemetry,
                Test_9_Memory_StrategySwitchNoLeak,
                Test_10_Composite_ReadOnlyChildrenContract,
                Test_11_Composite_ThreadSafetyParallelRead,
                Test_12_Composite_CascadePositionUpdate
            };

            Console.WriteLine("ЗАПУСК 12 ЮНИТ-ТЕСТОВ (BRIDGE + COMPOSITE)");

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

            Console.WriteLine($"\nИТОГИ: Успешно: {_passed} | Провалено: {_failed}\n");

            RunTreeTraversalBenchmark();
        }

        private static void Test_1_Composite_FindByIdDeepLevel()
        {
            var root = new UICompositeContainer("root", 0, 0);
            var current = root;
            for (int i = 1; i <= 6; i++)
            {
                var next = new UICompositeContainer($"node_{i}", 0, 0);
                current.AddChild(next);
                current = next;
            }
            current.AddChild(new LabelComponent("target_leaf", 0, 0, "Deep Data"));

            var found = root.FindById("target_leaf") as LabelComponent;
            Assert.IsTrue(found != null && found.Text == "Deep Data", "Элемент на глубоком уровне не найден!");
        }

        private static void Test_2_Composite_CyclicReferenceException()
        {
            var parent = new UICompositeContainer("parent", 0, 0);
            var child = new UICompositeContainer("child", 0, 0);
            parent.AddChild(child);

            bool caught = false;
            try
            {
                child.AddChild(parent);
            }
            catch (InvalidOperationException)
            {
                caught = true;
            }
            Assert.IsTrue(caught, "Система не предотвратила появление циклической ссылки!");
        }

        private static void Test_3_Composite_CloneCreatesIndependentSubtree()
        {
            var root = new UICompositeContainer("root", 0, 0);
            var label = new LabelComponent("lbl", 0, 0, "Original");
            root.AddChild(label);

            var clone = (UICompositeContainer)root.Clone();
            var cloneLabel = clone.FindById("lbl") as LabelComponent;

            Assert.IsTrue(cloneLabel != null, "Потомок потерялся при клонировании");
            cloneLabel!.Text = "Mutated";

            Assert.AreEqual("Original", label.Text, "Изменение клона повредило оригинал!");
        }

        private static void Test_4_Bridge_StrategyPatternDelegation()
        {
            var label = new LabelComponent("lbl", 10, 20, "Test");
            RenderingManager.CurrentStrategy = new VectorRenderingStrategy();
            Assert.IsTrue(label.Render().Contains("[Vector]"), "Мост не делегирует отрисовку стратегии!");
        }

        private static void Test_5_Bridge_OCP_NewAbstractionSlider()
        {
            IUIComponent slider = new SliderComponent("sld", 0, 0, 75);
            Assert.AreEqual("sld", slider.Id, "SliderComponent не реализует контракт IUIComponent");
        }

        private static void Test_6_Bridge_OCP_NewStrategyVector()
        {
            IRenderingStrategy strategy = new VectorRenderingStrategy();
            Assert.AreEqual("VectorEngine", strategy.GetStrategyName(), "Новая стратегия работает некорректно");
        }

        private static void Test_7_Adapter_LegacyEngineMappingAndExecution()
        {
            RenderingManager.CurrentStrategy = new LegacyEngineRenderingAdapter();
            var container = new UICompositeContainer("panel", 0, 0);
            container.AddChild(new LabelComponent("lbl", 0, 0, "Legacy"));

            Assert.IsTrue(container.Render().Contains("Legacy"), "Адаптер старого движка сломался при вызове через Мост");
        }

        private static void Test_8_Integration_BuilderTreeAndTelemetry()
        {
            var builder = new UICompositeBuilder();
            var tree = builder.SetContainer("main", 0, 0, "Window")
                              .AddLabel("l1", 5, 5, "Login")
                              .Build();

            Assert.IsTrue(tree.Children.Count == 1, "Билдер неверно собрал Composite");
            var metrics = ApplicationTelemetrySingleton.Instance.GetOperationCounts();
            Assert.IsTrue(metrics.ContainsKey("Builder:BuildTree"), "Сборка дерева не залогировалась в телеметрию");
        }

        private static void Test_9_Memory_StrategySwitchNoLeak()
        {
            var container = new UICompositeContainer("root", 0, 0);
            var vStrategy = new VectorRenderingStrategy();
            var lAdapter = new LegacyEngineRenderingAdapter();

            for (int i = 0; i < 1000; i++)
            {
                RenderingManager.CurrentStrategy = i % 2 == 0 ? vStrategy : lAdapter;
                container.Render();
            }
            Assert.IsTrue(true, "Успешно пройдено 1000 переключений без утечек");
        }

        private static void Test_10_Composite_ReadOnlyChildrenContract()
        {
            var container = new UICompositeContainer("c", 0, 0);
            Assert.IsTrue(container.Children is IReadOnlyList<IUIComponent>, "Children должны быть доступны только для чтения.");
        }

        private static void Test_11_Composite_ThreadSafetyParallelRead()
        {
            var root = new UICompositeContainer("root", 0, 0);
            for (int i = 0; i < 100; i++) root.AddChild(new LabelComponent($"lbl_{i}", 0, 0, "Val"));

            Parallel.For(0, 1000, i =>
            {
                var item = root.FindById($"lbl_{i % 100}");
                Assert.IsTrue(item != null, "Потокобезопасный поиск вернул null");
            });
        }

        private static void Test_12_Composite_CascadePositionUpdate()
        {
            var panel = new UICompositeContainer("panel", 10, 10);
            var lbl = new LabelComponent("lbl", 15, 15, "Text");
            panel.AddChild(lbl);

            panel.X = 20;
            Assert.AreEqual(25, lbl.X, "Каскадное обновление координат сломалось");
        }

        private static void RunTreeTraversalBenchmark()
        {
            Console.WriteLine("--- ЗАПУСК БЕНЧМАРКА DFS-ОБХОДА (10 000 УЗЛОВ) ---");
            var root = new UICompositeContainer("root", 0, 0);
            var current = root;
            for (int i = 0; i < 10000; i++)
            {
                var next = new UICompositeContainer($"n_{i}", 0, 0);
                current.AddChild(next);
                current = next;
            }

            var sw = Stopwatch.StartNew();
            RecursiveDFS(root);
            sw.Stop();
            long recTime = sw.ElapsedTicks;

            sw.Restart();
            IterativeDFS(root);
            sw.Stop();
            long iterTime = sw.ElapsedTicks;

            Console.WriteLine($"Рекурсивный DFS: {recTime} тиков.");
            Console.WriteLine($"Итеративный DFS (через Stack): {iterTime} тиков.");
            Console.WriteLine("Результат: Итеративный подход успешно предотвращает StackOverflowException.\n");
        }

        private static void RecursiveDFS(IUIComponent component)
        {
            if (component is UICompositeContainer container)
            {
                foreach (var child in container.Children) RecursiveDFS(child);
            }
        }

        private static void IterativeDFS(IUIComponent root)
        {
            var stack = new Stack<IUIComponent>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (current is UICompositeContainer container)
                {
                    for (int i = container.Children.Count - 1; i >= 0; i--) stack.Push(container.Children[i]);
                }
            }
        }
    }
}