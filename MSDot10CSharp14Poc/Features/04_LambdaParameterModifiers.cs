namespace MSDot10CSharp14Poc.Features;

/// <summary>
/// C# 14 Feature: Simple Lambda Parameters with Modifiers
/// Allows using ref, out, in, scoped, and params modifiers on lambda parameters
/// without explicit type declarations.
/// </summary>
public static class LambdaParameterModifiersDemo
{
    public static void Run()
    {
        Console.WriteLine("=== C# 14: Lambda Parameter Modifiers ===\n");

        // BEFORE C# 14 - Required explicit types with modifiers
        Console.WriteLine("BEFORE C# 14 (Required explicit types with modifiers):");
        DemonstrateBefore();

        Console.WriteLine();

        // AFTER C# 14 - Modifiers without explicit types
        Console.WriteLine("AFTER C# 14 (Modifiers on simple lambda parameters):");
        DemonstrateAfter();
    }

    // Delegate definitions
    delegate void RefAction<T>(ref T value);
    delegate void OutAction<T>(out T value);
    delegate void InAction<T>(in T value);
    delegate T ParamsFunc<T>(params T[] values);

    private static void DemonstrateBefore()
    {
        // Before C# 14 - had to specify types when using modifiers
        RefAction<int> incrementOld = (ref int x) => x++;
        int valueOld = 5;
        incrementOld(ref valueOld);
        Console.WriteLine($"  (ref int x) => x++ : Value after increment: {valueOld}");

        OutAction<string> initializeOld = (out string s) => s = "Initialized";
        initializeOld(out string resultOld);
        Console.WriteLine($"  (out string s) => ... : Result: \"{resultOld}\"");

        InAction<int> readOnlyOld = (in int x) => Console.WriteLine($"  (in int x) => ... : Read-only value: {x}");
        readOnlyOld(in valueOld);
    }

    private static void DemonstrateAfter()
    {
        // C# 14 - Modifiers work without explicit type declarations!

        // 'ref' modifier without type
        RefAction<int> increment = (ref x) => x++;  // <-- No 'int' needed!
        int value = 5;
        increment(ref value);
        Console.WriteLine($"  (ref x) => x++ : Value after increment: {value}");

        // 'out' modifier without type
        OutAction<string> initialize = (out s) => s = "Initialized";  // <-- No 'string' needed!
        initialize(out string result);
        Console.WriteLine($"  (out s) => ... : Result: \"{result}\"");

        // 'in' modifier without type (read-only reference)
        InAction<int> readOnly = (in x) => Console.WriteLine($"  (in x) => ... : Read-only value: {x}");
        readOnly(in value);

        // 'params' modifier in lambdas (requires explicit type currently)
        ParamsFunc<int> sum = (params int[] values) => values.Sum();
        Console.WriteLine($"  (params int[] values) => values.Sum() : Sum(1,2,3,4,5) = {sum(1, 2, 3, 4, 5)}");

        // Combining modifiers - scoped ref
        Console.WriteLine($"\n  Combined example with ref:");
        RefAction<int> triple = (ref n) =>
        {
            n *= 3;  // Modify the original value
        };
        int number = 10;
        triple(ref number);
        Console.WriteLine($"  (ref n) => n *= 3 : 10 * 3 = {number}");
    }
}
