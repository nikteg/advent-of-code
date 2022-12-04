namespace adventofcode.Day4;

public static class Solution
{
    public static void Solve()
    {
        var input = File.ReadAllText("./input");
        // const string input = """
        //     2-4,6-8
        //     2-3,4-5
        //     5-7,7-9
        //     2-8,3-7
        //     6-6,4-6
        //     2-6,4-8
        //     """;

        var lines = input.TrimEnd().Split("\n");

        var part1 = lines.Count(line =>
        {
            var ranges = line.Split(",");
            return Contains(ranges) || Contains(ranges.Reverse().ToArray());
        });
        var part2 = lines.Count(line =>
        {
            var ranges = line.Split(",");
            return Intersects(ranges) || Intersects(ranges.Reverse().ToArray());
        });

        Console.WriteLine(part1);
        Console.WriteLine(part2);
    }

    private static bool Contains(string[] ranges)
    {
        var (d1, u1) = RangeToNumbers(ranges[0]);
        var (d2, u2) = RangeToNumbers(ranges[1]);

        Console.WriteLine($"{d1} {u1} {d2} {u2}");

        return d1 >= d2 && u1 <= u2;
    }

    private static bool Intersects(string[] ranges)
    {
        var (d1, u1) = RangeToNumbers(ranges[0]);
        var (d2, u2) = RangeToNumbers(ranges[1]);

        return d1 >= d2 && d1 <= u2 || u1 >= u2 && u1 <= u2;
    }

    private static (int, int) RangeToNumbers(string range)
    {
        var r = range.Split("-").ToArray();

        return (int.Parse(r[0]), int.Parse(r[1]));
    }
}
