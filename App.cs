namespace Joymg.AoC23;

public class App
{
    public static string AOC_YEAR = "2023";
    public static Puzzle puzzle ;
    
    static void Main(string[] args)
    {
        puzzle.stopwatch.Start();
        Console.WriteLine($"{puzzle.SolveExample()}");
        puzzle.stopwatch.Stop();
        Console.WriteLine($"{puzzle.stopwatch.ElapsedMilliseconds}");
    }
}