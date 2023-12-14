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

        FileStream inputFile = File.Open($"../../../{root}/{day}/{fileName}.in{part}.txt", FileMode.OpenOrCreate);

        var outputFile = File.Open($"../../../{root}/{day}/{fileName}.out{part}.txt", FileMode.OpenOrCreate);
        var checkFile = File.Open($"../../../{root}/{day}/{fileName}.check{part}.txt", FileMode.OpenOrCreate);

        var computeName = $"day: {day} part: {part} file: {fileName}";

        var computedSolution = Compute(inputFile);

        CheckSolution(computedSolution, outputFile, checkFile, computeName, checkMandatory);
        Console.WriteLine($"Computed Solution for day:{day} part:{part} file: {fileName} is {computedSolution}");
    }

    private static string Compute(FileStream inputFile)
    {
        if (inputFile.Length == 0)
        {
            Console.WriteLine($"Empty Input for {inputFile} using {App.puzzle}");
        }

        string foundSolution = App.puzzle.SolveFile(File.ReadAllLines(inputFile.ToString()));
        if (string.IsNullOrEmpty(foundSolution))
        {
            Console.WriteLine($"Empty solution for {inputFile} using {App.puzzle}");
        }

        return foundSolution;
    }

    private static void CheckSolution(string computedSolution, FileStream outputFile, FileStream checkFile,
        string computeName, bool checkMandatory)
    {
        if (File.Exists(checkFile.ToString()))
        {
            string expectedSolution = File.ReadAllLines(checkFile.ToString())[0].Trim();
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

                Console.WriteLine(
                    $"Unchecked Solution for {computedSolution} using ${App.puzzle} is {computedSolution}");
            }
        }

        CheckAndAppendToOutput(outputFile, computedSolution);
    }

    private static void CheckAndAppendToOutput(FileStream outputFile, string computedSolution)
    {
        outputFile.Write(Encoding.ASCII.GetBytes(computedSolution));
    }
}