using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace NET10PerformanceImprovements.Tests.StackAllocation;

[MemoryDiagnoser(displayGenColumns: false)]
[HideColumns("Job", "Error", "StdDev", "Median", "RatioSD")]
public partial class StackAllocationTest
{
    [Benchmark]
    public void Test()
    {
        Process(["a", "b", "c"]);

        static void Process(string[] inputs)
        {
            foreach (string input in inputs)
            {
                Use(input);
            }

            [MethodImpl(MethodImplOptions.NoInlining)]
            static void Use(string input) { }
        }
    }
}
