using BenchmarkTemplateEngines.Contracts;

namespace BenchmarkTemplateEngines.Engines.HandlebarsCore
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
