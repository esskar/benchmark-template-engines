﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using BenchmarkTemplateEngines.Contracts;

namespace BenchmarkTemplateEngines.Runner
{
    public class BenchmarkRunner
    {
        public Dictionary<ITemplateEngine, IList<BenchmarkResult>> Run(long iterations)
        {
            var results = new Dictionary<ITemplateEngine, IList<BenchmarkResult>>();
            var templateEngineProvider = new TemplateEngineProvider();
            foreach (var templateEngine in templateEngineProvider.GetTemplateEngines())
            {
                var engineResults = Benchmark(templateEngine, templateEngine.Iterations ?? iterations);
                results.Add(templateEngine, engineResults);
            }
            return results;
        }

        public IList<BenchmarkResult> Benchmark(ITemplateEngine templateEngine, long iterations)
        {
            var result = new List<BenchmarkResult>();
            result.AddRange(BenchmarkHelloWorld(templateEngine, iterations));
            result.AddRange(BenchmarkHelloWorldWithData(templateEngine, iterations));
            return result;
        }

        public IList<BenchmarkResult> BenchmarkHelloWorld(ITemplateEngine templateEngine, long iterations)
        {
            var dataProvider = templateEngine.GetDataProvider();
            var templateData = dataProvider.GetHelloWorldTemplateData();
            var data = new {};
            return BenchmarkAll(templateEngine, templateData, data, iterations, "HelloWorld");
        }

        public IList<BenchmarkResult> BenchmarkHelloWorldWithData(ITemplateEngine templateEngine, long iterations)
        {
            var dataProvider = templateEngine.GetDataProvider();
            var templateData = dataProvider.GetHelloWorldWithDataTemplateData();
            var data = new
            {
                world = "World",
            };
            return BenchmarkAll(templateEngine, templateData, data, iterations, "HelloWorld with Data");
        }

        private static IList<BenchmarkResult> BenchmarkAll(ITemplateEngine templateEngine, string templateData,
            object data, long iterations, string name)
        {
            var result = new List<BenchmarkResult>
            {
                BenchmarkRender(templateEngine, templateData, data, iterations, name),
                BenchmarkCompile(templateEngine, templateData, data, iterations, name),
                BenchmarkRenderCompile(templateEngine, templateData, data, iterations, name)
            };
            return result;
        }

        private static BenchmarkResult BenchmarkRender(ITemplateEngine templateEngine, string templateData, object data, long iterations, string section)
        {
            var result = new BenchmarkResult
            {
                Section = section,
                Name = "Render",
                IsSupported = true
            };
            BenchmarkAction(result, iterations, () => templateEngine.Render(templateData, data), templateEngine.Name);
            return result;
        }

        private static BenchmarkResult BenchmarkCompile(ITemplateEngine templateEngine, string templateData, object data, long iterations, string section)
        {
            var result = new BenchmarkResult
            {
                Section = section,
                Name = "Compile",
                IsSupported = templateEngine.CanCompile
            };
            BenchmarkAction(result, iterations, () => templateEngine.Compile(templateData, data), templateEngine.Name);
            return result;
        }

        private static BenchmarkResult BenchmarkRenderCompile(ITemplateEngine templateEngine, string templateData, object data, long iterations, string section)
        {
            var result = new BenchmarkResult
            {
                Section = section,
                Name = "Compile&Render",
                IsSupported = templateEngine.CanCompile
            };
            if (templateEngine.CanCompile)
            {
                var compiledTemplate = templateEngine.Compile(templateData, data);
                BenchmarkAction(result, iterations, () => templateEngine.Render(compiledTemplate, data), templateEngine.Name);
            }
            else
            {
                BenchmarkAction(result, iterations, () => { }, templateEngine.Name);
            }
            return result;
        }

        private static void BenchmarkAction(BenchmarkResult result, long iterations, Action action, string templateEngineName)
        {
            if (result.IsSupported)
            {
                result.Iterations = iterations;
                Console.WriteLine("{0}: Starting benchmark '{1}:{2}' with {3} iterations ...",
                    templateEngineName, result.Section, result.Name, iterations);
                try
                {
                    var sw = Stopwatch.StartNew();
                    for (var i = 0; i < iterations; i++)
                    {
                        action();
                    }
                    sw.Stop();

                    double elapsedMilliseconds = sw.ElapsedMilliseconds;
                    result.Elapsed = elapsedMilliseconds / iterations;
                    result.IsCompletedSuccessfully = true;
                    Console.WriteLine("{0}: Completed '{1}:{2}' in {3:N5}ms",
                        templateEngineName, result.Section, result.Name, result.Elapsed.Value);
                }
                catch (Exception e)
                {
                    result.IsCompletedSuccessfully = false;
                    result.Exception = e;
                    Console.WriteLine("{0}: Failed to benchmark '{1}:{2}': {3}",
                       templateEngineName, result.Section, result.Name, e);
                }
            }
            else
            {
                Console.WriteLine("{0}: '{1}' is not supported.", templateEngineName, result.Name);
            }
        }
    }
}