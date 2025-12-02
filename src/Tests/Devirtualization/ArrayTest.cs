using System;
using System.Collections.ObjectModel;
using BenchmarkDotNet.Attributes;

namespace NET10PerformanceImprovements.Tests.Devirtualization;

[HideColumns("Job", "Error", "StdDev", "Median", "RatioSD")]
public class ArrayTest
{
    private ReadOnlyCollection<int> _list = new(Enumerable.Range(1, 1000).ToArray());

    [Benchmark]
    public int SumEnumerable()
    {
        int sum = 0;
        foreach (var item in _list)
        {
            sum += item;
        }
        return sum;
    }

    [Benchmark]
    public int SumForLoop()
    {
        ReadOnlyCollection<int> list = _list;
        int sum = 0;
        int count = list.Count;
        for (int i = 0; i < count; i++)
        {
            sum += _list[i];
        }
        return sum;
    }
}
