namespace Joymg.AoC23.Day11;

public static class CosmicSnapshot
{
    public const char galaxy = '#';

    public static Matrix<char> ExpandUniverse(Matrix<char> snapshot)
    {
        Matrix<char> expandedUniverse = ExpandVertically(snapshot);
        expandedUniverse = expandedUniverse.Transpose();
        expandedUniverse = ExpandVertically(expandedUniverse);
        expandedUniverse = expandedUniverse.Transpose();
        return expandedUniverse;
    }

    private static Matrix<char> ExpandVertically(Matrix<char> snapshot)
    {
        List<List<char>> tmp = snapshot.ToList();
        for (int j = 0; j < tmp.Count; j++)
        {
            if (!tmp[j].Contains(galaxy))
            {
                tmp[j] = tmp[j].Select(value => {value = '?'; return value;}).ToList();
                j++;
            }
        }

        return MatrixExtensions.ToArray(tmp);
    }
}