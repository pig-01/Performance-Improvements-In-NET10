// dotnet run -c Release -f net9.0 --filter "*" --runtimes net9.0 net10.0
using BenchmarkDotNet.Running;
using BoundsChecking = NET10PerformanceImprovements.Tests.BoundsChecking;
using Devirtualization = NET10PerformanceImprovements.Tests.Devirtualization;
using StackAllocation = NET10PerformanceImprovements.Tests.StackAllocation;

string? input = string.Empty;

while (true)
{
    input = string.Empty;
    Console.WriteLine("Running benchmarks...");
    Console.WriteLine();

    // Show benchmarks menu
    Console.WriteLine(
        $@"
{nameof(StackAllocation)} Benchmarks:
1. {nameof(StackAllocation.DelegateTest)}
2. {nameof(StackAllocation.StackAllocationTest)}
3. {nameof(StackAllocation.EscapeAnalysisTest)}

{nameof(Devirtualization)} Benchmarks:
4. {nameof(Devirtualization.ArrayTest)}
5. {nameof(Devirtualization.ListTest)}
6. {nameof(Devirtualization.LinqTest)}
7. {nameof(Devirtualization.GenericEqualsTest)}

{nameof(BoundsChecking)} Benchmarks:
8. {nameof(BoundsChecking.StaticArrayTest)}
9. {nameof(BoundsChecking.ArrayTest)}
10. {nameof(BoundsChecking.Log2SoftwareFallbackTest)}
"
    );

    // Read user input
    Console.Write("Enter the number of the benchmark to run (or Press ctrl + C to quit): ");
    input = Console.ReadLine();
    switch (input)
    {
        case "1":
            BenchmarkRunner.Run<StackAllocation.DelegateTest>(null, args);
            break;
        case "2":
            BenchmarkRunner.Run<StackAllocation.StackAllocationTest>(null, args);
            break;
        case "3":
            BenchmarkRunner.Run<StackAllocation.EscapeAnalysisTest>(null, args);
            break;
        case "4":
            BenchmarkRunner.Run<Devirtualization.ArrayTest>(null, args);
            break;
        case "5":
            BenchmarkRunner.Run<Devirtualization.ListTest>(null, args);
            break;
        case "6":
            BenchmarkRunner.Run<Devirtualization.LinqTest>(null, args);
            break;
        case "7":
            BenchmarkRunner.Run<Devirtualization.GenericEqualsTest>(null, args);
            break;
        case "8":
            BenchmarkRunner.Run<BoundsChecking.ArrayTest>(null, args);
            break;
        case "9":
            BenchmarkRunner.Run<BoundsChecking.StaticArrayTest>(null, args);
            break;
        case "10":
            BenchmarkRunner.Run<BoundsChecking.Log2SoftwareFallbackTest>(null, args);
            break;
        default:
            Console.WriteLine("Invalid input. Please try again.");
            break;
    }
}
