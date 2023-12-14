using System.Text;

namespace Joymg.AoC23.Day11;

public class Matrix<T> : IEquatable<T>
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
    
    public string CalculateLoad(Matrix<char> matrix)
    {
        matrix = matrix.Transpose();
        int load = 0;
        for (int i = 0; i < matrix.height; i++)
        {
            for (int j = 0; j < matrix.width; j++)
            {
                load += matrix.data[i][j] == 'O' ? matrix.width - j : 0;
            }
        }

        return load.ToString();
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                sb.Append(data[i][j]);
            }

            sb.Append("\n");
        }

        return sb.ToString();
    }

    public bool Equals(T? other)
    {
        T val1 = data[0][0];
        return EqualityComparer<T>.Default.Equals(val1, other);
        throw new NotImplementedException();
    }

    public override bool Equals(object? obj)
    {
        Matrix<T> other = obj as Matrix<T>;
        if (other == null)
        {
            return false;
        }

        if (width != other.width || height != other.height)
        {
            return false;
        }

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (!data[i][j]!.Equals(other.data[i][j]))
                {
                    return false;
                }
            }
        }

        return true;
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
            arrayMatrix[j] = matrix[j].ToArray();
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

    public static Matrix<char> Cycle(this Matrix<char> matrix, long cycles)
    {
        List<Matrix<char>> cycledMatrices = new List<Matrix<char>>();

        Matrix<char> newMatrix = ToArray(matrix.ToList());
        int repeatsAt = -1;
        int firstRepatIndex = -1;
        int patternLength = 1;
        //cycledMatrices.Add(matrix);
        for (int currentCycle = 0; currentCycle < cycles; currentCycle++)
        {
            newMatrix = Cycle(newMatrix);

            int cycleIndex = cycledMatrices.IndexOf(newMatrix);
            if (cycleIndex != -1)
            {
                if (repeatsAt == -1)
                {
                    repeatsAt = currentCycle;
                    firstRepatIndex = cycleIndex;
                }
                else if (cycleIndex == firstRepatIndex)
                {
                    patternLength = currentCycle - repeatsAt;
                    break;
                }
            }
            else
            {
                cycledMatrices.Add(newMatrix);
            }
        }

        int targetIteration = firstRepatIndex + ((int)(cycles - repeatsAt) % patternLength);
        return cycledMatrices[targetIteration-1];
    }

    private static Matrix<char> Cycle(this Matrix<char> matrix)
    {
        return matrix.Transpose().Tilt(true).Transpose().Tilt(true).Transpose().Tilt(false).Transpose().Tilt(false);
    }

    public static Matrix<char> Tilt(this Matrix<char> matrix, bool Upwards = true)
    {
        var listMatrix = matrix.ToList();
        var auxiliarMatrix = matrix.ToList();
        List<char> charList = new List<char>();
        for (int i = 0; i < matrix.height; i++)
        {
            for (int j = 0; j < matrix.width; j++)
            {
                if (auxiliarMatrix[i][0] == '#')
                {
                    charList.Add('#');
                    auxiliarMatrix[i].RemoveAt(0);
                }
                else
                {
                    int fixPosition = auxiliarMatrix[i].IndexOf('#');
                    if (fixPosition == -1)
                    {
                        fixPosition = auxiliarMatrix[i].Count;
                    }

                    if (Upwards)
                    {
                        charList.AddRange(auxiliarMatrix[i].GetRange(0, fixPosition).OrderByDescending(value => value));
                    }
                    else
                    {
                        charList.AddRange(auxiliarMatrix[i].GetRange(0, fixPosition).OrderBy(value => value));
                    }

                    auxiliarMatrix[i].RemoveRange(0, fixPosition);
                    j += fixPosition - 1;
                }
            }

            listMatrix[i] = charList.ToList();
            charList.Clear();
        }

        return ToArray(listMatrix);
    }

    public class MatrixEqualityComparer : IEqualityComparer<Matrix<char>>
    {
        public bool Equals(Matrix<char> matrix, Matrix<char> other)
        {
            if (matrix.width != other.width || matrix.height != other.height)
            {
                return false;
            }

            for (int i = 0; i < matrix.height; i++)
            {
                for (int j = 0; j < matrix.width; j++)
                {
                    if (matrix.data[i][j] != other.data[i][j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public int GetHashCode(Matrix<char> obj)
        {
            return obj.width * obj.height;
        }
    }
}