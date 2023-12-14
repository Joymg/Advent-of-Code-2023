using System.Text;

namespace Joymg.AoC23;

public static class AOC
{
    public static void RunExample()
    {
        RunPuzzle("example", true);
    }

    public static void RunInput()
    {
        RunPuzzle("input", false);
    }

    private static void RunPuzzle(string fileName, bool checkMandatory)
    {
        RunFile(Puzzle.day, Puzzle.part, fileName, checkMandatory);
    }

    private static void RunFile(string day, string part, string fileName, bool checkMandatory)
    {
        string root = "Puzzles";

        string path, checkPath;
        if (checkMandatory)
        {
            path = $"../../../{root}/{day}/{fileName}.in{part}.txt";
            checkPath = $"../../../{root}/{day}/{fileName}.check{part}.txt";
            Startup.CreateIfDontExist(checkPath);
        }
        else
        {
            path = $"../../../{root}/{day}/{fileName}.txt";
            checkPath = "";
        }

        var outputPath = $"../../../{root}/{day}/{fileName}.out{part}.txt";
        Startup.CreateIfDontExist(outputPath);


        var computeName = $"day: {day} part: {part} file: {fileName}";

        var computedSolution = Compute(path);
        CheckSolution(computedSolution, outputPath, checkPath, computeName, checkMandatory);

        Console.WriteLine($"Computed Solution for day:{day} part:{part} file: {fileName} is {computedSolution}");
    }

    private static string Compute(string inputFile)
    {
        if (inputFile.Length == 0)
        {
            Console.WriteLine($"Empty Input for {inputFile} using {App.puzzle}");
        }

        string foundSolution = App.puzzle.SolveFile(File.ReadAllLines(inputFile));
        if (string.IsNullOrEmpty(foundSolution))
        {
            Console.WriteLine($"Empty solution for {inputFile} using {App.puzzle}");
        }

        return foundSolution;
    }

    private static void CheckSolution(string computedSolution, string outputFile, string checkFile,
        string computeName, bool checkMandatory)
    {
        if (File.Exists(checkFile))
        {
            string expectedSolution = File.ReadAllLines(checkFile)[0].Trim();
            if (string.IsNullOrEmpty(expectedSolution))
            {
                Console.WriteLine($"Check file is empty for {computeName}");
                return;
            }

            if (expectedSolution != computedSolution)
            {
                Console.WriteLine(
                    $"Wrong Solution for {computeName} with {computedSolution}, expected is {expectedSolution}");
            }
            else
            {
                Console.WriteLine($"Correct Solution for {computeName} using {App.puzzle} is {computedSolution}");
            }
        }
        else
        {
            if (checkMandatory)
            {
                if (checkMandatory)
                {
                    throw new Exception($"Check file not found but mandatory for $computeName");
                }
            }
            Console.WriteLine($"Unchecked Solution for {computedSolution} using ${App.puzzle} is {computedSolution}");
        }

        CheckAndAppendToOutput(outputFile, computedSolution);
    }

    private static void CheckAndAppendToOutput(string outputFile, string computedSolution)
    {
        computedSolution += $" {App.puzzle.stopwatch.ElapsedMilliseconds}";
        File.WriteAllBytes(outputFile, Encoding.ASCII.GetBytes(computedSolution));
    }
}