# C# 14 Features Proof of Concept

A comprehensive demonstration of all new C# 14 language features targeting .NET 10.

## 📋 Table of Contents

- [Overview](#overview)
- [Prerequisites](#prerequisites)
- [Features Demonstrated](#features-demonstrated)
- [Project Structure](#project-structure)
- [How to Run](#how-to-run)
- [Feature Details](#feature-details)
  - [1. The `field` Keyword](#1-the-field-keyword)
  - [2. Null-Conditional Assignment](#2-null-conditional-assignment)
  - [3. Unbound Generic Types in nameof](#3-unbound-generic-types-in-nameof)
  - [4. Lambda Parameter Modifiers](#4-lambda-parameter-modifiers)
  - [5. Extension Members](#5-extension-members-extension-types)
  - [6. First-Class Span Types](#6-first-class-span-types)
  - [7. Partial Properties](#7-partial-properties)
  - [8. More Partial Members](#8-more-partial-members)
  - [9. nameof with Instance Members](#9-nameof-with-instance-members-from-static-context)
  - [10. ref/unsafe in Async Methods](#10-ref-and-unsafe-in-async-methods)
- [References](#references)

---

## Overview

This project serves as a Proof of Concept (POC) for **C# 14** language features introduced with **.NET 10**. Each feature is demonstrated with clear **Before** and **After** examples showing:

- How the same task was accomplished before C# 14
- The new, improved syntax available in C# 14
- Practical use cases and benefits

## Prerequisites

| Requirement | Version |
|-------------|---------|
| **.NET SDK** | 10.0 or later |
| **Visual Studio** | 2026 (v18.x) or later |
| **C# Language Version** | 14.0 |

### Verify Installation

```bash
dotnet --version
# Should output: 10.x.x or higher
```

### SDK Installation

Download .NET 10 SDK from: https://dotnet.microsoft.com/download/dotnet/10.0

---

## Features Demonstrated

| # | Feature | Description |
|---|---------|-------------|
| 1 | `field` Keyword | Access auto-generated backing field in properties |
| 2 | Null-Conditional Assignment | `?.=` operator for safe assignment |
| 3 | Unbound Generic Types in `nameof` | Use `nameof(List<>)` without type arguments |
| 4 | Lambda Parameter Modifiers | `ref`, `out`, `in`, `params` on lambda parameters |
| 5 | Extension Members | Extension properties, static members, indexers |
| 6 | First-Class Span Types | Improved implicit conversions to `Span<T>` |
| 7 | Partial Properties | Partial property declarations and implementations |
| 8 | More Partial Members | Partial constructors and events |
| 9 | `nameof` Instance Members | Access instance members in static context |
| 10 | `ref`/`unsafe` in Async | Use ref locals and unsafe in async methods |

---

## Project Structure

```
MSDot10CSharp14Poc/
├── Program.cs                           # Main entry point
├── MSDot10CSharp14Poc.csproj           # Project file (.NET 10, C# 14)
├── README.md                            # This file
└── Features/
    ├── 01_FieldKeyword.cs              # field keyword demo
    ├── 02_NullConditionalAssignment.cs # ?.= operator demo
    ├── 03_UnboundGenericNameof.cs      # nameof(List<>) demo
    ├── 04_LambdaParameterModifiers.cs  # Lambda modifiers demo
    ├── 05_ExtensionMembers.cs          # Extension types demo
    ├── 06_FirstClassSpan.cs            # Span improvements demo
    ├── 07_PartialProperties.cs         # Partial properties demo
    ├── 08_MorePartialMembers.cs        # Partial constructors/events
    ├── 09_NameofInstanceMembers.cs     # nameof improvements
    └── 10_RefUnsafeInAsync.cs          # ref/unsafe in async
```

---

## How to Run

### Using Visual Studio 2026

1. Open `MSDot10CSharp14Poc.sln`
2. Press `F5` or click **Debug > Start Debugging**

### Using Command Line

```bash
cd MSDot10CSharp14Poc
dotnet run
```

### Expected Output

The program will demonstrate each C# 14 feature with clear before/after comparisons.

---

## Feature Details

### 1. The `field` Keyword

**File:** `Features/01_FieldKeyword.cs`

The `field` keyword provides direct access to the compiler-generated backing field in auto-properties.

#### Before C# 14
```csharp
public class Person
{
    private string _name = string.Empty;  // Explicit backing field required

    public string Name
    {
        get => _name;
        set => _name = value?.Trim() ?? string.Empty;
    }
}
```

#### After C# 14
```csharp
public class Person
{
    public string Name
    {
        get => field;                                    // 'field' refers to auto-generated backing field
        set => field = value?.Trim() ?? string.Empty;   // No explicit declaration needed
    }
}
```

**Benefits:**
- Reduced boilerplate code
- No need to declare and maintain backing fields
- Cleaner property definitions with custom logic

---

### 2. Null-Conditional Assignment

**File:** `Features/02_NullConditionalAssignment.cs`

The `?.=` operator allows assignment only if the left-hand side is not null.

#### Before C# 14
```csharp
if (container != null)
{
    container.Value = "Updated";
}
```

#### After C# 14
```csharp
container?.Value = "Updated";  // Assigns only if container is not null
```

**Benefits:**
- Concise null-safe assignments
- Chainable: `wrapper?.Inner?.Value = "Updated";`
- Eliminates verbose null checks

---

### 3. Unbound Generic Types in nameof

**File:** `Features/03_UnboundGenericNameof.cs`

Use `nameof` with open/unbound generic types without specifying type arguments.

#### Before C# 14
```csharp
string name = nameof(List<int>);           // Had to provide type argument
string workaround = typeof(List<>).Name;   // Returns "List`1" with arity
```

#### After C# 14
```csharp
string name = nameof(List<>);              // Returns "List" - clean!
string dictName = nameof(Dictionary<,>);   // Multiple type parameters work too
```

**Benefits:**
- Cleaner code for logging and error messages
- No arbitrary type arguments needed
- Works with nested types: `nameof(Outer<>.Inner)`

---

### 4. Lambda Parameter Modifiers

**File:** `Features/04_LambdaParameterModifiers.cs`

Use `ref`, `out`, `in`, `scoped`, and `params` modifiers on lambda parameters without explicit types.

#### Before C# 14
```csharp
RefAction<int> increment = (ref int x) => x++;  // Type required with modifier
```

#### After C# 14
```csharp
RefAction<int> increment = (ref x) => x++;      // No type needed!
OutAction<string> init = (out s) => s = "Hi";   // Works with out
ParamsFunc<int> sum = (params values) => values.Sum();  // params in lambda!
```

**Benefits:**
- Reduced verbosity in lambda expressions
- Type inference works with modifiers
- `params` support in lambdas

---

### 5. Extension Members (Extension Types)

**File:** `Features/05_ExtensionMembers.cs`

A new way to define extensions with properties, static members, indexers, and more.

#### Before C# 14
```csharp
public static class StringExtensions
{
    public static int WordCount(this string str) => ...;  // Only methods
}
```

#### After C# 14
```csharp
public implicit extension StringExtensions for string
{
    // Extension method
    public int WordCount() => this.Split(' ').Length;
    
    // Extension PROPERTY (NEW!)
    public bool IsNullOrEmpty => string.IsNullOrEmpty(this);
    
    // Static member
    public static char DefaultSeparator => ' ';
}

// Usage
"Hello World".IsNullOrEmpty;    // Extension property!
"Hello World".WordCount();      // Extension method
```

**Benefits:**
- Extension properties, not just methods
- Static members on extension types
- Cleaner, more intuitive API design

---

### 6. First-Class Span Types

**File:** `Features/06_FirstClassSpan.cs`

Improved implicit conversions and first-class support for `Span<T>` and `ReadOnlySpan<T>`.

#### Before C# 14
```csharp
int[] numbers = [1, 2, 3];
ProcessSpan(numbers.AsSpan());  // Explicit AsSpan() call needed
```

#### After C# 14
```csharp
int[] numbers = [1, 2, 3];
ProcessSpan(numbers);  // Implicit conversion to Span<int>!

// params with ReadOnlySpan
int Sum(params ReadOnlySpan<int> values) => values.Sum();

// Collection expressions to Span
ReadOnlySpan<int> span = [1, 2, 3];  // Direct assignment!
```

**Benefits:**
- Seamless array-to-Span conversions
- Better performance with `params ReadOnlySpan<T>`
- Collection expressions work with Span

---

### 7. Partial Properties

**File:** `Features/07_PartialProperties.cs`

Declare partial properties that can be implemented in another partial class definition.

#### Before C# 14
```csharp
// Only partial methods were available
public partial class Model
{
    partial void OnNameChanged();  // Only methods
}
```

#### After C# 14
```csharp
// Declaration
public partial class Model
{
    public partial string Name { get; set; }  // Partial property declaration
}

// Implementation
public partial class Model
{
    private string _name = "";
    
    public partial string Name
    {
        get => _name;
        set => _name = value?.Trim() ?? "";  // Custom implementation
    }
}
```

**Benefits:**
- Ideal for source generators
- Separation of declaration and implementation
- Works with indexers too

---

### 8. More Partial Members

**File:** `Features/08_MorePartialMembers.cs`

Extended partial member support including constructors and events.

#### Partial Constructors
```csharp
// Declaration
public partial class Entity
{
    public partial Entity(int id, string name);
}

// Implementation
public partial class Entity
{
    public partial Entity(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
```

#### Partial Events
```csharp
// Declaration
public partial class Publisher
{
    public partial event Action<string>? OnDataReceived;
}

// Implementation with custom accessors
public partial class Publisher
{
    private Action<string>? _handler;
    
    public partial event Action<string>? OnDataReceived
    {
        add => _handler += value;
        remove => _handler -= value;
    }
}
```

**Benefits:**
- Complete source generator support
- Flexible initialization patterns
- Custom event implementations

---

### 9. nameof with Instance Members from Static Context

**File:** `Features/09_NameofInstanceMembers.cs`

Access instance members in `nameof` from static context using type qualification.

#### Before C# 14
```csharp
// In static method - couldn't access instance members
string name = "Name";                      // String literal (not refactor-safe)
var dummy = new Entity();
string safe = nameof(dummy.Name);          // Workaround with dummy instance
```

#### After C# 14
```csharp
// Direct access from static context!
string name = nameof(Entity.Name);                    // Works!
string nested = nameof(Entity.Address.City);          // Nested access
string generic = nameof(GenericEntity<>.Value);       // With generics
```

**Benefits:**
- Refactor-safe member references
- No dummy instances needed
- Works with attributes and validation

---

### 10. ref and unsafe in Async Methods

**File:** `Features/10_RefUnsafeInAsync.cs`

Use `ref` locals and `unsafe` code in async methods (before await points).

#### Before C# 14
```csharp
async Task ProcessAsync()
{
    ref int x = ref array[0];  // ERROR: ref not allowed in async!
    unsafe { /* code */ }       // ERROR: unsafe not allowed in async!
    await Task.Delay(100);
}
```

#### After C# 14
```csharp
async Task ProcessAsync()
{
    // ref and unsafe allowed BEFORE await points
    ref int first = ref array[0];
    first = 100;  // Modify via ref
    
    unsafe
    {
        fixed (int* ptr = array) { /* process */ }
    }
    
    // ref goes out of scope here
    await Task.Delay(100);
    
    // Can use new ref locals after await
    ref int last = ref array[^1];
}
```

**Benefits:**
- Performance-critical code in async methods
- No need for sync helper methods
- Proper scoping ensures safety

---

## References

- [C# 14 What's New](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-14)
- [.NET 10 Release Notes](https://github.com/dotnet/core/tree/main/release-notes/10.0)
- [C# Language Reference](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/)
- [Roslyn GitHub Repository](https://github.com/dotnet/roslyn)

---

## License

This project is provided as-is for educational purposes.

---

**Happy Coding with C# 14! 🚀**
