using System.Collections.Immutable;

namespace adventofcode.Day6;

public static class Solution
{
    public static void Solve()
    {
        var input = File.ReadAllText("./input");
        // const string input = """mjqjpqmgbljsphdztnvjfqwrcgsmlb""";

        Console.WriteLine(GetMarker(input, 4));
        Console.WriteLine(GetMarker(input, 14));
    }

    private static int GetMarker(string input, int characterCount)
    {
        var span = input.AsSpan();

        for (var i = 0; i < span.Length - characterCount; i++)
        {
            if (span.Slice(i, characterCount).ToImmutableArray().Distinct().Count() == characterCount)
            {
                return i + characterCount;
            }
        }

        return 0;
    }
}
