using System.Diagnostics;
using static Joymg.AoC23.Utils;
using static Joymg.AoC23.Day8.Day8_1;

namespace Joymg.AoC23.Day8
{
    internal class Day8_2
    {
        static string[] inputs;
        const string inputFolderPath = "..\\..\\..\\Day8_HauntedWasteland_AoC_23\\Input\\";
        static long result;


        static void Main(string[] args)
        {

            Stopwatch sw = Stopwatch.StartNew();
            inputs = ReadFile(inputFolderPath, InputType.First);

            Solve(inputs);

            Console.WriteLine(result);

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

        }

        private static void Solve(string[] inputs)
        {
            int[] directions = ParseDirections(inputs[0]);
            List<string> startingNodes = new List<string>();
            

            Dictionary<string, CrossRoad> map = new Dictionary<string, CrossRoad>();


            for (int i = 2; i < inputs.Length; i++)
            {
                (string id, CrossRoad roads) = ParseCrossroadEntry(inputs[i]);
                map.Add(id, roads);
                if (id[2] == 'A')
                {
                    startingNodes.Add(id);
                }
            }
            long[] numStepsPerPath = new long[startingNodes.Count];
            long numSteps = 0;


            for (int i = 0; i < startingNodes.Count; i++)
            {
                string currentId = startingNodes[i];
                while (!currentId.EndsWith('Z'))
                {
                    int direction = directions[numSteps % directions.Length];
                    currentId = map[currentId].roads[direction];
                    numSteps++;

                }

                numStepsPerPath[i] = numSteps;
                numSteps = 0;

            }

            long mcm = lcm(numStepsPerPath[0], numStepsPerPath[1]);

            for (int i = 2; i < numStepsPerPath.Length ; i++)
            {
                long newValue = numStepsPerPath[i];
                mcm = lcm(mcm, newValue);
            }

            result = mcm;
        }

        private static bool AllEndInZ(string[] endingNodes)
        {
            foreach (var node in endingNodes)
            {
                if (node[2] != 'Z')
                {
                    return false;
                }
            }
            return true;
        }

        static long gcf(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        static long lcm(long a, long b)
        {
            return (a / gcf(a, b)) * b;
        }
    }
}
