using System.Collections.Generic;
using BenchmarkTemplateEngines.Contracts;

namespace BenchmarkTemplateEngines.Runner
{
    public class TemplateEngineProvider
    {
        public IEnumerable<ITemplateEngine> GetTemplateEngines()
        {
            // HINT: Add every engine here
            yield return new Engines.HandlebarsDotNet.TemplateEngine();
            yield return new Engines.HandlebarsCore.TemplateEngine();
            yield return new Engines.RazorLight.TemplateEngine();
        }
    }
}
