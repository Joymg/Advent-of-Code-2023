using Joymg.AoC23.Day11;

namespace Joymg.AoC23.Puzzles;

public class Day13Part2 : LinesPuzzle
{
    protected override string Solve(string[] puzzleInput)
    {
        int result = 0;
        foreach (Matrix<char> matrix in CreateMatrices(puzzleInput))
        {
            result += CheckReflection(matrix);
        }

        return result.ToString();
    }

    private int CheckReflection(Matrix<char> matrix)
    {
        matrix = matrix.Transpose();
        List<List<char>> listMatrix = matrix.ToList();
        for (int i = 0; i < matrix.height - 1; i++)
        {
            if (GetSmudges(listMatrix, i))
            {
                return i + 1;
            }
        }

        matrix = matrix.Transpose();
        listMatrix = matrix.ToList();
        for (int i = 0; i < matrix.height - 1; i++)
        {
            if (GetSmudges(listMatrix, i))
            {
                return (i + 1) * 100;
            }
        }

        return -1;
    }


    private bool GetSmudges(List<List<char>> listMatrix, int i)
    {
        int totalSmudges = 0;

        int aux = i + 1;
        while (i >= 0 && aux < listMatrix.Count)
        {
            for (int j = 0; j < listMatrix[i].Count; j++)
            {
                if (listMatrix[i][j] != listMatrix[aux][j])
                {
                    totalSmudges++;
                }

                if (totalSmudges > 1)
                {
                    return false;
                }
            }

            i--;
            aux++;
        }

        return totalSmudges == 1;
    }

    private bool IsReflecting(List<List<char>> listMatrix, int i)
    {
        int aux = i + 1;
        while (i >= 0 && aux < listMatrix.Count)
        {
            if (!listMatrix[i].SequenceEqual(listMatrix[aux]))
            {
                return false;
            }

            i--;
            aux++;
        }

        return true;
    }

    private List<Matrix<char>> CreateMatrices(string[] puzzleInput)
    {
        List<Matrix<char>> matrices = new List<Matrix<char>>();
        List<string> matrixContent = new List<string>();
        for (var index = 0; index < puzzleInput.Length; index++)
        {
            var input = puzzleInput[index];
            if (!string.IsNullOrEmpty(input) || !string.IsNullOrWhiteSpace(input))
            {
                matrixContent.Add(input);
            }
            else
            {
                CreateMatrix(matrices, matrixContent);
            }
        }

        if (matrixContent.Count > 0)
        {
            CreateMatrix(matrices, matrixContent);
        }

        return matrices;
    }

    private static void CreateMatrix(List<Matrix<char>> matrices, List<string> matrixContent)
    {
        matrices.Add(new Matrix<char>(MatrixExtensions.ParseInputAsChar(matrixContent.ToArray())));
        matrixContent.Clear();
    }
}