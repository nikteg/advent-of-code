using shared;

namespace aoc2023.Day5;

public static class Solution
{
    public static void Solve()
    {
        var input = File.ReadAllText("./input").TrimEnd();
        const string sample = """
            seeds: 79 14 55 13
            
            seed-to-soil map:
            50 98 2
            52 50 48
            
            soil-to-fertilizer map:
            0 15 37
            37 52 2
            39 0 15
            
            fertilizer-to-water map:
            49 53 8
            0 11 42
            42 0 7
            57 7 4
            
            water-to-light map:
            88 18 7
            18 25 70
            
            light-to-temperature map:
            45 77 23
            81 45 19
            68 64 13
            
            temperature-to-humidity map:
            0 69 1
            1 0 69
            
            humidity-to-location map:
            60 56 37
            56 93 4
            """;

        // input = sample;

        var lines = input.Split("\n");

        var seeds = lines[0].Split(new char[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries)[
            1..
        ]
            .Select(long.Parse)
            .ToList();

        var maps = input.Split("\n\n")[1..]
            .Select(
                str =>
                    str.Split("\n")[1..]
                        .Select(str2 => str2.Split(" ").Select(long.Parse).ToList())
                        .ToList()
            )
            .ToList();

        var lowest = seeds
            .Select(seed => maps.Aggregate(seed, MapSeed))
            .Aggregate(long.MaxValue, long.Min);

        Console.WriteLine(lowest);
    }

    private static long MapSeed(long seed, IReadOnlyList<IReadOnlyList<long>> nums)
    {
        foreach (var num in nums)
        {
            var dst = num[0];
            var src = num[1];
            var len = num[2];

            if (seed >= src && seed <= src + len)
            {
                return seed + dst - src;
            }
        }

        return seed;
    }
}
