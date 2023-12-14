using System.Diagnostics;
using static Joymg.AoC23.Utils;

namespace Joymg.AoC23.Day12;

public class Day12_1
{
    static string[] inputs;
    const string inputFolderPath = "..\\..\\..\\Day12_HotSprings_AoC_23\\Input\\";
    static long result;

    public static void Run()
    {
        Stopwatch sw = Stopwatch.StartNew();
        inputs = ReadFile(inputFolderPath, InputType.Test);

        Solve(inputs);

        Console.WriteLine(result);

        sw.Stop();
        Console.WriteLine(sw.ElapsedMilliseconds);
    }

    private static void Solve(string[] inputs)
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            (char[] springs, int[] damagedSpringSizes) = ParseInput(inputs[i]);
            SpringRecord springRecord = new SpringRecord(damagedSpringSizes, springs);
            result += ArrangementCounter.GetPossibleArrangements(springRecord);
        }
    }

    private static (char[] springs, int[] damagedSpringSizes) ParseInput(string input)
    {
        string[] split = input.Split(" ");
        var charArray = split[0].ToCharArray();
        var intArray = split[1].Split(',').Where(x => !string.IsNullOrEmpty(x) && !string.IsNullOrWhiteSpace(x))
            .Select(int.Parse).ToArray();
        return (charArray, intArray);
    }

    
}

