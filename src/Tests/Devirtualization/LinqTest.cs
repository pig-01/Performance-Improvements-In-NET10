using System.Collections.ObjectModel;
using BenchmarkDotNet.Attributes;

namespace NET10PerformanceImprovements.Tests.Devirtualization;

/// <summary>
/// Tests the performance of LINQ devirtualization in .NET 10.
/// </summary>
[HideColumns("Job", "Error", "StdDev", "Median", "RatioSD")]
public class LinqTest
{
    private ReadOnlyCollection<int> _list = new(Enumerable.Range(1, 1000).ToArray());

    [Benchmark]
    public int SkipTakeSum() => _list.Skip(100).Take(800).Sum();
}
