
using static Joymg.AoC23.Utils;

namespace Joymg.AoC23.Day6
{
    internal class Day6_1
    {
        static string[] inputs;
        const string inputFolderPath = "..\\..\\..\\Day6_WaitForIt_AoC_23\\Input\\";
        static int result = 1;

        static void Main(string[] args)
        {
            inputs = ReadFile(inputFolderPath, InputType.First);

            long[] times, records;
            ParseTimeAndRecords(inputs, out times, out records);
            CalculateWinningCount(times, records);

            Console.WriteLine(result);

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
            for (int pressedTime = 0; pressedTime <= time; pressedTime++)
            {
                long timeTravelling = time - pressedTime;
                long distanceTravelled = timeTravelling * pressedTime;

                if (distanceTravelled > record)
                {
                    sum++;
                }
            }
            return sum;
        }

        private static void ParseTimeAndRecords(string[] inputs, out long[] times, out long[] records)
        {
            times = RemoveHeader(inputs[0]).Split(" ").Where(x => !string.IsNullOrEmpty(x)).ToArray()
                .Select(long.Parse).ToArray();
            records = RemoveHeader(inputs[1]).Split(" ").Where(x => !string.IsNullOrEmpty(x)).ToArray()
                .Select(long.Parse).ToArray();
        }
    }
}
