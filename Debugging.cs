using System.Runtime.CompilerServices;

namespace adventofcode;

public static class Debugging
{
    public static void Assert<T>(T condition, T expected,
        [CallerArgumentExpression("condition")] string? conditionName = null,
        [CallerArgumentExpression("expected")] string? expectedName = null)
    {
        if (!EqualityComparer<T>.Default.Equals(condition, expected))
        {
            Console.WriteLine($"({conditionName} = {condition}) != {expectedName}");
        }
    }
}
