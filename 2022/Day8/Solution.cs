namespace aoc2022.Day8;

public static class Solution
{
    public static void Solve()
    {
        var input = File.ReadAllText("./input").TrimEnd();
        // const string input = """
        // 30373
        // 25512
        // 65332
        // 33549
        // 35390
        // """;

        var lines = input.TrimEnd().Split("\n");

        var forest = new Grid(lines.First().Length, lines.Length);

        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            for (var x = 0; x < line.Length; x++)
            {
                var height = line[x] - 48;
                forest.AddTree(x, y, height);
            }
        }

        // forest.Print();

        // Console.WriteLine();
        // Console.WriteLine(forest.GetVisibleTreeCount());

        Console.WriteLine(forest.GetBestScenicScore());

        // Console.WriteLine(forest.GetScenicScore(new Tree(2, 3, 5)));
    }
}

public class Grid
{
    private readonly int _width;
    private readonly int _height;
    private readonly HashSet<Tree> Trees = new();

    public Grid(int width, int height)
    {
        _width = width;
        _height = height;
    }

    public void AddTree(int x, int y, int height)
    {
        Trees.Add(new Tree(x, y, height));
    }

    public int GetVisibleTreeCount()
    {
        return Trees.Count(IsTreeVisible);
    }

    public void Print()
    {
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                var tree = Trees.First(t => t.X == x && t.Y == y);
                Console.Write(IsTreeVisible(tree) ? tree.Height : " ");
            }

            Console.WriteLine();
        }
    }

    public int GetBestScenicScore()
    {
        return Trees.Max(GetScenicScore);
    }

    private int GetScenicScore(Tree t)
    {
        var up = Trees
            .OrderByDescending(t2 => t2.Y)
            .Where(t2 => t2.X == t.X && t2.Y < t.Y);
        var down = Trees
            .OrderBy(t2 => t2.Y)
            .Where(t2 => t2.X == t.X && t2.Y > t.Y);
        var left = Trees
            .OrderByDescending(t2 => t2.X)
            .Where(t2 => t2.Y == t.Y && t2.X < t.X);
        var right = Trees
            .OrderBy(t2 => t2.X)
            .Where(t2 => t2.Y == t.Y && t2.X > t.X);

        // Console.WriteLine($"{Distance(t, up)}x{Distance(t, left)}x{Distance(t, down)}x{Distance(t, right)}");

        return Distance(t, up) * Distance(t, down) * Distance(t, left) * Distance(t, right);
    }

    private static int Distance(Tree t, IEnumerable<Tree> trees)
    {
        var count = 0;
        foreach (var t2 in trees)
        {
            count += 1;
            if (t2.Height >= t.Height)
            {
                return count;
            }
        }

        return count;
    }

    private bool IsTreeVisible(Tree t)
    {
        if (t.X == 0 || t.X == _width - 1 || t.Y == 0 || t.Y == _height - 1)
        {
            return true;
        }

        if (Trees.Where(t2 => t2.X > t.X && t2.Y == t.Y).All(t2 => t2.Height < t.Height))
        {
            return true;
        }

        if (Trees.Where(t2 => t2.X < t.X && t2.Y == t.Y).All(t2 => t2.Height < t.Height))
        {
            return true;
        }

        if (Trees.Where(t2 => t2.Y > t.Y && t2.X == t.X).All(t2 => t2.Height < t.Height))
        {
            return true;
        }

        if (Trees.Where(t2 => t2.Y < t.Y && t2.X == t.X).All(t2 => t2.Height < t.Height))
        {
            return true;
        }

        return false;
    }
}

public record Tree(int X, int Y, int Height);
