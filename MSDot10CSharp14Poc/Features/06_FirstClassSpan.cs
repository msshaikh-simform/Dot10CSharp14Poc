namespace MSDot10CSharp14Poc.Features;

/// <summary>
/// C# 14 Feature: First-Class Span Types
/// Improved implicit conversions and support for Span&lt;T&gt; and ReadOnlySpan&lt;T&gt;
/// </summary>
public static class FirstClassSpanDemo
{
    public static void Run()
    {
        Console.WriteLine("=== C# 14: First-Class Span Types ===\n");

        // BEFORE C# 14 - Limited span conversions
        Console.WriteLine("BEFORE C# 14 (Limited Span conversions):");
        DemonstrateBefore();

        Console.WriteLine();

        // AFTER C# 14 - Improved span support
        Console.WriteLine("AFTER C# 14 (First-class Span support):");
        DemonstrateAfter();
    }

    private static void DemonstrateBefore()
    {
        // Before - explicit conversions often needed
        int[] numbers = [1, 2, 3, 4, 5];

        // Had to explicitly create spans
        Span<int> span = numbers.AsSpan();
        Console.WriteLine($"  numbers.AsSpan() required for Span<int>");

        ReadOnlySpan<int> readOnlySpan = numbers.AsSpan();
        Console.WriteLine($"  Explicit AsSpan() call needed");

        // String to ReadOnlySpan<char> worked but limited
        string text = "Hello";
        ReadOnlySpan<char> chars = text.AsSpan();
        Console.WriteLine($"  \"{text}\".AsSpan() for ReadOnlySpan<char>");
    }

    private static void DemonstrateAfter()
    {
        // C# 14 - Implicit conversions to Span types
        int[] numbers = [1, 2, 3, 4, 5];

        // Implicit conversion from array to Span
        ProcessSpan(numbers);  // <-- No AsSpan() needed!
        Console.WriteLine($"  ProcessSpan(int[]) - implicit conversion to Span<int>");

        // Implicit conversion from array to ReadOnlySpan
        ProcessReadOnlySpan(numbers);  // <-- Direct array pass
        Console.WriteLine($"  ProcessReadOnlySpan(int[]) - implicit conversion");

        // String to ReadOnlySpan<char> - improved scenarios
        string text = "Hello, World!";
        ProcessCharSpan(text);  // <-- String directly to ReadOnlySpan<char>
        Console.WriteLine($"  ProcessCharSpan(string) - implicit conversion");

        // Span in params
        Console.WriteLine($"\n  Span with params:");
        int sum = SumParams(1, 2, 3, 4, 5);  // <-- params ReadOnlySpan<int>
        Console.WriteLine($"  SumParams(1, 2, 3, 4, 5) = {sum}");

        // Collection expressions to Span
        ReadOnlySpan<int> inlineSpan = [10, 20, 30];  // <-- Collection expression to span
        Console.WriteLine($"  ReadOnlySpan<int> = [10, 20, 30] (collection expression)");
        Console.WriteLine($"  Sum: {SumSpan(inlineSpan)}");

        // Span in conditional expressions
        int[] array1 = [1, 2, 3];
        int[] array2 = [4, 5, 6];
        bool condition = true;
        ReadOnlySpan<int> selected = condition ? array1 : array2;  // <-- Works in ternary!
        Console.WriteLine($"  Ternary with spans: condition ? array1 : array2 = [{string.Join(", ", selected.ToArray())}]");
    }

    // Helper methods
    private static void ProcessSpan(Span<int> span)
    {
        // Process span
    }

    private static void ProcessReadOnlySpan(ReadOnlySpan<int> span)
    {
        // Process read-only span
    }

    private static void ProcessCharSpan(ReadOnlySpan<char> chars)
    {
        // Process character span
    }

    // C# 14 - params with ReadOnlySpan
    private static int SumParams(params ReadOnlySpan<int> values)
    {
        int sum = 0;
        foreach (var value in values)
        {
            sum += value;
        }
        return sum;
    }

    private static int SumSpan(ReadOnlySpan<int> span)
    {
        int sum = 0;
        foreach (var item in span)
        {
            sum += item;
        }
        return sum;
    }
}
