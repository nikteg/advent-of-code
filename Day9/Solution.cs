namespace adventofcode.Day9;

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

        var snake = new Snake(9);

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
            for (var i = 0; i < length; i++)
            {
                var tail = new Tail(parent);
                Tails.Add(tail);
                parent = tail;
            }
        }

        public void Move(char direction, int steps)
        {
            for (var i = 0; i < steps; i++)
            {
                Head.Move(direction);
                foreach (var tail in Tails)
                {
                    tail.Move();
                }

                Visited.Add(Tails.Last().Position);
            }
        }

        public void Print()
        {
            for (var y = -4; y < 1; y++)
            {
                for (var x = 0; x < 6; x++)
                {
                    if (Head.Position.X == x && Head.Position.Y == y)
                    {
                        Console.Write("H");
                    }
                    else if (Tails.Any(t => t.Position.X == x && t.Position.Y == y))
                    {
                        Console.Write("T");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine();
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

        public void Move()
        {
            // ..H
            // ..X
            // .T.
            if (Parent.Position.X > Position.X && Parent.Position.Y < Position.Y - 1)
            {
                Position.X++;
                Position.Y--;
            }
            // H..
            // X..
            // .T.
            else if (Parent.Position.X < Position.X && Parent.Position.Y < Position.Y - 1)
            {
                Position.X--;
                Position.Y--;
            }
            // .T.
            // ..X
            // ..H
            else if (Parent.Position.X > Position.X && Parent.Position.Y > Position.Y + 1)
            {
                Position.X++;
                Position.Y++;
            }
            // .T.
            // X..
            // H..
            else if (Parent.Position.X < Position.X && Parent.Position.Y > Position.Y + 1)
            {
                Position.X--;
                Position.Y++;
            }
            // ..T
            // HX.
            // ...
            else if (Parent.Position.Y > Position.Y && Parent.Position.X < Position.X - 1)
            {
                Position.X--;
                Position.Y++;
            }
            // ...
            // HX.
            // ..T
            else if (Parent.Position.Y < Position.Y && Parent.Position.X < Position.X - 1)
            {
                Position.X--;
                Position.Y--;
            }
            // T..
            // .XH
            // ...
            else if (Parent.Position.Y > Position.Y && Parent.Position.X > Position.X + 1)
            {
                Position.X++;
                Position.Y++;
            }
            // ...
            // .XH
            // T..
            else if (Parent.Position.Y < Position.Y && Parent.Position.X > Position.X + 1)
            {
                Position.X++;
                Position.Y--;
            }
            // ...
            // TXH
            // ...
            else if (Parent.Position.X > Position.X + 1)
            {
                Position.X++;
            }
            // ...
            // HXT
            // ...
            else if (Parent.Position.X < Position.X - 1)
            {
                Position.X--;
            }
            // .T.
            // .X.
            // .H.
            else if (Parent.Position.Y > Position.Y + 1)
            {
                Position.Y++;
            }
            // .H.
            // .X.
            // .T.
            else if (Parent.Position.Y < Position.Y - 1)
            {
                Position.Y--;
            }
        }
    }
}
