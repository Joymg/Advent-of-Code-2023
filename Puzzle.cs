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
        string[] splitData = data.Split('_');
        day = splitData[0].Remove(0, 3);
        part = splitData[1];
    }


    public abstract string SolveFile(string[] input);

    public string SolveInput()
    {
        return SolveFile(File.ReadAllLines($@"..\..\..\{day}\input.txt"));
    }

    public string SolveExample()
    {
        return SolveFile(File.ReadAllLines($@"..\..\..\{day}\example.in.txt"));
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