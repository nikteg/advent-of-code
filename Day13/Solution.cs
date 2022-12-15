using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace adventofcode.Day13;

public static class Solution
{
    public static void Solve()
    {
        var input = File.ReadAllText("./input");
//          const string input = """
//          [1,1,3,1,1]
//          [1,1,5,1,1]
//
//          [[1],[2,3,4]]
//          [[1],4]
//
//          [9]
//          [[8,7,6]]
//
//          [[4,4],4,4]
//          [[4,4],4,4,4]
//
//          [7,7,7,7]
//          [7,7,7]
//
//          []
//          [3]
//
//          [[[]]]
//          [[]]
//
//          [1,[2,[3,[4,[5,6,7]]]],8,9]
//          [1,[2,[3,[4,[5,6,0]]]],8,9]
//          """;

        // var rightOrderPairs = new HashSet<int>();
        // var index = 0;
        // foreach (var groups in input.TrimEnd().Split("\n\n"))
        // {
        //     var line = groups.Split("\n");
        //     var left = JsonNode.Parse(line[0])!;
        //     var right = JsonNode.Parse(line[1])!;
        //
        //     index++;
        //
        //     // if (index == 3)
        //     {
        //         Console.WriteLine($"== Pair {index} ==");
        //         if (Walk(left, right) == Ordering.Lt)
        //         {
        //             rightOrderPairs.Add(index);
        //         }
        //         Console.WriteLine();
        //     }
        // }
        //
        // Debugging.Dump(rightOrderPairs);
        // Debugging.Dump(rightOrderPairs.Sum());
        
        var lines = input.TrimEnd().Split("\n\n").SelectMany(l => l.Split("\n").Select(q => JsonNode.Parse(q)!)).ToList();
        lines.Sort((a, b) => (int)Walk(a, b));
        // Debugging.Dump(lines);

        foreach (var jn in lines)
        {
            Debugging.Dump(jn);
            // Find [[2]] and [[6]] in a file
        }
    }

    private static Ordering Walk(JsonNode? left, JsonNode? right)
    {
        if (left == null)
        {
            Console.WriteLine("Left side ran out of items, so inputs are in the right order");
            return Ordering.Lt;
        }

        if (right == null)
        {
            Console.WriteLine("Right side ran out of items, so inputs are not in the right order");
            return Ordering.Gt;
        }

        if (left is JsonValue && right is JsonValue)
        {
            var l = left.GetValue<int>();
            var r = right.GetValue<int>();

            Console.WriteLine($"Compare {l} vs {r}");

            if (l < r)
            {
                Console.WriteLine("Left side is smaller, so inputs are in the right order");
                return Ordering.Lt;
            }

            if (l > r)
            {
                return Ordering.Gt;
            }

            return Ordering.Eq;
        }

        if (left is JsonArray && right is JsonArray)
        {
            Console.WriteLine($"Compare {left.Print()} vs {right.Print()}");
            var largest = Math.Max(left.AsArray().Count, right.AsArray().Count);
            foreach (var i in Enumerable.Range(0, largest))
            {
                var ordering = Walk(left.AsArray().ElementAtOrDefault(i), right.AsArray().ElementAtOrDefault(i));
                if (ordering != Ordering.Eq) return ordering;
            }

            return Ordering.Eq;
        }

        if (left is JsonArray && right is JsonValue)
        {
            return Walk(left, new JsonArray(right.GetValue<int>()));
        }

        if (left is JsonValue && right is JsonArray)
        {
            return Walk(new JsonArray(left.GetValue<int>()), right);
        }

        throw new ArgumentOutOfRangeException("Tråkigt");
    }
}

public enum Ordering
{
    Lt = -1,
    Eq = 0,
    Gt = 1,
}

public static class JsonExtensions
{
    private static readonly Regex StripWhitespace = new(@"\s+");

    public static string Print(this JsonNode node)
    {
        return StripWhitespace.Replace(node.ToString(), "");
    }
}
