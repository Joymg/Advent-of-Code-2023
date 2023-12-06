
using static Joymg.AoC23.Utils;
using static Joymg.AoC23.Day5.Day5_1;

namespace Joymg.AoC23.Day5
{
    internal class Day5_2
    {
        static string[] inputs;
        const string inputFolderPath = "..\\..\\..\\Day5_IfYouGiveASeedAFertilizer_AoC_23\\Input\\";
        static long result;

        static void Main(string[] args)
        {
            inputs = ReadFile(inputFolderPath, InputType.First);

            long[] seeds = PreprocessSeedsRange(RemoveHeader(inputs[0]).Split(' ').Select(long.Parse).ToArray());

            result = CalculateMinimumLocation(seeds, inputs);

            Console.WriteLine(result);

        }

        private static long[] PreprocessSeedsRange(long[] seedsRange)
        {
            List<long> list = new List<long>();

            for (int i = 1; i < seedsRange.Length; i += 2)
            {
                for (int j = 0; j < seedsRange[i]; j++)
                {
                    list.Add(seedsRange[i-1] + j);
                }
            }
            return list.ToArray();
        }
    }
}
