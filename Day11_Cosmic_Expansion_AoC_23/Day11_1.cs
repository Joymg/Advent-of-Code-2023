using System.Diagnostics;
using static Joymg.AoC23.Utils;

namespace Joymg.AoC23.Day11;

public class Day11_1
{
    static string[] inputs;
    const string inputFolderPath = "..\\..\\..\\Day11_Cosmic_Expansion_AoC_23\\Input\\";
    static long result;

    public static void Run()
    {
        Stopwatch sw = Stopwatch.StartNew();
        inputs = ReadFile(inputFolderPath, InputType.First);

        Solve(inputs);

        Console.WriteLine(result);

        sw.Stop();
        Console.WriteLine(sw.ElapsedMilliseconds);
    }

    private static void Solve(string[] inputs)
    {
        char[][] data = MatrixExtensions.ParseInputAsChar(inputs);

        Matrix<char> cosmicSnapshot = new Matrix<char>(data);
        cosmicSnapshot = CosmicSnapshot.ExpandUniverse(cosmicSnapshot);
        var galaxyLocations = GalaxyLocator.Find(cosmicSnapshot);
        result = ManhattanCalculator.DistanceBetweenElements(galaxyLocations);

    }

    
}