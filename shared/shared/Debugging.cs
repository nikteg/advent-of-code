using System.Runtime.CompilerServices;
using System.Text.Json;

namespace shared;

public static class Debugging
{
    public static void Assert<T>(
        T condition,
        T expected,
        [CallerArgumentExpression("condition")] string? conditionName = null,
        [CallerArgumentExpression("expected")] string? expectedName = null
    )
    {
        if (!EqualityComparer<T>.Default.Equals(condition, expected))
        {
            Console.WriteLine($"({conditionName} = {condition}) != {expectedName}");
        }
    }

    public static void Dump(object obj)
    {
        Console.WriteLine(JsonSerializer.Serialize(obj));
    }
}
