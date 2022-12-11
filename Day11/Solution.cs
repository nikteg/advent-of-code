namespace adventofcode.Day11;

public static class Solution
{
    public static void Solve()
    {
        var input = File.ReadAllText("./input");
//          const string input = """
//          Monkey 0:
//            Starting items: 79, 98
//            Operation: new = old * 19
//            Test: divisible by 23
//              If true: throw to monkey 2
//              If false: throw to monkey 3
//
//          Monkey 1:
//            Starting items: 54, 65, 75, 74
//            Operation: new = old + 6
//            Test: divisible by 19
//              If true: throw to monkey 2
//              If false: throw to monkey 0
//
//          Monkey 2:
//            Starting items: 79, 60, 97
//            Operation: new = old * old
//            Test: divisible by 13
//              If true: throw to monkey 1
//              If false: throw to monkey 3
//
//          Monkey 3:
//            Starting items: 74
//            Operation: new = old + 3
//            Test: divisible by 17
//              If true: throw to monkey 0
//              If false: throw to monkey 1
//          """;

        var monkeys = new List<Monkey>();
        var friends = new List<(Monkey monkey, (int m1, int m2) friends)>();
        long totalWorryLevel = 0;

        foreach (var monkeyString in input.TrimEnd().Split("\n\n"))
        {
            var lines = monkeyString.Split("\n");

            var items = lines[1][18..].Split(", ").Select(long.Parse).Select(wl => new Item(wl)).ToList();

            var operationLine = lines[2][23..];
            var hasValue = long.TryParse(operationLine[2..], out var value);
            var operation = new Operation(operationLine[0], hasValue ? value : null);

            var divisor = int.Parse(lines[3][21..]);

            var monkey = new Monkey(items, divisor, operation);

            var m1 = int.Parse(lines[4][29..]);
            var m2 = int.Parse(lines[5][30..]);

            friends.Add((monkey, (m1, m2)));
            monkeys.Add(monkey);
        }

        foreach (var friend in friends)
        {
            var m1 = monkeys[friend.friends.m1];
            var m2 = monkeys[friend.friends.m2];
            friend.monkey.Friends = (m1, m2);
        }

        const bool part2 = true;
        const int numRounds = part2 ? 10_000 : 20;

        for (var i = 1; i <= numRounds; i++)
        {
            foreach (var monkey in monkeys)
            {
                monkey.Round(ref totalWorryLevel, part2, monkeys);
            }
        }

        var monkeyBusiness = monkeys.OrderByDescending(m => m.InspectionCount).Take(2)
            .Aggregate(1L, (current, monkey) => current * monkey.InspectionCount);

        Console.WriteLine(monkeyBusiness);
    }
}

public record Operation(char Op, long? Value);

public record Item(long WorryLevel);

public class Monkey
{
    public int InspectionCount;

    private readonly Queue<Item> Items;
    private readonly long Divisor;
    private readonly Operation Operation;

    public (Monkey m1, Monkey m2) Friends { get; set; }

    public Monkey(List<Item> items, int divisor, Operation operation)
    {
        Items = new Queue<Item>(items);
        Divisor = divisor;
        Operation = operation;
    }

    public void Round(ref long totalWorryLevel, bool part2, List<Monkey> monkeys)
    {
        InspectionCount += Items.Count;

        while (Items.Count > 0)
        {
            var item = Items.Dequeue();

            RunOperation(ref totalWorryLevel, item);

            if (part2)
            {
                totalWorryLevel %= monkeys.Aggregate(1L, (c, m) => c * m.Divisor);
            }
            else
            {
                totalWorryLevel = (int)Math.Truncate(totalWorryLevel / 3.0);
            }

            var newItem = new Item(totalWorryLevel);
            if (totalWorryLevel % Divisor == 0)
            {
                Friends.m1.Items.Enqueue(newItem);
            }
            else
            {
                Friends.m2.Items.Enqueue(newItem);
            }
        }
    }

    private void RunOperation(ref long worryLevel, Item item)
    {
        var value = Operation.Value ?? item.WorryLevel;
        worryLevel = Operation.Op switch
        {
            '+' => value + item.WorryLevel,
            '*' => value * item.WorryLevel,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
