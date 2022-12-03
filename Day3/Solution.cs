using System.Diagnostics;

namespace adventofcode.Day3;

public static class Solution
{
    public static void Solve()
    {
        var input = File.ReadAllText("./input");
        // const string input = """
        //     vJrwpWtwJgWrhcsFMMfFFhFp
        //     jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
        //     PmmdzqPrVvPwwTWBwg
        //     wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
        //     ttgJtRGJQctTZtZT
        //     CrZsJsPPZsGzwwsLwLmpwMDw
        //     """;

        Debugging.Assert(Priority('a'), 1);
        Debugging.Assert(Priority('z'), 26);
        Debugging.Assert(Priority('A'), 27);
        Debugging.Assert(Priority('Z'), 52);

        var lines = input.TrimEnd().Split("\n");

        var rucksacks = lines.Select(line => line.Select(Priority).Chunk(line.Length / 2).ToList()); // [ [[1 2 3], [4 2 5]], ... ]
        var sum = rucksacks.Sum(sack => sack[0].Intersect(sack[1]).ToList().First());
        
        var elvesTriples = lines.Select(line => line.Select(Priority)).Chunk(3); // [ [[1 2 3], [4 2 5], [6 7 2]], ... ]
        var sum2 = elvesTriples.Sum(elves => elves[0].Intersect(elves[1]).Intersect(elves[2]).First());
        
        // Debugging.Assert(sum, 157);
        // Debugging.Assert(sum2, 70);

        Console.WriteLine(sum);
        Console.WriteLine(sum2);
    }

    private static int Priority(char c)
    {
        // a = 97 => 1
        // z = 122 => 26
        // A = 65 => 27

        if (c >= 96)
        {
            return c - 96; // a-z
        }

        return c - 38; // A-Z
    }
}
