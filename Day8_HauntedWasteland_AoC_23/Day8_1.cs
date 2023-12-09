
using static Joymg.AoC23.Utils;
using System.Diagnostics;

namespace Joymg.AoC23.Day8
{
    internal class Day8_1
    {
        static string[] inputs;
        const string inputFolderPath = "..\\..\\..\\Day8_HauntedWasteland_AoC_23\\Input\\";
        static int result;


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

            Dictionary<string, CrossRoad> map = new Dictionary<string, CrossRoad>();

            for (int i = 2; i < inputs.Length; i++)
            {
                (string id, CrossRoad roads) = ParseCrossroadEntry(inputs[i]);
                map.Add(id, roads);
            }

            int numSteps = 0;
            string currentId = "AAA";

            while (currentId != "ZZZ")
            {
                int direction = directions[numSteps % directions.Length];
                currentId = map[currentId].roads[direction];
                numSteps++;
            }

            result = numSteps;
        }

        public static (string, CrossRoad) ParseCrossroadEntry(string input)
        {
            string id;
            CrossRoad roads;
            string[] entry = input.Split('=');

            id = entry[0].Trim();
            string[] roadsString = entry[1].Split('(', ',', ')');
            roadsString = roadsString.Where(road => !string.IsNullOrWhiteSpace(road) && !string.IsNullOrEmpty(road)).Select(road => road = road.Trim()).ToArray();
            roads = new CrossRoad(roadsString);

            return (id, roads);
        }

        public static int[] ParseDirections(string directionString)
        {
            directionString = directionString.Replace('L', '0').Replace('R', '1');
            List<char> characters = directionString.ToCharArray().ToList();
            int[] result = new int[characters.Count];
            for (int i = 0; i < characters.Count; i++)
            {
                result[i] = int.Parse(characters[i].ToString());
            }
            return result;
        }

        public class CrossRoad
        {
            public string[] roads;

            public CrossRoad(string[] roads)
            {
                this.roads = roads;
            }
        }
    }
}
