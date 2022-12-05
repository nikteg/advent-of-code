namespace adventofcode.Day5;

public static class Solution
{
    public static void Solve()
    {
        // var input = File.ReadAllText("./input");
        const string input = """
                 [D]    
             [N] [C]    
             [Z] [M] [P]
              1   2   3 

             move 1 from 2 to 1
             move 3 from 1 to 3
             move 2 from 2 to 1
             move 1 from 1 to 2
             """;

        var lines = input.TrimEnd().Split("\n");

        var phase = 0;
        var stacks = new List<Stack<char>>();
        var stacksBuffer = new List<List<char>>();
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            if (line.StartsWith(" 1")) // Setup phase done, put crates into stacks in correct order (reversed)
            {
                foreach (var buffer in stacksBuffer)
                {
                    buffer.Reverse();
                    stacks.Add(new Stack<char>(buffer));
                }

                phase = 1;
                continue;
            }

            switch (phase)
            {
                case 0: // Setup phase, collect crates
                {
                    var index = 0;
                    var chunks = line.Chunk(4).ToList();

                    // Debugging.Assert(chunks.Count, 3);

                    if (stacksBuffer.Count == 0)
                    {
                        stacksBuffer.AddRange(chunks.Select(_ => new List<char>()));
                    }

                    // Debugging.Assert(stacksBuffer.Count, 3);

                    foreach (var chunk in chunks)
                    {
                        var item = chunk[1];
                        if (item != ' ')
                        {
                            stacksBuffer[index].Add(item);
                        }

                        index++;
                    }

                    break;
                }
                case 1: // Done with the crates, start reading moving instructions
                    var strings = line.Split(" ");

                    switch (strings)
                    {
                        case ["move", var count, "from", var from, "to", var to]:
                            UpdateStacks(stacks, int.Parse(count), int.Parse(from), int.Parse(to));
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
            }
        }

        Console.WriteLine(PeekStacks(stacks));
    }

    private static void UpdateStacks(List<Stack<char>> stacks, int count, int from, int to)
    {
        var buffer = new List<char>();
        var toStack = stacks[to - 1];
        var fromStack = stacks[from - 1];

        for (var i = 0; i < count; i++)
        {
            buffer.Add(fromStack.Pop());
        }

        // Uncomment for part 2
        // buffer.Reverse();

        for (var i = 0; i < count; i++)
        {
            toStack.Push(buffer[i]);
        }
    }

    private static string PeekStacks(List<Stack<char>> stacks)
    {
        return stacks.Aggregate("", (current, stack) => current + stack.Peek());
    }
}
