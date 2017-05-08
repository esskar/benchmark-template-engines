using System;

namespace BenchmarkTemplateEngines.Runner
{
    public class BenchmarkResult
    {
        public string Section { get; set; }

        public string Name { get; set; }

        public long Iterations { get; set; }

        public bool IsSupported { get; set; }

        public bool IsCompletedSuccessfully { get; set; }

        public Exception Exception { get; set; }

        public double? Elapsed { get; set; }
    }
}
