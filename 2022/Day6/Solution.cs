namespace aoc2022.Day6;

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
        for (var i = 0; i < input.Length - characterCount; i++)
        {
            if (input[i..(i + characterCount)].Distinct().Count() == characterCount)
            {
                return i + characterCount;
            }
        }

        return 0;
    }
}
