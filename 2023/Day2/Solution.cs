namespace aoc2023.Day2;

public static class Solution
{
    public static void Solve()
    {
        var input = File.ReadAllText("./input").TrimEnd();
        const string sample = """
            Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
            Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
            Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
            Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
            Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
            """;

        // input = sample;

        var lines = input.Split("\n");

        Console.WriteLine(Part1(lines));
        Console.WriteLine(Part2(lines));
    }

    private static int Part1(string[] lines)
    {
        var sum = 0;

        foreach (var line in lines)
        {
            var g = line.Split(':', ',', ';');

            var id = int.Parse(g[0].Split(' ')[^1]);

            var possible = true;

            foreach (var cube in g[1..])
            {
                var s = cube.Trim().Split(" ");
                var count = int.Parse(s[0]);
                var color = s[^1];

                if (
                    color == "red" && count > 12
                    || color == "green" && count > 13
                    || color == "blue" && count > 14
                )
                {
                    possible = false;
                    break;
                }
            }

            if (possible)
                sum += id;
        }

        return sum;
    }

    private static int Part2(string[] lines)
    {
        var sum = 0;

        foreach (var line in lines)
        {
            var g = line.Split(':', ',', ';');

            var id = int.Parse(g[0].Split(' ')[^1]);

            var red = 0;
            var green = 0;
            var blue = 0;

            foreach (var cube in g[1..])
            {
                var s = cube.Trim().Split(" ");
                var count = int.Parse(s[0]);
                var color = s[^1];

                if (color == "red")
                    red = int.Max(red, count);
                if (color == "green")
                    green = int.Max(green, count);
                if (color == "blue")
                    blue = int.Max(blue, count);
            }

            sum += red * green * blue;
        }

        return sum;
    }
}
