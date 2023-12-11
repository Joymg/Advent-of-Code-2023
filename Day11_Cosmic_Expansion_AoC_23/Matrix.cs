namespace Joymg.AoC23.Day11;

public class Matrix<T>
{
    public T[][] data;
    public int width;
    public int height;

    public Matrix()
    {
    }

    public Matrix(T[][] input)
    {
        width = input[0].Length;
        height = input.Length;
        data = new T[height][];
        for (int j = 0; j < height; j++)
        {
            data[j] = new T[width];
            for (int i = 0; i < width; i++)
            {
                data[j][i] = input[j][i];
            }
        }
    }
}

public static class MatrixExtensions
{
    public static int[][] ParseInputAsInt(string[] input)
    {
        int w = input[0].Length;
        int h = input.Length;
        int[][] data = new int[h][];
        for (int j = 0; j < input.Length; j++)
        {
            data[j] = new int[w];
            for (int i = 0; i < w; i++)
            {
                data[j][i] = int.Parse(input[j][i].ToString());
            }
        }

        return data;
    }

    public static long[][] ParseInputAsLong(string[] input)
    {
        int w = input[0].Length;
        int h = input.Length;
        long[][] data = new long[h][];
        for (int j = 0; j < input.Length; j++)
        {
            data[j] = new long[w];
            for (int i = 0; i < w; i++)
            {
                data[j][i] = long.Parse(input[j][i].ToString());
            }
        }

        return data;
    }

    public static char[][] ParseInputAsChar(string[] input)
    {
        int w = input[0].Length;
        int h = input.Length;
        char[][] data = new char[h][];
        for (int j = 0; j < h; j++)
        {
            data[j] = new char[w];
            for (int i = 0; i < w; i++)
            {
                data[j][i] = input[j][i];
            }
        }

        return data;
    }

    public static List<List<T>> ToList<T>(this Matrix<T> matrix)
    {
        List<List<T>> listMatrix = new List<List<T>>();
        
        for (int j = 0; j < matrix.height; j++)
        {
            listMatrix.Add(new List<T>(matrix.data[j]));
        }

        return listMatrix;
    }

    public static Matrix<T> ToArray<T>(List<List<T>> matrix)
    {
        T[][] arrayMatrix = new T[matrix.Count][];
        
        for (int j = 0; j < matrix.Count; j++)
        {
            arrayMatrix[j] =matrix[j].ToArray();
        }

        return new Matrix<T>(arrayMatrix);
    }

    public static Matrix<T> Transpose<T>(this Matrix<T> matrix)
    {
        int w = matrix.height;
        int h = matrix.width;
        T[][] data = new T[h][];
        for (int j = 0; j < h; j++)
        {
            data[j] = new T[w];
            for (int i = 0; i < w; i++)
            {
                data[j][i] = matrix.data[i][j];
            }
        }

        return new Matrix<T>(data);
    }
}