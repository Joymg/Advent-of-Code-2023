using System.Linq;

namespace Joymg.AoC23.Day12;

public class ArrangementCounter
{
    public static long GetPossibleArrangements(SpringRecord springRecord)
    {
        List<char> subset = new List<char>();
        List<List<char>> result = new List<List<char>>();
        List<string> stringResult = new List<string>();

        long index = 0;
        subset = new List<char>(springRecord.springs);
        //CalculateSubset(springRecord, result, subset, index);
        return CalculateArrangementsDP(springRecord);

    }

    private static void CalculateSubset(SpringRecord springRecord, List<List<char>> result, List<char> subset,
        long index)
    {
        if (!subset.Contains('?'))
        {
            if (springRecord.IsValid(new string(subset.ToArray())))
            {
                result.Add(subset.ToList());
            }

            return;
        }


        for (int i = (int)index; i < springRecord.springs.Length; i++)
        {
            if (subset[i] == '?')
            {
                subset[i] = '.';

                CalculateSubset(springRecord, result, subset, i + 1);

                subset[i] = '#';

                CalculateSubset(springRecord, result, subset, i + 1);

                subset[i] = '?';
            }
        }
    }

    private static int CalculateArrangementsDP(SpringRecord springRecord)
    {
        int maxRun = springRecord.damagedSpringSizes.Max();
        List<char> s = new List<char>(springRecord.springs);
        s.Add('.');
        List<int> targetRuns = new List<int>(springRecord.damagedSpringSizes);
        targetRuns.Add(0);


        int n = s.Count;
        int m = targetRuns.Count;
        int[,,] dp = new int[n, m, maxRun+1];

        for (int i = 0; i < n; i++)
        {
            char x = s[i];
            for (int j = 0; j < m; j++)
            {
                for (int k = 0; k <= maxRun; k++)
                {
                    if (i == 0)
                    {
                        if (j != 0)
                        {
                            dp[i, j, k] = 0;
                            continue;
                        }


                        if (x == '#')
                        {
                            if (k != 1)
                            {
                                dp[i, j, k] = 0;
                                continue;
                            }

                            dp[i, j, k] = 1;
                            continue;
                        }

                        if (x == '.')
                        {
                            if (k != 0)
                            {
                                dp[i, j, k] = 0;
                                continue;
                            }

                            dp[i, j, k] = 1;
                            continue;
                        }

                        if (x == '?')
                        {
                            if (k is not 0 and not 1)
                            {
                                dp[i, j, k] = 0;
                                continue;
                            }

                            dp[i, j, k] = 1;
                            continue;
                        }
                    }

                    //Process if currentCharacter is a dot
                    int dotCase = 0;
                    if (k != 0)
                    {
                        dotCase = 0;
                    }
                    else if (j >0)
                    {
                        if (k==0)
                        {
                            dotCase = dp[i - 1, j - 1, targetRuns[j - 1]];
                            dotCase += dp[i - 1, j, 0];
                        }
                    }
                    else
                    {
                        //i > 0, j > 0, k > 0
                        dotCase = s.GetRange(0, i).Contains('#') ? 1 : 0;
                    }
                    
                    //process if currentCharacter is #
                    int hashtagCase = 0;
                    if (k==0)
                    {
                        hashtagCase = 0;
                    }
                    else
                    {
                        hashtagCase = dp[i - 1, j, k - 1];
                    }

                    if (x == '.')
                    {
                        dp[i, j, k] = dotCase;
                    }

                    if (x== '#')
                    {
                        dp[i, j, k] = hashtagCase;
                    }
                    else
                    {
                        dp[i, j, k] = dotCase + hashtagCase;
                    }
                }
            }
        }

        return dp[n-1, m-1, 0];
    }
}