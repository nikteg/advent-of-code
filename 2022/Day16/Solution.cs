using System.Text.RegularExpressions;

namespace aoc2022.Day16;

public static class Solution
{
    public static void Solve()
    {
        var input = File.ReadAllText("./Day16/input");
        var sampleInput = File.ReadAllText("./Day16/input");

        input = sampleInput;

        var regex = new Regex(
            @"Valve ([A-Z]+) has flow rate=(\d+); tunnels? leads? to valves? ([A-Z, ]+)"
        );

        var ts = new TunnelSystem();

        foreach (var line in input.TrimEnd().Split("\n"))
        {
            var match = regex.Match(line);
            var valve = match.Groups[1].Value;
            var flowRate = int.Parse(match.Groups[2].Value);
            var connectingValves = match.Groups[3].Value.Split(", ");

            if (flowRate > 0)
            {
                ts.AddValve(valve, flowRate, connectingValves);
            }
        }

        ts.GetMostPressure(30);
    }
}

public record Valve(string Id, int FlowRate, string[] ConnectingValvesIds);

public record State(string Current, int Pressure, int Time, string[] Opened);

public class TunnelSystem
{
    private readonly Dictionary<string, Valve> _valves = new();

    public void AddValve(string id, int flowRate, string[] connectingValvesIds)
    {
        _valves.Add(id, new Valve(id, flowRate, connectingValvesIds));
    }

    public void GetMostPressure(int timeLimit) { }
}
