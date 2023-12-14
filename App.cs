using Joymg.AoC23.Puzzles;

namespace Joymg.AoC23;

public static class App
{
    public static string AOC_YEAR = "2023";
    public static Puzzle puzzle = new Day14Part2();

    static void Main(string[] args)
    {
        puzzle.stopwatch.Start();
        AOC.RunInput();
        puzzle.stopwatch.Stop();
        Console.WriteLine($"{puzzle.stopwatch.ElapsedMilliseconds}");
    }
}