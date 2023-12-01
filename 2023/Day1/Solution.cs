using System.Text.RegularExpressions;

namespace aoc2023.Day1;

public static partial class Solution
{
    public static void Solve()
    {
        var input = File.ReadAllText("./input").TrimEnd();
        // const string input = """
        //     1abc2
        //     pqr3stu8vwx
        //     a1b2c3d4e5f
        //     treb7uchet
        //     """;
        // const string input = """
        //     two1nine
        //     eightwothree
        //     abcone2threexyz
        //     xtwone3four
        //     4nineeightseven2
        //     zoneight234
        //     7pqrstsixteen
        //     """;

        var sum1 = input.Split("\n").Select(r => CreateNumber(r)).Sum();
        var sum2 = input.Split("\n").Select(r => CreateNumber(r, part2: true)).Sum();

        Console.WriteLine(sum1);
        Console.WriteLine(sum2);
    }

    private static int CreateNumber(string r, bool part2 = false)
    {
        var numbers = GetNumbers(r, part2).ToList();
        var num1 = numbers.FirstOrDefault(0);
        var num2 = numbers.LastOrDefault(0);

        var number = int.Parse(num1 + "" + num2);

        return number;
    }

    private static IEnumerable<int> GetNumbers(string str, bool part2)
    {
        return (part2 ? NumbersAndStringRegex() : NumbersRegex())
            .Matches(str)
            .Select(m => ToNumber(m.Value));
    }

    private static int ToNumber(string str) =>
        str switch
        {
            "1" or "on" => 1, // Marvelous stupid code yes but it works
            "2" or "tw" => 2,
            "3" or "thre" => 3,
            "4" or "four" => 4,
            "5" or "fiv" => 5,
            "6" or "six" => 6,
            "7" or "seve" => 7,
            "8" or "eigh" => 8,
            "9" or "nin" => 9
        };

    // Have you seen a better regex?
    [GeneratedRegex(
        "(1|on(?=e)|2|tw(?=o)|3|thre(?=e)|4|four|5|fiv(?=e)|6|six|7|seve(?=n)|8|eigh(?=t)|9|nin(?=e))"
    )]
    private static partial Regex NumbersAndStringRegex();

    // who needs \d anyways
    [GeneratedRegex("(1|2|3|4|5|6|7|8|9)")]
    private static partial Regex NumbersRegex();
}
