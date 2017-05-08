using BenchmarkTemplateEngines.Contracts;

namespace BenchmarkTemplateEngines.Engines.RazorLight
{
    public class TemplateDataProvider : ITemplateDataProvider
    {
        public string GetHelloWorldTemplateData()
        {
            return "Hello World!";
        }

        public string GetHelloWorldWithDataTemplateData()
        {
            return "Hello @Model.world!";
        }
    }
}