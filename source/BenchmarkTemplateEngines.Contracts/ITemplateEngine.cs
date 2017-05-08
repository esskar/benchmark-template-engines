namespace BenchmarkTemplateEngines.Contracts
{
    public interface ITemplateEngine
    {
        bool CanCompile { get; }

        string Name { get; }

        ITemplateDataProvider GetDataProvider();

        object Compile(string template);

        string Render(string template, object data);

        string Render(object compiledTemplate, object data);
    }
}
