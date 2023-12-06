
using static Joymg.AoC23.Utils;
using System.Linq;
using System.Diagnostics;

namespace Joymg.AoC23.Day6
{
    internal class Day6_2
    {
        static string[] inputs;
        const string inputFolderPath = "..\\..\\..\\Day6_WaitForIt_AoC_23\\Input\\";
        static int result = 1 ;

        static void Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();

            inputs = ReadFile(inputFolderPath, InputType.First);

            long[] times, records;
            ParseTimeAndRecords(inputs, out times, out records);
            CalculateWinningCount(times, records);


            Console.WriteLine(result);
            sw.Stop();
            Console.WriteLine(sw.Elapsed);

        }

        public static void CalculateWinningCount(long[] times, long[] records)
        {

            for (int i = 0; i < times.Length; i++)
            {
                result *= CalculatePosibleWinningStrats(times[i], records[i]);
            }
        }


        private static int CalculatePosibleWinningStrats(long time, long record)
        {
            int sum = 0;
            for (int pressedTime = (int)time / 2; pressedTime > 0; pressedTime--)
            {
                long timeTravelling = time - pressedTime;
                long distanceTravelled = timeTravelling * pressedTime;

                if (distanceTravelled > record)
                {
                    sum++;
                }
                else
                {
                    break;
                }
            }

            for (int pressedTime = (int)(time / 2) + 1; pressedTime <= time; pressedTime++)
            {
                long timeTravelling = time - pressedTime;
                long distanceTravelled = timeTravelling * pressedTime;

                if (distanceTravelled > record)
                {
                    sum++;
                }
                else
                {
                    break;
                }
            }
            return sum;
        }


        private static void ParseTimeAndRecords(string[] inputs, out long[] times, out long[] records)
        {
            times = new long[] { long.Parse(String.Join("", RemoveHeader(inputs[0]).Split(" ").Where(x => !string.IsNullOrEmpty(x)))) };
            records = new long[] { long.Parse(String.Join("", RemoveHeader(inputs[1]).Split(" ").Where(x => !string.IsNullOrEmpty(x)))) };
        }

    }
}
