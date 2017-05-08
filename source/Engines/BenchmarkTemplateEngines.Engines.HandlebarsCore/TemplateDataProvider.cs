using BenchmarkTemplateEngines.Contracts;

namespace BenchmarkTemplateEngines.Engines.HandlebarsCore
{
    public class TemplateDataProvider : ITemplateDataProvider
    {
        public string GetHelloWorldData()
        {
            return "Hello World!";
        }
    }
}
