using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace NET10PerformanceImprovements.Tests.Devirtualization;

/// <summary>
/// Tests the performance of generic Equals method devirtualization in .NET 10.
/// </summary>
[HideColumns("Job", "Error", "StdDev", "Median", "RatioSD")]
public partial class GenericEqualsTest
{
    [Benchmark]
    public bool Test() => GenericEquals("abc", "abc");

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static bool GenericEquals<T>(T a, T b) => EqualityComparer<T>.Default.Equals(a, b);
}
