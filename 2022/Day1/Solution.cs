namespace aoc2022.Day1;

public static class Solution
{
    public static void Solve()
    {
        var input = File.ReadAllText("./input").TrimEnd();
//         const string input = """
//             1000
//             2000
//             3000
//
//             4000
//
//             5000
//             6000
//
//             7000
//             8000
//             9000
//
//             10000
//         """;
        
        var largest = input.Split("\n\n").Select(c => c.Split("\n")).Select(elf => elf.Select(int.Parse).Sum()).Max();
        var largest2 = input.Split("\n\n").Select(c => c.Split("\n")).Select(elf => elf.Select(int.Parse).Sum()).OrderDescending().Take(3).Sum();

        Console.WriteLine(largest);
        Console.WriteLine(largest2);
    }
}
