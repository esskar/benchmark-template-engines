using System;
using BenchmarkTemplateEngines.Contracts;
using Handlebars.Core;

namespace BenchmarkTemplateEngines.Engines.HandlebarsCore
{
    public class TemplateEngine : ITemplateEngine
    {
        private readonly IHandlebarsEngine _engine;

        public TemplateEngine()
        {
            _engine = new HandlebarsEngine();
        }

        public ITemplateDataProvider GetDataProvider()
        {
            return new TemplateDataProvider();
        }

        public bool CanCompile => true;

        public string Name => "HandlebarsDotNet";

        public object Compile(string template)
        {
            return _engine.Compile(template);
        }

        public string Render(string template, object data)
        {
            var compiledTemplate = _engine.Compile(template);
            return compiledTemplate.Render(data);
        }

        public string Render(object compiledTemplate, object data)
        {
            return ((HandlebarsTemplate)compiledTemplate).Render(data);
        }
    }
}
