namespace adventofcode.Day10;

public static class Solution
{
    public static void Solve()
    {
        var input = File.ReadAllText("./input");
        // const string input = """
        // addx 15
        // addx -11
        // addx 6
        // addx -3
        // addx 5
        // addx -1
        // addx -8
        // addx 13
        // addx 4
        // noop
        // addx -1
        // addx 5
        // addx -1
        // addx 5
        // addx -1
        // addx 5
        // addx -1
        // addx 5
        // addx -1
        // addx -35
        // addx 1
        // addx 24
        // addx -19
        // addx 1
        // addx 16
        // addx -11
        // noop
        // noop
        // addx 21
        // addx -15
        // noop
        // noop
        // addx -3
        // addx 9
        // addx 1
        // addx -3
        // addx 8
        // addx 1
        // addx 5
        // noop
        // noop
        // noop
        // noop
        // noop
        // addx -36
        // noop
        // addx 1
        // addx 7
        // noop
        // noop
        // noop
        // addx 2
        // addx 6
        // noop
        // noop
        // noop
        // noop
        // noop
        // addx 1
        // noop
        // noop
        // addx 7
        // addx 1
        // noop
        // addx -13
        // addx 13
        // addx 7
        // noop
        // addx 1
        // addx -33
        // noop
        // noop
        // noop
        // addx 2
        // noop
        // noop
        // noop
        // addx 8
        // noop
        // addx -1
        // addx 2
        // addx 1
        // noop
        // addx 17
        // addx -9
        // addx 1
        // addx 1
        // addx -3
        // addx 11
        // noop
        // noop
        // addx 1
        // noop
        // addx 1
        // noop
        // noop
        // addx -13
        // addx -19
        // addx 1
        // addx 3
        // addx 26
        // addx -30
        // addx 12
        // addx -1
        // addx 3
        // addx 1
        // noop
        // noop
        // noop
        // addx -9
        // addx 18
        // addx 1
        // addx 2
        // noop
        // noop
        // addx 9
        // noop
        // noop
        // noop
        // addx -1
        // addx 2
        // addx -37
        // addx 1
        // addx 3
        // noop
        // addx 15
        // addx -21
        // addx 22
        // addx -6
        // addx 1
        // noop
        // addx 2
        // addx 1
        // noop
        // addx -10
        // noop
        // noop
        // addx 20
        // addx 1
        // addx 2
        // addx 2
        // addx -6
        // addx -11
        // noop
        // noop
        // noop
        // """;

        var c = new Computer();

        foreach (var line in input.TrimEnd().Split("\n"))
        {
            c.RunInstruction(line);
        }

        Console.WriteLine();
        Console.WriteLine($"Total strength: {c.TotalSignalStrength}");
    }
}

public class Computer
{
    private int X = 1;
    private int Cycles = 0;
    public int TotalSignalStrength = 0;

    public void RunInstruction(string line)
    {
        switch (line[..4])
        {
            case "addx":
                var v = int.Parse(line[5..]);
                Step(2);
                X += v;
                break;
            case "noop":
                Step(1);
                break;
        }
    }

    private void Step(int cost)
    {
        for (var i = 0; i < cost; i++)
        {
            PrintPixel();
            Cycles += 1;
            if (Cycles == 20 || (Cycles + 20) % 40 == 0)
            {
                TotalSignalStrength += Cycles * X;
            }
        }
    }

    private void PrintPixel()
    {
        var x = Cycles % 40;
        if (Cycles > 0 && x == 0)
        {
            Console.WriteLine();
        }

        if (x >= X - 1 && x <= X + 1)
        {
            Console.Write("#");
        }
        else
        {
            Console.Write(" ");
        }
    }
}
