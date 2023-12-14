using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Joymg.AoC23;

public abstract class Puzzle
{
    public static string day;
    public static string part;
    public Stopwatch stopwatch;

    public Puzzle()
    {
        string data = GetType().Name;
        day = new string(data).Remove(5, 5).Remove(0, 3);
        part = new string(data).Remove(0, 9);
        stopwatch = new Stopwatch();
    }


    public abstract string SolveFile(string[] input);

    public string SolveInput()
    {
        var path = $"..\\..\\..\\Puzzles\\{day}\\input.txt";
        return SolveFile(File.ReadAllLines(path));
    }

    public string SolveExample()
    {
        var path = $"..\\..\\..\\Puzzles\\{day}\\example.in1.txt";
        return SolveFile(File.ReadAllLines(path));
    }
}

public abstract class LinesPuzzle : Puzzle
{
    public override string SolveFile(string[] input)
    {
        return Solve(input);
    }

    protected abstract string Solve(string[] puzzleInput);
}