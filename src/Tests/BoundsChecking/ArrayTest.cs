using BenchmarkDotNet.Attributes;

namespace NET10PerformanceImprovements.Tests.BoundsChecking;

/// <summary>
/// Tests the performance of Array bounds checking in .NET 10.
/// </summary>
[DisassemblyDiagnoser]
[HideColumns("Job", "Error", "StdDev", "Median", "RatioSD")]
public partial class ArrayTest
{
    private int[] _array = new int[3];

    [Benchmark]
    public int Read() => _array[2];
}
