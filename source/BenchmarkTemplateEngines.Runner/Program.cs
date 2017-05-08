using System;

namespace BenchmarkTemplateEngines.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var runner = new BenchmarkRunner();
            runner.Run(25000);
        }
    }
}