using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace NET10PerformanceImprovements.Tests.StackAllocation;

[MemoryDiagnoser(displayGenColumns: false)]
[HideColumns("Job", "Error", "StdDev", "Median", "RatioSD")]
public class EscapeAnalysisTest
{
    private byte[] _buffer = new byte[3];

    [Benchmark]
    public void Test() => Copy3Bytes(0x12345678, _buffer);

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void Copy3Bytes(int value, Span<byte> dest) =>
        BitConverter.GetBytes(value).AsSpan(0, 3).CopyTo(dest);
}
