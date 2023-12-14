namespace Joymg.AoC23.Day12;

public class SpringRecord
{
    public int[] damagedSpringSizes;
    public char[] springs;

    public SpringRecord(int[] damagedSpringSizes, char[] springs)
    {
        this.damagedSpringSizes = damagedSpringSizes;
        this.springs = springs;
        
    }
}

public static class SpringRecordExtensions
{
    public static bool IsValid(this SpringRecord springRecord, string springSet)
    {
        int consecutiveFaultySprings = 0;
        bool faultyFound = false, previousFaulty = false;
        List<int> recordSizeDistribution = new List<int>();

        for (int i = 0; i < springSet.Length; i++)
        {
            faultyFound = springSet[i] == '#';
            if (faultyFound)
            {
                consecutiveFaultySprings++;
            }

            if (!faultyFound && previousFaulty)
            {
                recordSizeDistribution.Add(consecutiveFaultySprings);
                consecutiveFaultySprings = 0;
            }

            previousFaulty = faultyFound;
        }

        if (faultyFound)
        {
            recordSizeDistribution.Add(consecutiveFaultySprings);
        }

        if (recordSizeDistribution.Count != springRecord.damagedSpringSizes.Length)
        {
            return false;
        }

        for (int i = 0; i < springRecord.damagedSpringSizes.Length; i++)
        {
            if (springRecord.damagedSpringSizes[i] != recordSizeDistribution[i])
            {
                return false;
            }
        }

        return true;
    }

    public static SpringRecord Expand(this SpringRecord springRecord)
    {
        List<char> stringExpansion = new List<char>();
        List<int> intExpansion = new List<int>();

        for (int i = 0; i < 5; i++)
        {
            
            stringExpansion.AddRange(springRecord.springs);
            if (i != 4)
            {
                stringExpansion.Add('?');
            }

            intExpansion.AddRange(springRecord.damagedSpringSizes);
        }
        return new SpringRecord(intExpansion.ToArray(),stringExpansion.ToArray());
    }
}