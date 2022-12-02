namespace adventofcode.Day2;

public static class Solution
{
    public static void Solve()
    {
        var input = File.ReadAllText("./input").TrimEnd();
        // const string input = """
        //     A Y
        //     B X
        //     C Z
        //     """;

        var lines = input.TrimEnd().Split("\n");

        var score = lines.Sum(line => line switch
        {
            "C X" => 6 + 1,
            "A Y" => 6 + 2,
            "B Z" => 6 + 3,
            "A X" => 3 + 1,
            "B Y" => 3 + 2, 
            "C Z" => 3 + 3,
            "B X" => 0 + 1,
            "C Y" => 0 + 2,
            "A Z" => 0 + 3,
            _ => throw new ArgumentOutOfRangeException()
        });
        
        var score2 = lines.Sum(line => line switch
        {
            "C X" => 0 + 2,
            "A Y" => 3 + 1,
            "B Z" => 6 + 3,
            "A X" => 0 + 3,
            "B Y" => 3 + 2, 
            "C Z" => 6 + 1,
            "B X" => 0 + 1,
            "C Y" => 3 + 3,
            "A Z" => 6 + 2,
            _ => throw new ArgumentOutOfRangeException()
        });

        Console.WriteLine(score);
        Console.WriteLine(score2);
    }
}
