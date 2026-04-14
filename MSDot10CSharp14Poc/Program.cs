using MSDot10CSharp14Poc.Features;

Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
Console.WriteLine("║           C# 14 Features Proof of Concept (.NET 10)           ║");
Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
Console.WriteLine();

// Feature 1: The 'field' keyword
FieldKeywordDemo.Run();
Console.WriteLine("\n" + new string('─', 65) + "\n");

// Feature 2: Null-conditional assignment
NullConditionalAssignmentDemo.Run();
Console.WriteLine("\n" + new string('─', 65) + "\n");

// Feature 3: Unbound generic types in nameof
UnboundGenericNameofDemo.Run();
Console.WriteLine("\n" + new string('─', 65) + "\n");

// Feature 4: Lambda parameter modifiers
LambdaParameterModifiersDemo.Run();
Console.WriteLine("\n" + new string('─', 65) + "\n");

// Feature 5: Extension members
ExtensionMembersDemo.Run();
Console.WriteLine("\n" + new string('─', 65) + "\n");

// Feature 6: First-class Span types
FirstClassSpanDemo.Run();
Console.WriteLine("\n" + new string('─', 65) + "\n");

// Feature 7: Partial properties
PartialPropertiesDemo.Run();
Console.WriteLine("\n" + new string('─', 65) + "\n");

// Feature 8: More partial members
MorePartialMembersDemo.Run();
Console.WriteLine("\n" + new string('─', 65) + "\n");

// Feature 9: nameof with instance members
NameofInstanceMembersDemo.Run();
Console.WriteLine("\n" + new string('─', 65) + "\n");

// Feature 10: ref and unsafe in async methods
RefUnsafeInAsyncDemo.Run();

Console.WriteLine("\n" + new string('═', 65));
Console.WriteLine("All C# 14 features demonstrated successfully!");
Console.WriteLine(new string('═', 65));
