namespace MSDot10CSharp14Poc.Features;

/// <summary>
/// C# 14 Feature: Allows 'ref' and 'unsafe' in async methods and iterators
/// Enables using ref locals and unsafe code in async methods with proper scoping.
/// </summary>
public static class RefUnsafeInAsyncDemo
{
    public static void Run()
    {
        Console.WriteLine("=== C# 14: ref and unsafe in Async Methods ===\n");

        // BEFORE C# 14 - ref/unsafe not allowed in async
        Console.WriteLine("BEFORE C# 14 (ref/unsafe not allowed in async):");
        DemonstrateBefore();

        Console.WriteLine();

        // AFTER C# 14 - ref/unsafe allowed with restrictions
        Console.WriteLine("AFTER C# 14 (ref/unsafe allowed in async methods):");
        DemonstrateAfter().GetAwaiter().GetResult();
    }

    private static void DemonstrateBefore()
    {
        Console.WriteLine("  // This would NOT compile before C# 14:");
        Console.WriteLine("  // async Task ProcessAsync() {");
        Console.WriteLine("  //     ref int x = ref someArray[0];  // ERROR!");
        Console.WriteLine("  //     unsafe { /* code */ }          // ERROR!");
        Console.WriteLine("  //     await Task.Delay(100);");
        Console.WriteLine("  // }");
        Console.WriteLine();
        Console.WriteLine("  Had to use workarounds or separate sync methods");

        // Workaround - separate sync method
        var array = new int[] { 1, 2, 3 };
        ProcessSyncOld(array);
        Console.WriteLine($"  Sync workaround result: [{string.Join(", ", array)}]");
    }

    private static void ProcessSyncOld(int[] array)
    {
        ref int first = ref array[0];
        first = 100;
    }

    private static async Task DemonstrateAfter()
    {
        var array = new int[] { 1, 2, 3, 4, 5 };
        Console.WriteLine($"  Before: [{string.Join(", ", array)}]");

        // C# 14 - ref locals in async methods (before await)
        await ProcessWithRefAsync(array);
        Console.WriteLine($"  After ref modification: [{string.Join(", ", array)}]");

        // C# 14 - unsafe in async methods (before await)
        int result = await ProcessWithUnsafeAsync(array);
        Console.WriteLine($"  Unsafe sum result: {result}");

        // Demonstrate iterator with ref
        Console.WriteLine($"\n  Iterator with ref struct:");
        foreach (var item in GetRefStructItems())
        {
            Console.WriteLine($"    Item: {item}");
        }
    }

    // C# 14 - async method with ref local (ref must not cross await)
    private static async Task ProcessWithRefAsync(int[] array)
    {
        // ref local is allowed BEFORE await points
        ref int first = ref array[0];
        first = 100;  // Modify via ref

        ref int last = ref array[^1];
        last = 500;

        // ref locals go out of scope before await
        await Task.Delay(10);

        // After await, can use new ref locals
        ref int middle = ref array[2];
        middle = 300;
    }

    // C# 14 - async method with unsafe code
    private static async Task<int> ProcessWithUnsafeAsync(int[] array)
    {
        int sum = 0;

        // unsafe block allowed before await
        unsafe
        {
            fixed (int* ptr = array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    sum += *(ptr + i);
                }
            }
        }

        await Task.Delay(10);

        return sum;
    }

    // C# 14 - Iterator with ref struct (scoped properly)
    private static IEnumerable<string> GetRefStructItems()
    {
        // Can use Span in iterators now (before yield)
        ReadOnlySpan<int> numbers = [1, 2, 3];

        // Process span before yielding
        var items = new List<string>();
        foreach (var n in numbers)
        {
            items.Add($"Value: {n}");
        }

        // Yield after span is done
        foreach (var item in items)
        {
            yield return item;
        }
    }
}
