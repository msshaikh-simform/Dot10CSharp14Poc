namespace MSDot10CSharp14Poc.Features;

/// <summary>
/// C# 14 Feature: More Partial Members
/// Extended partial member support including constructors and events.
/// </summary>
public static class MorePartialMembersDemo
{
    public static void Run()
    {
        Console.WriteLine("=== C# 14: More Partial Members ===\n");

        // Demonstrate partial constructors
        Console.WriteLine("Partial Constructors:");
        var entity = new PartialEntity(1, "Test Entity");
        Console.WriteLine($"  Created entity: Id={entity.Id}, Name=\"{entity.Name}\"");
        Console.WriteLine($"  CreatedAt: {entity.CreatedAt}");
        Console.WriteLine();

        // Demonstrate partial events
        Console.WriteLine("Partial Events:");
        var publisher = new EventPublisher();
        publisher.OnDataReceived += data => Console.WriteLine($"  Event received: \"{data}\"");
        publisher.SendData("Hello from partial event!");
        Console.WriteLine();

        // Demonstrate source generator friendly patterns
        Console.WriteLine("Source Generator Pattern:");
        var generated = new GeneratorFriendlyClass();
        Console.WriteLine($"  generated.ComputedValue = {generated.ComputedValue}");
        Console.WriteLine($"  generated.GeneratedProperty = \"{generated.GeneratedProperty}\"");
    }
}

// ============================================================
// Partial Constructors (C# 14)
// ============================================================

// Declaration part - typically from source generator or separate file
public partial class PartialEntity
{
    // Partial constructor declaration
    public partial PartialEntity(int id, string name);

    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
}

// Implementation part
public partial class PartialEntity
{
    // Partial constructor implementation
    public partial PartialEntity(int id, string name)
    {
        Id = id;
        Name = name;
        CreatedAt = DateTime.UtcNow;
        // Additional initialization logic
    }
}

// ============================================================
// Partial Events (C# 14)
// ============================================================

// Event publisher with custom event accessors
public partial class EventPublisher
{
    private Action<string>? _onDataReceived;

    // Event with custom add/remove accessors
    public event Action<string>? OnDataReceived
    {
        add
        {
            Console.WriteLine($"  Subscriber added");
            _onDataReceived += value;
        }
        remove
        {
            _onDataReceived -= value;
            Console.WriteLine($"  Subscriber removed");
        }
    }

    public void SendData(string data)
    {
        ProcessData(data);
        _onDataReceived?.Invoke(data);
    }

    partial void ProcessData(string data);
}

// Implementation part
public partial class EventPublisher
{

    partial void ProcessData(string data)
    {
        // Pre-processing logic
    }
}

// ============================================================
// Source Generator Friendly Pattern (C# 14)
// ============================================================

// This pattern is ideal for source generators
// Declaration (could be generated)
public partial class GeneratorFriendlyClass
{
    // Generated partial property declarations
    public partial int ComputedValue { get; }
    public partial string GeneratedProperty { get; set; }
}

// Implementation (user code or another generator)
public partial class GeneratorFriendlyClass
{
    private string _generatedProperty = "Default";

    public partial int ComputedValue => DateTime.Now.Second * 2;

    public partial string GeneratedProperty
    {
        get => _generatedProperty;
        set => _generatedProperty = $"[Generated] {value}";
    }
}
