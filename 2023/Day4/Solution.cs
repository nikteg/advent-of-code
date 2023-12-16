namespace aoc2023.Day4;

public static class Solution
{
    private static readonly char[] Separators = { '|', ':' };

    public static void Solve()
    {
        var input = File.ReadAllText("./input").TrimEnd();
        const string sample = """
            Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
            Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
            Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
            Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
            Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
            Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
            """;

        // input = sample;

        Console.WriteLine(Part1(input));
        Console.WriteLine(Part2(input));
    }

    private static int Part1(string input)
    {
        var lines = input.Split("\n");

        var sum = 0;

        foreach (var line in lines)
        {
            var s = line.Split(Separators);

            var winning = s[1]
                .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToHashSet();

            var numbers = s[2]
                .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToHashSet();

            var count = winning.Intersect(numbers).Count();

            if (count > 0)
            {
                sum += 1 << (count - 1);
            }
        }

        return sum;
    }

    private static int Part2(string input)
    {
        var lines = input.Split("\n");
        var sum = 0;
        var cards = new Dictionary<int, int>();
        var stack = new Stack<int>();

        var id = 1;
        foreach (var line in lines)
        {
            var s = line.Split(Separators);

            var winning = s[1]
                .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
            var numbers = s[2]
                .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            var count = winning.Intersect(numbers).Count();

            cards[id] = count;
            stack.Push(id);
            id++;
        }

        while (stack.Count > 0)
        {
            var idx = stack.Pop();
            var count = cards[idx];

            for (var i = 1; i <= count; i++)
                stack.Push(idx + i);

            sum += 1;
        }

        return sum;
    }
}
