namespace aoc2022.Day12;

public static class Solution
{
    public static void Solve()
    {
        var input = File.ReadAllText("./input");
        // const string input = """
        // Sabqponm
        // abcryxxl
        // accszExk
        // acctuvwj
        // abdefghi
        // """;

        var nodes = new List<Node>();
        Node start = null;
        Node end = null;
        var x = 0;
        var y = 0;
        foreach (var line in input.TrimEnd().Split("\n"))
        {
            x = 0;
            foreach (var letter in line)
            {
                Node node;

                switch (letter)
                {
                    case 'S':
                        node = new Node(x, y, 'a', new List<Node>());
                        start = node;
                        break;
                    case 'E':
                        node = new Node(x, y, 'z', new List<Node>());
                        end = node;
                        break;
                    default:
                        node = new Node(x, y, letter, new List<Node>());
                        break;
                }

                nodes.Add(node);
                x++;
            }

            y++;
        }

        nodes = nodes.OrderByDescending(n => n.Letter).ToList();

        var climber = new Climber(nodes, end, start);
        var part1 = climber.FindShortestPath();
        Console.WriteLine($"Part 1: {part1}");
        
        var paths = new List<int>();

        foreach (var node in nodes.Where(node => node.Letter == 'a'))
        {
            paths.Add(new Climber(nodes, end, node).FindShortestPath());
        }

        Console.WriteLine($"Part 2: {paths.Min()}");
    }

    private class Climber
    {
        private readonly Dictionary<Node, int> _distances = new();
        private readonly Queue<Node> _queue = new();
        private readonly HashSet<Node> _visited = new();
        private readonly Node _end;

        public Climber(List<Node> nodes, Node start, Node end)
        {
            _end = end;

            foreach (var node in nodes)
            {
                node.Neighbours.AddRange(nodes.Where(n => IsNeighbour(node, n)));
                _distances[node] = int.MaxValue;
            }
            
            _distances[start] = 0;
            _queue.Enqueue(start);
        }

        public int FindShortestPath()
        {
            while (_queue.TryDequeue(out var node))
            {
                if (_visited.Contains(node)) continue;
                _visited.Add(node);

                foreach (var neighbour in node.Neighbours)
                {
                    var alt = _distances[node] + 1;

                    if (alt < _distances[neighbour])
                    {
                        _distances[neighbour] = alt;
                    }

                    _queue.Enqueue(neighbour);
                }
            }

            return _distances[_end];
        }
    }

    private static bool IsNeighbour(Node n1, Node n2)
    {
        var dx = Math.Abs(n1.X - n2.X);
        var dy = Math.Abs(n1.Y - n2.Y);
        var dh = n1.Letter - n2.Letter;

        if (n1 == n2) return false;

        return (dx == 1 && dy == 0 || dx == 0 && dy == 1) && dh <= 1;
    }

    private record Node(int X, int Y, char Letter, List<Node> Neighbours);
}
