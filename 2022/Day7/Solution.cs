namespace aoc2022.Day7;

public static class Solution
{
    public static void Solve()
    {
        var input = File.ReadAllText("./input").TrimEnd();
        // const string input = """
        //     $ cd /
        //     $ ls
        //     dir a
        //     14848514 b.txt
        //     8504156 c.dat
        //     dir d
        //     $ cd a
        //     $ ls
        //     dir e
        //     29116 f
        //     2557 g
        //     62596 h.lst
        //     $ cd e
        //     $ ls
        //     584 i
        //     $ cd ..
        //     $ cd ..
        //     $ cd d
        //     $ ls
        //     4060174 j
        //     8033020 d.log
        //     5626152 d.ext
        //     7214296 k
        //     """;

        var root = new AocDirectory("/");
        var cwd = root;

        foreach (var line in input.TrimEnd().Split("\n").Skip(1))
        {
            var l = line.Split(" ");

            switch (l[0])
            {
                case "$":
                {
                    if (l[1] == "cd")
                    {
                        var dir = l[2];
                        cwd = dir == ".." ? cwd!.Parent : cwd!.Directories.Single(d => d.Name == dir);
                    }

                    continue;
                }
                case "dir":
                    cwd!.AddDirectory(new AocDirectory(l[1], cwd));
                    break;
                default:
                    cwd!.AddFile(new AocFile(int.Parse(l[0])));
                    break;
            }
        }

        var sizes = new List<int>();
        CollectSizes(root, sizes);

        var sum = sizes.Where(x => x <= 100_000).Sum();
        Console.WriteLine(sum);

        var spaceNeeded = 30_000_000 - (70_000_000 - root.Size);
        var size = sizes.Order().First(s => s >= spaceNeeded);

        Console.WriteLine(size);
    }

    private static void CollectSizes(AocDirectory? directory, ICollection<int> sizes)
    {
        if (directory == null)
        {
            return;
        }

        sizes.Add(directory.Size);

        foreach (var dir in directory.Directories)
        {
            CollectSizes(dir, sizes);
        }
    }

    private record AocFile(int Size);

    private record AocDirectory(string Name, AocDirectory? Parent = null)
    {
        private ICollection<AocFile> Files { get; } = new List<AocFile>();
        public ICollection<AocDirectory> Directories { get; } = new List<AocDirectory>();
        public int Size => Directories.Sum(d => d.Size) + Files.Sum(f => f.Size);
        public void AddDirectory(AocDirectory dir) => Directories.Add(dir);
        public void AddFile(AocFile file) => Files.Add(file);
    }
}
