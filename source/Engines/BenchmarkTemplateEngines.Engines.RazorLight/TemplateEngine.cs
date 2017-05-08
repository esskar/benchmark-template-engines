using System.Collections.Generic;
using BenchmarkTemplateEngines.Contracts;
using RazorLight;
using RazorLight.Extensions;
using RazorLight.Templating;

namespace BenchmarkTemplateEngines.Engines.RazorLight
{
    public class TemplateEngine : ITemplateEngine
    {
        private readonly Dictionary<object, IRazorLightEngine> _engines;

        public TemplateEngine()
        {
            _engines = new Dictionary<object, IRazorLightEngine>();
            this.Init();
        }

        public ITemplateDataProvider GetDataProvider()
        {
            return new TemplateDataProvider();
        }

        public bool CanCompile => true;

        public long? Iterations => 1;

        public string Name => "RazorLight";

        public string Url => "https://github.com/toddams/RazorLight";

        public object Compile(string template, object data)
        {
            var engine = GetEngine(data);
            var templateSource = new LoadedTemplateSource(template);

            var modelTypeInfo = new ModelTypeInfo(data.GetType());
            var result = engine.Core.CompileSource(templateSource, modelTypeInfo);
            result.EnsureSuccessful();

            var page = engine.Activate(result.CompiledType);
            page.PageContext = new PageContext { ModelTypeInfo = modelTypeInfo };

            return page;
        }

        public string Render(string template, object data)
        {
            var engine = GetEngine(data);
            var result = engine.ParseString(template, data, data.GetType());
            return result;
        }

        public string Render(object compiledTemplate, object data)
        {
            var engine = GetEngine(data);
            return engine.RunTemplate((TemplatePage)compiledTemplate, data);
        }

        private IRazorLightEngine GetEngine(object data)
        {
            if (!_engines.TryGetValue(data, out IRazorLightEngine engine))
            {
                engine = EngineFactory.CreateEmbedded(data.GetType());
                _engines.Add(data, engine);
            }
            return engine;
        }

        private void Init()
        {
            Render("INIT!", new {});
        }
    }
}
