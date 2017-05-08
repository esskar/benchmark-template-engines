using BenchmarkTemplateEngines.Contracts;

namespace BenchmarkTemplateEngines.Engines.HandlebarsDotNet
{
    public class TemplateDataProvider : ITemplateDataProvider
    {
        public string GetHelloWorldData()
        {
            return "Hello World!";
        }
    }
}