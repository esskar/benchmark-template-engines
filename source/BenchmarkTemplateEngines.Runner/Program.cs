namespace BenchmarkTemplateEngines.Runner
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var runner = new BenchmarkRunner();
            var results = runner.Run(25000);

            var renderer = new BenchmarkResultRenderer();
            renderer.RenderToReadme(results);

        }
    }
}