using System.Diagnostics;
using static Joymg.AoC23.Day9.Day9_1;
using static Joymg.AoC23.Utils;

namespace Joymg.AoC23.Day9
{
    internal class Day9_2
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
                result += new ExtendedPrediction(ints).PredictBackwards();
            }
        }

        public class ExtendedPrediction : Prediction
        {
            public ExtendedPrediction(int[] readings) : base(readings)
            {
            }

            public int PredictBackwards()
            {
                List<int> firstValues = new List<int>();
                List<int> differences = new List<int>(readings);
                List<int> tmp = new List<int>();

                firstValues.Add(readings[0]);

                while (!AreAllZeros(differences))
                {
                    for (int i = 0; i < differences.Count - 1; i++)
                    {
                        tmp.Add(differences[i + 1] - differences[i]);
                    }
                    differences = tmp.ToList();
                    tmp.Clear();
                    firstValues.Add(differences[0]);
                }

                int prediction = 0;
                for (int i = firstValues.Count - 2; i >= 0; i--)
                {
                    prediction = firstValues[i] - prediction;
                }
                return prediction;
            }
        }
    }
}
