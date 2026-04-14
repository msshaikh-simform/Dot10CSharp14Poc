namespace MSDot10CSharp14Poc.Features;

/// <summary>
/// C# 14 Feature: nameof Accessing Instance Members
/// Allows nameof to access instance members from static context using type qualification.
/// </summary>
public static class NameofInstanceMembersDemo
{
    public static void Run()
    {
        Console.WriteLine("=== C# 14: nameof with Instance Members from Static Context ===\n");

        // BEFORE C# 14 - Limited nameof in static context
        Console.WriteLine("BEFORE C# 14 (Workarounds needed):");
        DemonstrateBefore();

        Console.WriteLine();

        // AFTER C# 14 - Direct access
        Console.WriteLine("AFTER C# 14 (Direct instance member access in nameof):");
        DemonstrateAfter();
    }

    private static void DemonstrateBefore()
    {
        // Before - couldn't easily use nameof with instance members in static context
        // Had to use workarounds like creating a dummy instance or string literals

        // Workaround 1: String literal (error-prone, not refactor-safe)
        string propertyName = "Name";  // Not refactor-safe!
        Console.WriteLine($"  Using string literal: \"{propertyName}\" (not refactor-safe)");

        // Workaround 2: Create instance just for nameof
        var dummy = new SampleEntity();
        string safeName = nameof(dummy.Name);
        Console.WriteLine($"  Using dummy instance: nameof(dummy.Name) = \"{safeName}\"");

        // Workaround 3: Use from instance method (not always possible)
        Console.WriteLine("  (Required workarounds in static context)");
    }

    private static void DemonstrateAfter()
    {
        // C# 14 - Can access instance members directly in nameof from static context
        string propertyName = nameof(SampleEntity.Name);  // <-- Works from static context!
        Console.WriteLine($"  nameof(SampleEntity.Name) = \"{propertyName}\"");

        string methodName = nameof(SampleEntity.CalculateTotal);
        Console.WriteLine($"  nameof(SampleEntity.CalculateTotal) = \"{methodName}\"");

        // Works with nested properties
        string nestedName = nameof(SampleEntity.Address.City);
        Console.WriteLine($"  nameof(SampleEntity.Address.City) = \"{nestedName}\"");

        // Useful for attributes and data annotations
        Console.WriteLine($"\n  Practical use cases:");
        Console.WriteLine($"  [Required(ErrorMessage = nameof(SampleEntity.Name) + \" is required\")]");
        Console.WriteLine($"  ArgumentNullException.ThrowIfNull(value, nameof(SampleEntity.Name))");

        // Works with generic type members
        string genericMember = nameof(GenericEntity<int>.Value);
        Console.WriteLine($"  nameof(GenericEntity<int>.Value) = \"{genericMember}\"");

        // Combining with unbound generics (another C# 14 feature)
        string unboundMember = nameof(GenericEntity<>.Value);
        Console.WriteLine($"  nameof(GenericEntity<>.Value) = \"{unboundMember}\"");
    }
}

// Supporting classes
public class SampleEntity
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Address Address { get; set; } = new();

    public decimal CalculateTotal(int quantity) => Price * quantity;
}

public class Address
{
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}

public class GenericEntity<T>
{
    public T? Value { get; set; }
    public string Name { get; set; } = string.Empty;
}
