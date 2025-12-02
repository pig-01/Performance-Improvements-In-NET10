using BenchmarkDotNet.Attributes;

namespace NET10PerformanceImprovements.Tests.StackAllocation;

/// <summary>
/// Benchmark to test delegate allocation improvements in .NET 10.
/// </summary>
[DisassemblyDiagnoser]
[MemoryDiagnoser(displayGenColumns: false)]
[HideColumns("Job", "Error", "StdDev", "Median", "RatioSD", "y")]
public partial class DelegateTest
{
    [Benchmark]
    [Arguments(42)]
    public int Sum(int y)
    {
        Func<int, int> addY = x => x + y;
        return DoubleResult(addY, y);
    }

    private int DoubleResult(Func<int, int> func, int arg)
    {
        int result = func(arg);
        return result + result;
    }
}
