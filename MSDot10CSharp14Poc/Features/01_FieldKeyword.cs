namespace MSDot10CSharp14Poc.Features;

/// <summary>
/// C# 14 Feature: The 'field' keyword
/// Allows direct access to the compiler-generated backing field in auto-properties.
/// </summary>
public static class FieldKeywordDemo
{
    public static void Run()
    {
        Console.WriteLine("=== C# 14: The 'field' Keyword ===\n");

        // BEFORE C# 14 - Required explicit backing field
        Console.WriteLine("BEFORE C# 14 (Explicit backing field):");
        var oldPerson = new PersonOld { Name = "  John Doe  " };
        Console.WriteLine($"  Name: '{oldPerson.Name}'");
        Console.WriteLine($"  (Required declaring private field manually)\n");

        // AFTER C# 14 - Using 'field' keyword
        Console.WriteLine("AFTER C# 14 (Using 'field' keyword):");
        var newPerson = new PersonNew { Name = "  Jane Doe  " };
        Console.WriteLine($"  Name: '{newPerson.Name}'");
        Console.WriteLine($"  (No explicit backing field needed - uses 'field' keyword)\n");

        // Demonstrating validation with field keyword
        Console.WriteLine("With validation using 'field':");
        var product = new Product { Price = -50 };
        Console.WriteLine($"  Setting Price = -50, Actual Price: {product.Price} (validated to 0)");
        product.Price = 100;
        Console.WriteLine($"  Setting Price = 100, Actual Price: {product.Price}");
    }
}

// ============================================================
// BEFORE C# 14: Required explicit backing field
// ============================================================
public class PersonOld
{
    private string _name = string.Empty;  // <-- Explicit backing field required

    public string Name
    {
        get => _name;
        set => _name = value?.Trim() ?? string.Empty;
    }
}

// ============================================================
// AFTER C# 14: Using 'field' keyword (no explicit backing field)
// ============================================================
public class PersonNew
{
    public string Name
    {
        get => field;                                    // <-- 'field' refers to auto-generated backing field
        set => field = value?.Trim() ?? string.Empty;   // <-- Direct access without declaring it
    }
}

// Another example with validation
public class Product
{
    public decimal Price
    {
        get => field;
        set => field = value >= 0 ? value : 0;  // <-- Validation using 'field'
    }
}
