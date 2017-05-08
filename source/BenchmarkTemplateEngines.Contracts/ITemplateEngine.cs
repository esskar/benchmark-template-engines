namespace BenchmarkTemplateEngines.Contracts
{
    public interface ITemplateEngine
    {
        bool CanCompile { get; }

        string Name { get; }

        string Url { get; }

        long? Iterations { get; }

        ITemplateDataProvider GetDataProvider();

        object Compile(string template, object data);

        string Render(string template, object data);

        string Render(object compiledTemplate, object data);
    }
}
