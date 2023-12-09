using System.Diagnostics;
using static Joymg.AoC23.Utils;

namespace Joymg.AoC23.Day9
{
    internal class Day9_1
    {
        static string[] inputs;
        const string inputFolderPath = "..\\..\\..\\Day9_MirageMaintenance_AoC_23\\Input\\";
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
            int[] ints;
            for (int i = 0; i < inputs.Length; i++)
            {
                ints = inputs[i].Split(" ").Where(x => !string.IsNullOrEmpty(x)).ToArray()
                .Select(int.Parse).ToArray();
                result += new Prediction(ints).PredictForward();
            }
        }

        public class Prediction
        {
            public int[] readings;

            public Prediction(int[] readings)
            {
                this.readings = readings;
            }

            public int PredictForward()
            {
                List<int> lastvalues = new List<int>();
                List<int> differences = new List<int>(readings);
                List<int> tmp = new List<int>();

                lastvalues.Add(readings[^1]);

                while (!AreAllZeros(differences))
                {
                    for (int i = 0; i < differences.Count - 1; i++)
                    {
                        tmp.Add(differences[i + 1] - differences[i]);
                    }
                    differences = tmp.ToList();
                    tmp.Clear();
                    lastvalues.Add(differences[^1]);
                }

                int prediction = 0;
                for (int i = lastvalues.Count - 2; i >= 0; i--)
                {
                    prediction += lastvalues[i];
                }
                return prediction;
            }

            public bool AreAllZeros(List<int> differences)
            {
                foreach (int difference in differences)
                {
                    if (difference != 0)
                        return false;
                }
                return true;
            }
        }
    }
}
