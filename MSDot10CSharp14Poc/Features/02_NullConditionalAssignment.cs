namespace MSDot10CSharp14Poc.Features;

/// <summary>
/// C# 14 Feature: Null-Conditional Assignment (?.=)
/// Allows assignment only if the left-hand side is not null.
/// </summary>
public static class NullConditionalAssignmentDemo
{
    public static void Run()
    {
        Console.WriteLine("=== C# 14: Null-Conditional Assignment (?.=) ===\n");

        // BEFORE C# 14 - Verbose null check required
        Console.WriteLine("BEFORE C# 14 (Explicit null check):");
        DemonstrateBefore();

        Console.WriteLine();

        // AFTER C# 14 - Clean null-conditional assignment
        Console.WriteLine("AFTER C# 14 (Null-conditional assignment ?.=):");
        DemonstrateAfter();
    }

    private static void DemonstrateBefore()
    {
        var container = new ContainerOld { Value = "Initial" };

        // Old way - verbose null checking
        if (container != null)
        {
            container.Value = "Updated";
        }
        Console.WriteLine($"  Container value: {container?.Value}");

        ContainerOld? nullContainer = null;
        if (nullContainer != null)
        {
            nullContainer.Value = "This won't happen";
        }
        Console.WriteLine($"  Null container - no crash, but verbose syntax");
    }

    private static void DemonstrateAfter()
    {
        var container = new ContainerNew { Value = "Initial" };

        // C# 14 - Concise null-conditional assignment
        container?.Value = "Updated";  // <-- Assigns only if container is not null
        Console.WriteLine($"  Container value: {container?.Value}");

        ContainerNew? nullContainer = null;
        nullContainer?.Value = "This won't happen";  // <-- Safe, no crash
        Console.WriteLine($"  Null container - no crash, clean syntax!");

        // Works with nested objects too
        var wrapper = new Wrapper { Inner = new ContainerNew { Value = "Nested" } };
        wrapper?.Inner?.Value = "Updated Nested";  // <-- Chained null-conditional assignment
        Console.WriteLine($"  Nested value: {wrapper?.Inner?.Value}");

        var emptyWrapper = new Wrapper();
        emptyWrapper?.Inner?.Value = "Won't crash";  // <-- Safe even with null Inner
        Console.WriteLine($"  Empty wrapper inner: {emptyWrapper?.Inner?.Value ?? "null"}");
    }
}

// Supporting classes
public class ContainerOld
{
    public string? Value { get; set; }
}

public class ContainerNew
{
    public string? Value { get; set; }
}

public class Wrapper
{
    public ContainerNew? Inner { get; set; }
}
