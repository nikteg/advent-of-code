namespace aoc2023.Day3;

public static class Solution
{
    public static void Solve()
    {
        var input = File.ReadAllText("./input").TrimEnd();
        // const string sample = """
        //     467..114..
        //     ...*......
        //     ..35..633.
        //     ......#...
        //     617*......
        //     .....+.58.
        //     ..592.....
        //     ......755.
        //     ...$.*....
        //     .664.598..
        //     """;

        var part1 = Part1(input);
        var part2 = Part2(input);

        Console.WriteLine(part1);
        Console.WriteLine(part2);
    }

    private static int Part1(string input)
    {
        var (symbols, numbers) = GetSymbolsAndNumbers(input);

        var withSymbols = new Dictionary<(int y, int x), int>();

        foreach (var n in numbers)
            for (var y = -1; y < 2; y++)
                for (var x = -1; x < 2 + n.Value.ToString().Length - 1; x++)
                    if (symbols.ContainsKey((n.Key.y + y, n.Key.x + x)))
                        withSymbols.TryAdd(n.Key, n.Value);

        return withSymbols.Sum(s => s.Value);
    }

    private static int Part2(string input)
    {
        var (symbols, numbers) = GetSymbolsAndNumbers(input);

        var gearNumbers = new Dictionary<(int y, int x), List<int>>();

        foreach (var n in numbers)
            for (var y = -1; y < 2; y++)
                for (var x = -1; x < 2 + n.Value.ToString().Length - 1; x++)
                {
                    var ky = n.Key.y + y;
                    var kx = n.Key.x + x;

                    if (symbols.TryGetValue((ky, kx), out var s) && s == '*')
                    {
                        if (gearNumbers.TryGetValue((ky, kx), out var ns))
                        {
                            ns.Add(n.Value);
                        }
                        else
                        {
                            gearNumbers.Add((ky, kx), new List<int> { n.Value });
                        }
                    }
                }

        return gearNumbers.Where(gn => gn.Value.Count == 2).Sum(gn => gn.Value[0] * gn.Value[1]);
    }

    private static (
        Dictionary<(int y, int x), char> symbols,
        Dictionary<(int y, int x), int> numbers
    ) GetSymbolsAndNumbers(string input)
    {
        var lines = input.Split("\n");

        var symbols = new Dictionary<(int y, int x), char>();
        var numbers = new Dictionary<(int y, int x), int>();

        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y];

            var buf = "";

            for (var x = 0; x < line.Length; x++)
            {
                var c = line[x];

                if (IsNumber(c))
                {
                    buf += c;
                    continue;
                }

                if (IsSymbol(c))
                    symbols.TryAdd((y, x), c);

                if (buf.Length > 0)
                {
                    numbers.TryAdd((y, x - buf.Length), int.Parse(buf));
                    buf = "";
                }
            }

            if (buf.Length > 0)
                numbers.TryAdd((y, line.Length - buf.Length), int.Parse(buf));
        }

        return (symbols, numbers);
    }

    private static bool IsNumber(char c) => c >= 48 && c <= 57;

    private static bool IsSymbol(char c) => !IsNumber(c) && c != '.';
}
