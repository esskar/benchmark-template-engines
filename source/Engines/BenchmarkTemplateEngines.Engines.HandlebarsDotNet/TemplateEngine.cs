using System;
using BenchmarkTemplateEngines.Contracts;
using HandlebarsDotNet;

namespace BenchmarkTemplateEngines.Engines.HandlebarsDotNet
{
    public class TemplateEngine : ITemplateEngine
    {
        public ITemplateDataProvider GetDataProvider()
        {
            return new TemplateDataProvider();
        }

        public bool CanCompile => true;

        public string Name => "HandlebarsDotNet";

        public string Url => "https://github.com/rexm/Handlebars.Net";

        public long? Iterations => null;

        public object Compile(string template, object data)
        {
            return Handlebars.Compile(template);
        }

        public string Render(string template, object data)
        {
            var compiledTemplate = Handlebars.Compile(template);
            return compiledTemplate(data);
        }

        public string Render(object compiledTemplate, object data)
        {
            return ((Func<object, string>)compiledTemplate)(data);
        }
    }
}
