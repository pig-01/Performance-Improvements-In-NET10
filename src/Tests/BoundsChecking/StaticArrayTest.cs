using BenchmarkDotNet.Attributes;

namespace NET10PerformanceImprovements.Tests.BoundsChecking;

/// <summary>
/// Tests the performance of StaticArray bounds checking in .NET 10.
/// </summary>
[DisassemblyDiagnoser]
[HideColumns("Job", "Error", "StdDev", "Median", "RatioSD")]
public partial class StaticArrayTest
{
    private static readonly int[] s_array = new int[3];

    [Benchmark]
    public int Read() => s_array[2];

}
