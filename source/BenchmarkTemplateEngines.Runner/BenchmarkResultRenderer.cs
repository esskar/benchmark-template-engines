using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using BenchmarkTemplateEngines.Contracts;
using Handlebars.Core;

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
            var iterations = allResults[0].Iterations;

            var benchmarkNamesColumnNames = new List<string>(benchmarkNames);
            benchmarkNamesColumnNames.Insert(0, "Engine");
            var data = new Dictionary<string, object>
            {
                {"engines", engines},
                {"benchmarkNamesColumnNames", benchmarkNamesColumnNames},
                {"iterations", iterations}
            };

            var benchmarks = new List<Dictionary<string, object>>();
            foreach (var sectionName in sectionNames)
            {
                var section = new Dictionary<string, object> {{"name", sectionName}};

                var rows = new List<List<string>>();
                foreach (var templateEngine in engines)
                {
                    var columns = new List<string> { templateEngine.Name };
                    var rowResults = results[templateEngine]
                        .Where(r => r.Section == sectionName)
                        .ToDictionary(r => r.Name);
                    foreach (var benchmarkName in benchmarkNames)
                    {
                        var r = rowResults[benchmarkName];
                        if (r.IsSupported && r.Elapsed != null)
                        {
                            var elapsed = string.Format(CultureInfo.InvariantCulture,
                                "{0}ms", r.Elapsed.Value);
                            columns.Add(elapsed);
                        }
                        else
                        {
                            columns.Add("---");
                        }
                    }
                    rows.Add(columns);
                }
                section.Add("rows", rows);

                benchmarks.Add(section);
            }
            data.Add("benchmarks", benchmarks);
            var result = template.Render(data);
            return result;
        }
    }
}
