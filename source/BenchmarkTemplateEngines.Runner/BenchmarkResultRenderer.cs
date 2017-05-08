using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using BenchmarkTemplateEngines.Contracts;
using Handlebars.Core;
using RazorLight.Extensions;

namespace BenchmarkTemplateEngines.Runner
{
    public class BenchmarkResultRenderer
    {
        private readonly IHandlebarsEngine _engine;

        public BenchmarkResultRenderer()
        {
            _engine = new HandlebarsEngine();
            _engine.RegisterHelper("tableHeader", (engine, output, context, arguments) =>
            {
                var columnNames = (IList<string>)arguments[0];
                for (var i = 0; i < columnNames.Count; i++)
                {
                    if (i != 0)
                        output.Write("|");
                    output.WriteSafeString(columnNames[i]);
                }
                output.WriteLine();
                for (var i = 0; i < columnNames.Count; i++)
                {
                    if (i != 0)
                        output.Write("|");
                    output.Write(new string('-', columnNames[i].Length));
                }
                output.WriteLine();
            });
            _engine.RegisterHelper("tableRow", (engine, output, context, arguments) =>
            {
                var columns = (IList<string>)arguments[0];
                for (var i = 0; i < columns.Count; i++)
                {
                    if (i != 0)
                        output.Write("|");
                    output.WriteSafeString(columns[i]);
                }
                output.WriteLine();
            });
        }

        public void RenderToReadme(IDictionary<ITemplateEngine, IList<BenchmarkResult>> results)
        {
            var readmeTemplateData = File.ReadAllText("..\\..\\README.md.template");
            var readme = Render(results, readmeTemplateData);
            File.WriteAllText("..\\..\\README.md", readme);
        }

        public string Render(IDictionary<ITemplateEngine, IList<BenchmarkResult>> results, string templateData)
        {
            var template = _engine.Compile(templateData);

            var engines = results.Keys.OrderBy(e => e.Name).ToList();
            var allResults = results.Values.SelectMany(r => r).ToList();
            var sectionNames = allResults.Select(r => r.Section).Distinct().ToList();
            var benchmarkNames = allResults.Select(r => r.Name).Distinct().ToList();

            var benchmarkNamesColumnNames = new List<string> {"Engine"};
            foreach (var benchmarkName in benchmarkNames)
            {
                benchmarkNamesColumnNames.Add(benchmarkName);
                benchmarkNamesColumnNames.Add("Iterations");
            }
            var data = new Dictionary<string, object>
            {
                {"engines", engines}
            };

            var resultsAvg = new Dictionary<string, Dictionary<string, BenchmarkResult>>();
           
            var benchmarksAll = new List<Dictionary<string, object>>();
            foreach (var sectionName in sectionNames)
            {
                var section = new Dictionary<string, object>
                {
                    {"name", sectionName},
                    {"columnNames", benchmarkNamesColumnNames}
                };

                var rows = new List<List<string>>();
                foreach (var templateEngine in engines)
                {
                    var engineName = templateEngine.Name;
                    var columns = new List<string> { engineName };
                    var rowResults = results[templateEngine]
                        .Where(r => r.Section == sectionName)
                        .ToDictionary(r => r.Name);

                    if (!resultsAvg.TryGetValue(engineName, out Dictionary<string, BenchmarkResult> avgEngineResults))
                    {
                        avgEngineResults = new Dictionary<string, BenchmarkResult>();
                        resultsAvg.Add(engineName, avgEngineResults);
                    }
                    
                    foreach (var benchmarkName in benchmarkNames)
                    {
                        if (!avgEngineResults.TryGetValue(benchmarkName, out BenchmarkResult avg))
                        {
                            avg = new BenchmarkResult();
                            avgEngineResults.Add(benchmarkName, avg);
                        }

                        var r = rowResults[benchmarkName];
                        if (r.IsSupported)
                        {
                            if (r.Elapsed != null)
                            {
                                var elapsed = string.Format(CultureInfo.InvariantCulture,
                                    "{0:N5}ms", r.Elapsed.Value);
                                columns.Add(elapsed);
                                columns.Add(r.Iterations.ToString(CultureInfo.InvariantCulture));

                                avg.IsSupported = true;
                                avg.Elapsed = avg.Elapsed != null ? avg.Elapsed.Value + r.Elapsed.Value : r.Elapsed;
                                avg.Iterations += r.Iterations;
                            }
                            else if (r.Exception != null)
                            {
                                columns.Add("FAILED");
                                columns.Add("---");
                            }
                            else
                            {
                                columns.Add("---");
                                columns.Add("---");
                            }
                        }
                        else
                        {
                            columns.Add("---");
                            columns.Add("---");
                        }
                    }
                    rows.Add(columns);
                }
                section.Add("rows", rows);

                benchmarksAll.Add(section);
            }
            data.Add("benchmarksAll", benchmarksAll);

            var rowsAvg = new List<List<string>>();
            var benchmarksAvg = new Dictionary<string, object>
            {
                {"columnNames", benchmarkNamesColumnNames},
                {"rows", rowsAvg}
            };
            foreach (var templateEngine in engines)
            {
                var columns = new List<string> {templateEngine.Name};
                foreach (var benchmarkName in benchmarkNames)
                {
                    var r = resultsAvg[templateEngine.Name][benchmarkName];
                    if (r.IsSupported)
                    {
                        if (r.Elapsed != null)
                        {
                            var elapsed = string.Format(CultureInfo.InvariantCulture,
                                "{0:N5}ms", r.Elapsed.Value / sectionNames.Count);
                            columns.Add(elapsed);
                            columns.Add((r.Iterations / sectionNames.Count).ToString(CultureInfo.InvariantCulture));
                        }
                        else
                        {
                            columns.Add("---");
                            columns.Add("---");
                        }
                    }
                    else
                    {
                        columns.Add("---");
                        columns.Add("---");
                    }
                }
                rowsAvg.Add(columns);
            }
            data.Add("benchmarksAvg", benchmarksAvg);

            var result = template.Render(data);
            return result;
        }
    }
}
