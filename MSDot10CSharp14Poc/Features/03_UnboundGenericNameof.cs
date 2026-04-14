namespace MSDot10CSharp14Poc.Features;

/// <summary>
/// C# 14 Feature: Unbound Generic Types in nameof
/// Allows using nameof with open generic types like nameof(List&lt;&gt;)
/// </summary>
public static class UnboundGenericNameofDemo
{
    public static void Run()
    {
        Console.WriteLine("=== C# 14: Unbound Generic Types in nameof ===\n");

        // BEFORE C# 14 - Had to use closed generic or workaround
        Console.WriteLine("BEFORE C# 14 (Required closed generic type):");
        DemonstrateBefore();

        Console.WriteLine();

        // AFTER C# 14 - Can use open generic types
        Console.WriteLine("AFTER C# 14 (Unbound generic types allowed):");
        DemonstrateAfter();
    }

    private static void DemonstrateBefore()
    {
        // Before C# 14, you had to specify type arguments
        string listName = nameof(List<int>);  // Had to provide type argument
        Console.WriteLine($"  nameof(List<int>) = \"{listName}\"");

        string dictName = nameof(Dictionary<string, int>);  // All type arguments required
        Console.WriteLine($"  nameof(Dictionary<string, int>) = \"{dictName}\"");

        // Workaround was using typeof and getting name
        string workaround = typeof(List<>).Name;  // Returns "List`1"
        Console.WriteLine($"  typeof(List<>).Name = \"{workaround}\" (includes arity suffix)");
    }

    private static void DemonstrateAfter()
    {
        // C# 14 - Open/unbound generic types work directly in nameof
        string listName = nameof(List<>);  // <-- No type argument needed!
        Console.WriteLine($"  nameof(List<>) = \"{listName}\"");

        string dictName = nameof(Dictionary<,>);  // <-- Unbound with multiple type params
        Console.WriteLine($"  nameof(Dictionary<,>) = \"{dictName}\"");

        // Works with custom generic types too
        string customName = nameof(MyGenericClass<,>);
        Console.WriteLine($"  nameof(MyGenericClass<,>) = \"{customName}\"");

        // Useful for logging, error messages, and reflection scenarios
        Console.WriteLine($"\n  Practical use case - Exception message:");
        Console.WriteLine($"  \"Type {nameof(List<>)} is not supported\"");

        // Works with nested generics
        string nestedName = nameof(Outer<>.Inner);
        Console.WriteLine($"  nameof(Outer<>.Inner) = \"{nestedName}\"");
    }
}

// Custom generic types for demonstration
public class MyGenericClass<T1, T2>
{
    public T1? First { get; set; }
    public T2? Second { get; set; }
}

public class Outer<T>
{
    public class Inner
    {
        public T? Value { get; set; }
    }
}
