namespace MSDot10CSharp14Poc.Features;

/// <summary>
/// C# 14 Feature: Extension Members (Extension Types)
/// A new way to define extension methods, properties, static members, and more.
/// NOTE: Extension types syntax may require preview language features.
/// </summary>
public static class ExtensionMembersDemo
{
    public static void Run()
    {
        Console.WriteLine("=== C# 14: Extension Members (Extension Types) ===\n");

        // BEFORE C# 14 - Traditional extension methods only
        Console.WriteLine("BEFORE C# 14 (Traditional extension methods):");
        DemonstrateBefore();

        Console.WriteLine();

        // AFTER C# 14 - The new syntax concept
        Console.WriteLine("AFTER C# 14 (Extension types concept - new syntax):");
        DemonstrateAfter();
    }

    private static void DemonstrateBefore()
    {
        string text = "Hello World";

        // Traditional extension method
        int wordCount = text.WordCountOld();
        Console.WriteLine($"  \"{text}\".WordCountOld() = {wordCount}");

        // Could only do methods, no properties or static members
        bool isEmpty = text.IsEmptyOrWhitespaceOld();
        Console.WriteLine($"  \"{text}\".IsEmptyOrWhitespaceOld() = {isEmpty}");

        Console.WriteLine("  (Limited to instance methods only)");
    }

    private static void DemonstrateAfter()
    {
        // Demonstrating with current extension methods that mimic the new capabilities
        string text = "Hello World";

        Console.WriteLine("  New Extension Types Syntax (C# 14):");
        Console.WriteLine();
        Console.WriteLine("  // Declaration:");
        Console.WriteLine("  public implicit extension StringExtensions for string");
        Console.WriteLine("  {");
        Console.WriteLine("      public int WordCount() => this.Split(' ').Length;");
        Console.WriteLine("      public bool IsNullOrEmpty => string.IsNullOrEmpty(this);  // Property!");
        Console.WriteLine("      public static char DefaultSeparator => ' ';               // Static!");
        Console.WriteLine("  }");
        Console.WriteLine();
        Console.WriteLine("  // Usage:");
        Console.WriteLine("  text.WordCount();       // Extension method");
        Console.WriteLine("  text.IsNullOrEmpty;     // Extension PROPERTY (new!)");
        Console.WriteLine("  StringExtensions.DefaultSeparator;  // Static member (new!)");
        Console.WriteLine();

        // Using current extension methods to demonstrate functionality
        int wordCount = text.WordCount();
        Console.WriteLine($"  Current demo: \"{text}\".WordCount() = {wordCount}");

        bool isEmpty = text.GetIsNullOrEmpty();
        Console.WriteLine($"  Current demo: \"{text}\".GetIsNullOrEmpty() = {isEmpty}");

        int number = 42;
        Console.WriteLine($"  Current demo: {number}.GetIsEven() = {number.GetIsEven()}");
        Console.WriteLine($"  Current demo: {number}.GetSquared() = {number.GetSquared()}");
    }
}

// ============================================================
// BEFORE C# 14: Traditional extension methods (static class)
// ============================================================
public static class OldStyleExtensions
{
    public static int WordCountOld(this string str)
    {
        if (string.IsNullOrWhiteSpace(str)) return 0;
        return str.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
    }

    public static bool IsEmptyOrWhitespaceOld(this string str)
    {
        return string.IsNullOrWhiteSpace(str);
    }
}

// ============================================================
// Current extension methods demonstrating C# 14 capabilities
// When extension types are fully available, these become:
//
// public implicit extension StringExtensions for string
// {
//     public int WordCount() => ...;
//     public bool IsNullOrEmpty => ...;  // Extension property!
// }
// ============================================================
public static class NewStyleExtensions
{
    // Extension method (works the same way)
    public static int WordCount(this string str)
    {
        if (string.IsNullOrWhiteSpace(str)) return 0;
        return str.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
    }

    // Simulating extension property (will be actual property in C# 14)
    public static bool GetIsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

    // Simulating extension property for int
    public static bool GetIsEven(this int value) => value % 2 == 0;

    // Simulating extension property for int
    public static int GetSquared(this int value) => value * value;
}
