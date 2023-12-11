using System.Numerics;

namespace Joymg.AoC23.Day11;

public static class GalaxyLocator
{
    private const char galaxy = '#';

    public static List<Vector2> Find(Matrix<char> snapshot, int expansionValue = 2)
    {
        int expandedSpacesHorizontal = 0;
        int expandedSpacesVertical = 0;

        List<Vector2> galaxyLocations = new List<Vector2>();
        for (int j = 0; j < snapshot.height; j++)
        {
            expandedSpacesHorizontal = 0;
            if (snapshot.data[j][0] == '?')
            {
                expandedSpacesVertical++;
                continue;
            }
            
            for (int i = 0; i < snapshot.width; i++)
            {
                if (snapshot.data[j][i] == '?')
                {
                    expandedSpacesHorizontal++;
                }
                else if (snapshot.data[j][i] == galaxy)
                {
                    galaxyLocations.Add(new Vector2(i + (expandedSpacesHorizontal * (expansionValue-1)),
                        j + (expandedSpacesVertical * (expansionValue-1))));
                }
            }
        }

        return galaxyLocations;
    }
}