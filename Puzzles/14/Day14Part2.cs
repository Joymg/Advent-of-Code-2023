using Joymg.AoC23.Day11;

namespace Joymg.AoC23.Puzzles;

public class Day14Part2 : LinesPuzzle
{
    protected override string Solve(string[] puzzleInput)
    {
        Matrix<char> puzzleMatrix = new Matrix<char>(MatrixExtensions.ParseInputAsChar(puzzleInput));
        
        puzzleMatrix = puzzleMatrix.Cycle(1000000000);

        return puzzleMatrix.CalculateLoad(puzzleMatrix);
    }

    
}
