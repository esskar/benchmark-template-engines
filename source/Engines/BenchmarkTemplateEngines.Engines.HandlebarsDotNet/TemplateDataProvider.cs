using BenchmarkTemplateEngines.Contracts;

namespace BenchmarkTemplateEngines.Engines.HandlebarsDotNet
{
    public class TemplateDataProvider : ITemplateDataProvider
    {
        public string GetHelloWorldTemplateData()
        {
            return "Hello World!";
        }

        public string GetHelloWorldWithDataTemplateData()
        {
            return "Hello {{world}}!";
        }
    }
}