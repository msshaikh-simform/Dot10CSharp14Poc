namespace MSDot10CSharp14Poc.Features;

/// <summary>
/// C# 14 Feature: Partial Properties and Events
/// Allows partial definitions for properties and indexers (building on partial methods).
/// </summary>
public static class PartialPropertiesDemo
{
    public static void Run()
    {
        Console.WriteLine("=== C# 14: Partial Properties and Events ===\n");

        // BEFORE C# 14 - Only partial methods existed
        Console.WriteLine("BEFORE C# 14 (Only partial methods):");
        DemonstrateBefore();

        Console.WriteLine();

        // AFTER C# 14 - Partial properties supported
        Console.WriteLine("AFTER C# 14 (Partial properties and events):");
        DemonstrateAfter();
    }

    private static void DemonstrateBefore()
    {
        var oldModel = new OldPartialModel();
        oldModel.SetName("John Doe");  // Had to use methods
        Console.WriteLine($"  oldModel.SetName(\"John Doe\")");
        Console.WriteLine($"  oldModel.GetName() = \"{oldModel.GetName()}\"");
        Console.WriteLine("  (Only partial methods were available)");
    }

    private static void DemonstrateAfter()
    {
        var newModel = new NewPartialModel();
        newModel.Name = "Jane Doe";  // Can use properties now!
        Console.WriteLine($"  newModel.Name = \"Jane Doe\"");
        Console.WriteLine($"  newModel.Name = \"{newModel.Name}\"");

        // Computed partial property
        newModel.FirstName = "John";
        newModel.LastName = "Smith";
        Console.WriteLine($"  newModel.FullName = \"{newModel.FullName}\"");

        // Partial indexer
        newModel[0] = "First Item";
        newModel[1] = "Second Item";
        Console.WriteLine($"  newModel[0] = \"{newModel[0]}\"");
        Console.WriteLine($"  newModel[1] = \"{newModel[1]}\"");
    }
}

// ============================================================
// BEFORE C# 14: Only partial methods were available
// ============================================================
public partial class OldPartialModel
{
    private string _name = string.Empty;

    // Declaration
    partial void OnNameChanging(string value);
    partial void OnNameChanged();

    public void SetName(string name)
    {
        OnNameChanging(name);
        _name = name;
        OnNameChanged();
    }

    public string GetName() => _name;
}

public partial class OldPartialModel
{
    // Implementation
    partial void OnNameChanging(string value)
    {
        // Validation logic
    }

    partial void OnNameChanged()
    {
        // Notification logic
    }
}

// ============================================================
// AFTER C# 14: Partial properties and events
// ============================================================

// Declaration part
public partial class NewPartialModel
{
    // Partial property declaration
    public partial string Name { get; set; }

    // Partial property (computed)
    public partial string FullName { get; }

    // Partial indexer declaration
    public partial string this[int index] { get; set; }

    // Regular properties for the computed property
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}

// Implementation part
public partial class NewPartialModel
{
    private string _name = string.Empty;
    private readonly Dictionary<int, string> _items = [];

    // Partial property implementation
    public partial string Name
    {
        get => _name;
        set
        {
            // Can add validation, notification, etc.
            _name = value?.Trim() ?? string.Empty;
        }
    }

    // Partial computed property implementation
    public partial string FullName => $"{FirstName} {LastName}";

    // Partial indexer implementation
    public partial string this[int index]
    {
        get => _items.TryGetValue(index, out var value) ? value : string.Empty;
        set => _items[index] = value;
    }
}
