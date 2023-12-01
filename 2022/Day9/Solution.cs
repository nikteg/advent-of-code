namespace aoc2022.Day9;

public static class Solution
{
    public static void Solve()
    {
        var input = File.ReadAllText("./input").TrimEnd();
        // const string input = """
        // R 5
        // U 8
        // L 8
        // D 3
        // R 17
        // D 10
        // L 25
        // U 20
        // """;

        const bool part2 = true;
        var snake = new Snake(part2 ? 9 : 1);

        foreach (var line in input.TrimEnd().Split("\n"))
        {
            snake.Move(line[0], int.Parse(line[2..]));
        }

        snake.PrintVisitedCount();
    }

    private class Snake
    {
        private readonly Head Head = new();
        private readonly List<Tail> Tails = new();
        private readonly HashSet<Position> Visited = new();

        public Snake(int length)
        {
            Part parent = Head;
            foreach (var _ in Enumerable.Range(0, length))
            {
                var tail = new Tail(parent);
                Tails.Add(tail);
                parent = tail;
            }
        }

        public void Move(char direction, int steps)
        {
            foreach (var _ in Enumerable.Range(0, steps))
            {
                Head.Move(direction);

                foreach (var tail in Tails)
                {
                    tail.Follow();
                }

                Visited.Add(Tails.Last().Position);
            }
        }

        public void PrintVisitedCount()
        {
            Console.WriteLine(Visited.Count);
        }
    }

    private record Position
    {
        public int X;
        public int Y;
    }

    private abstract class Part
    {
        public readonly Position Position = new();
    }

    private class Head : Part
    {
        public void Move(char direction)
        {
            switch (direction)
            {
                case 'R':
                    Position.X++;
                    break;
                case 'L':
                    Position.X--;
                    break;
                case 'U':
                    Position.Y--;
                    break;
                case 'D':
                    Position.Y++;
                    break;
            }
        }
    }

    private class Tail : Part
    {
        private readonly Part Parent;

        public Tail(Part parent)
        {
            Parent = parent;
        }

        public void Follow()
        {
            var dx = Parent.Position.X - Position.X;
            var dy = Parent.Position.Y - Position.Y;
            var distance = Math.Max(Math.Abs(dx), Math.Abs(dy));

            if (distance <= 1) return;

            Position.X += Math.Sign(dx);
            Position.Y += Math.Sign(dy);
        }
    }
}
