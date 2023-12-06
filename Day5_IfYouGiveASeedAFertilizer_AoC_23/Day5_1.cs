using static Joymg.AoC23.Utils;

namespace Joymg.AoC23.Day5
{
    internal class Day5_1
    {
        static string[] inputs;
        const string inputFolderPath = "..\\..\\..\\Day5_IfYouGiveASeedAFertilizer_AoC_23\\Input\\";
        static long result;

        static void Main(string[] args)
        {
            inputs = ReadFile(inputFolderPath, InputType.First);

            long[] seeds = RemoveHeader(inputs[0]).Split(' ').Select(long.Parse).ToArray();
            result = CalculateMinimumLocation(seeds,inputs);

            Console.WriteLine(result);

        }

        public static long CalculateMinimumLocation(long[] seeds, string[] inputs)
        {
            bool[] hasChangedInThisCategory = new bool[seeds.Length];
            bool sameCategory = false;

            for (int i = 3; i < inputs.Length; i++)
            {
                sameCategory = IsValidInputLine(inputs[i]);

                if (sameCategory)
                {
                    long[] mapData = inputs[i].Split(" ").Select(long.Parse).ToArray();
                    for (int j = 0; j < seeds.Length; j++)
                    {
                        if (hasChangedInThisCategory[j])
                        {
                            continue;
                        }

                        long previousValue = seeds[j];
                        seeds[j] = MapValue(seeds[j], mapData[0], mapData[1], mapData[2]);

                        if (previousValue != seeds[j])
                            hasChangedInThisCategory[j] = true;
                    }
                }
                else
                {
                    ResetBoolArray(hasChangedInThisCategory);
                }
            }

            return seeds.Min();

        }

        private static void ResetBoolArray(bool[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = false;
            }
        }

        private static bool IsValidInputLine(string line)
        {
            return !(line.Equals("") || line.Equals(" ") || line.Contains(':'));
        }

        private static long MapValue(long valueToProcess, long destinationRangeStart, long sourceRangeStart, long rangeLength)
        {
            if (valueToProcess < sourceRangeStart)
            {
                return valueToProcess;
            }
            long currentRange = valueToProcess - sourceRangeStart;
            return currentRange <= rangeLength ? destinationRangeStart + currentRange : valueToProcess;
        }
    }
}
