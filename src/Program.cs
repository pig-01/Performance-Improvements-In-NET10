// dotnet run -c Release -f net9.0 --filter "*" --runtimes net9.0 net10.0
using BenchmarkDotNet.Running;
using NET10PerformanceImprovements.Tests.Devirtualization;
using NET10PerformanceImprovements.Tests.StackAllocation;

string? input = string.Empty;

while (true)
{
    input = string.Empty;
    Console.WriteLine("Running benchmarks...");
    Console.WriteLine();

    // Show benchmarks menu
    Console.WriteLine(
        $@"
Select a benchmark to run:
1. {nameof(NET10PerformanceImprovements.Tests.StackAllocation)} {nameof(DelegateTest)}
2. {nameof(NET10PerformanceImprovements.Tests.StackAllocation)} {nameof(StackAllocationTest)}
3. {nameof(NET10PerformanceImprovements.Tests.StackAllocation)} {nameof(EscapeAnalysisTest)}
4. {nameof(NET10PerformanceImprovements.Tests.Devirtualization)} {nameof(ArrayTest)}
5. {nameof(NET10PerformanceImprovements.Tests.Devirtualization)} {nameof(ListTest)}
6. {nameof(NET10PerformanceImprovements.Tests.Devirtualization)} {nameof(LinqTest)}
"
    );

    // Read user input
    Console.Write("Enter the number of the benchmark to run (or Press ctrl + C to quit): ");
    input = Console.ReadLine();
    switch (input)
    {
        case "1":
            BenchmarkRunner.Run<DelegateTest>(null, args);
            break;
        case "2":
            BenchmarkRunner.Run<StackAllocationTest>(null, args);
            break;
        case "3":
            BenchmarkRunner.Run<EscapeAnalysisTest>(null, args);
            break;
        case "4":
            BenchmarkRunner.Run<ArrayTest>(null, args);
            break;
        case "5":
            BenchmarkRunner.Run<ListTest>(null, args);
            break;
        case "6":
            BenchmarkRunner.Run<LinqTest>(null, args);
            break;
        default:
            Console.WriteLine("Invalid input. Please try again.");
            break;
    }
}
