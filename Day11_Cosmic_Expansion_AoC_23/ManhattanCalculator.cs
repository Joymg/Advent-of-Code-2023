using System.Numerics;

namespace Joymg.AoC23.Day11;

public static class ManhattanCalculator
{
    public static long DistanceBetweenElements(List<Vector2> positions)
    {
        long totalDistance = 0;
        for (int i = 0; i < positions.Count - 1; i++)
        {
            for (int j = i + 1; j < positions.Count; j++)
            {
                totalDistance += Distance(positions[i], positions[j]);
            }
        }

        return totalDistance;
    }

    private static long Distance(Vector2 a, Vector2 b)
    {
        return Convert.ToInt64(MathF.Abs(a.X - b.X) + MathF.Abs(a.Y - b.Y));
    }
}