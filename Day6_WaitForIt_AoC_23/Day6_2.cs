
using static Joymg.AoC23.Utils;
using System.Linq;
using System.Diagnostics;

namespace Joymg.AoC23.Day6
{
    internal class Day6_2
    {
        static string[] inputs;
        const string inputFolderPath = "..\\..\\..\\Day6_WaitForIt_AoC_23\\Input\\";
        static int result = 1;

        static void Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();

            inputs = ReadFile(inputFolderPath, InputType.First);

            long[] times, records;
            ParseTimeAndRecords(inputs, out times, out records);
            //CalculateWinningCount(times, records);

            result = CalculateCalculateWinningCountWithMath(times[0], records[0]);

            Console.WriteLine(result);
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

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

        public static int CalculateCalculateWinningCountWithMath(long time, long record)
        {

            // Distance =  chargeTime * (totalTime  -  chargeTime)
            //         =   chargeTime *  totalTime   -  chargeTime  *  chargeTime
            //         = -(chargeTime *  chargeTime) +  chargeTime  *  totalTime
            // Record  = -(chargeTime *  chargeTime) +  chargeTime  *  totalTime
            // 0       = -(chargeTime *  chargeTime) +  chargeTime  *  totalTime - Record

            int a = 1;
            long b = -time;
            long c = record;
            long delta = b * b - 4 * a * c;
            float squareRoot = MathF.Sqrt(delta);
            float x1 = MathF.Floor((-b - squareRoot) / 2 * a);
            float x2 = MathF.Ceiling((-b + squareRoot) / 2 * a);

            return (int)(x2 - x1 - 1);

        }

    }
}
