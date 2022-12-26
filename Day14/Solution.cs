namespace adventofcode.Day14;

public static class Solution
{
    public static void Solve()
    {
        var input = File.ReadAllText("./Day14/input");
        // const string input = """
        //   498,4 -> 498,6 -> 496,6
        //   503,4 -> 502,4 -> 502,9 -> 494,9
        //   """;

        var map = new Map();

        foreach (
            var pos in input
                .TrimEnd()
                .Split("\n")
                .Select(
                    line =>
                        line.Split(" -> ")
                            .Select(position =>
                            {
                                var pos = position.Split(",").Select(int.Parse).ToArray();

                                return (pos[0], pos[1]);
                            })
                )
        )
        {
            foreach (var (v1, v2) in pos.Pairwise())
            {
                map.AddRockLine(v1, v2);
            }
        }

        var sandCount = map.SimulateSand();

        Console.WriteLine(sandCount);
    }
}

public class Map
{
    private readonly HashSet<(int, int)> _grid = new();
    private int _floorLevel;

    public void AddRockLine((int x, int y) v1, (int x, int y) v2)
    {
        if (v1.x == v2.x)
        {
            AddRockLine(v1.y, v2.y, y => (v1.x, y));
        }
        else if (v1.y == v2.y)
        {
            AddRockLine(v1.x, v2.x, x => (x, v1.y));
        }

        var floorLevel = Math.Max(v1.y, v2.y);
        _floorLevel = Math.Max(_floorLevel, floorLevel);
    }

    private void AddRockLine(int v1, int v2, Func<int, (int, int)> adder)
    {
        var max = Math.Max(v1, v2);
        var min = Math.Min(v1, v2);
        var diff = max - min + 1;

        foreach (var v in Enumerable.Range(min, diff))
        {
            _grid.Add(adder(v));
        }
    }

    public void PrintGrid()
    {
        foreach (var y in Enumerable.Range(0, 10))
        {
            foreach (var x in Enumerable.Range(493, 12))
            {
                Console.Write(_grid.Contains((x, y)) ? "#" : " ");
            }
            Console.WriteLine();
        }
    }

    public int SimulateSand(int sandCount = 0)
    {
        while (true)
        {
            var x = 500;
            var y = 0;

            while (!_grid.Contains((x, y + 1)))
            {
                if (_grid.Contains((x, y + 2)))
                {
                    if (!_grid.Contains((x - 1, y + 2)))
                    {
                        x--;
                    }
                    else if (!_grid.Contains((x + 1, y + 2)))
                    {
                        x++;
                    }
                }

                y++;

                if (y == _floorLevel)
                {
                    return sandCount;
                }
            }

            _grid.Add((x, y));
            sandCount += 1;
        }
    }
}

public static class ExtensionMethods
{
    public static IEnumerable<(T, T)> Pairwise<T>(this IEnumerable<T> source)
    {
        var previous = default(T);
        using (var it = source.GetEnumerator())
        {
            if (it.MoveNext())
                previous = it.Current;

            while (it.MoveNext())
                yield return (previous, previous = it.Current);
        }
    }
}
