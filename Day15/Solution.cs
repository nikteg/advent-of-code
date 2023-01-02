using System.Numerics;
using System.Text.RegularExpressions;

namespace adventofcode.Day15;

public static class Solution
{
    public static void Solve()
    {
        var input = File.ReadAllText("./Day15/input");
        const string testInput = """
        Sensor at x=2, y=18: closest beacon is at x=-2, y=15
        Sensor at x=9, y=16: closest beacon is at x=10, y=16
        Sensor at x=13, y=2: closest beacon is at x=15, y=3
        Sensor at x=12, y=14: closest beacon is at x=10, y=16
        Sensor at x=10, y=20: closest beacon is at x=10, y=16
        Sensor at x=14, y=17: closest beacon is at x=10, y=16
        Sensor at x=8, y=7: closest beacon is at x=2, y=10
        Sensor at x=2, y=0: closest beacon is at x=2, y=10
        Sensor at x=0, y=11: closest beacon is at x=2, y=10
        Sensor at x=20, y=14: closest beacon is at x=25, y=17
        Sensor at x=17, y=20: closest beacon is at x=21, y=22
        Sensor at x=16, y=7: closest beacon is at x=15, y=3
        Sensor at x=14, y=3: closest beacon is at x=15, y=3
        Sensor at x=20, y=1: closest beacon is at x=15, y=3
        """;

        // input = testInput;

        var regex = new Regex(
            @"Sensor at x=(-?\d+), y=(-?\d+): closest beacon is at x=(-?\d+), y=(-?\d+)"
        );

        var map = new Map();

        foreach (var line in input.TrimEnd().Split("\n"))
        {
            var match = regex.Match(line);
            var sx = int.Parse(match.Groups[1].Value);
            var sy = int.Parse(match.Groups[2].Value);
            var bx = int.Parse(match.Groups[3].Value);
            var by = int.Parse(match.Groups[4].Value);

            map.AddSensor(sx, sy, bx, by);
        }

        // map.PrintGrid();
        // var count = map.GetImpossiblePositions(10);
        var count = map.GetImpossiblePositions(2000000);
        Console.WriteLine(count);
        var distressSignalTuningFrequency = map.GetDistressSignal(40_00_000);
        Console.WriteLine(distressSignalTuningFrequency);
    }
}

public class Map
{
    private readonly HashSet<(int x, int y, int range)> _sensors = new();
    private readonly HashSet<(int x, int y)> _beacons = new();

    public void AddSensor(int x, int y, int bx, int by)
    {
        _sensors.Add((x, y, range: GetDistance(x, y, bx, by)));
        _beacons.Add((bx, by));
    }

    public int GetImpossiblePositions(int y)
    {
        var minX = _sensors.Select(s => s.x - s.range).Min();
        var maxX = _sensors.Select(s => s.x + s.range).Max();

        var totalCount = 0;
        for (var x = minX; x <= maxX; x++)
        {
            if (_beacons.Contains((x, y)))
            {
                continue;
            }

            var anySensorInRange = _sensors.Any(s => GetDistance(x, y, s.x, s.y) <= s.range);
            if (anySensorInRange)
            {
                totalCount++;
            }
        }

        return totalCount;
    }

    public BigInteger GetDistressSignal(int max)
    {
        for (var y = 0; y < max; y++)
        {
            var x = 0;
            while (x < max)
            {
                var found = false;
                foreach (var sensor in _sensors)
                {
                    if (GetDistance(x, y, sensor.x, sensor.y) <= sensor.range)
                    {
                        x = sensor.x - Math.Abs(sensor.y - y) + sensor.range + 1;

                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    return GetTuningFrequency(x, y);
                }
            }
        }

        return 0;
    }

    private static BigInteger GetTuningFrequency(int x, int y)
    {
        return new BigInteger(x) * 4_000_000 + y;
    }

    private static int GetDistance(int x1, int y1, int x2, int y2)
    {
        return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
    }
}
