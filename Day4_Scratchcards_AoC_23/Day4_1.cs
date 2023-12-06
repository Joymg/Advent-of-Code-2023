using System.Diagnostics;
using static Joymg.AoC23.Utils;

namespace Joymg.AoC23.Day4
{
    internal class Day4_1
    {
        static string[] inputs;
        const string inputFolderPath = "..\\..\\..\\Day4_Scratchcards_AoC_23\\Input\\";
        static int result;

        static void Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();
            inputs = ReadFile(inputFolderPath, InputType.First);

            for (int i = 0; i < inputs.Length; i++)
            {
                CheckWinningCard(inputs[i]);
            }

            Console.WriteLine(result);


            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

        }

        private static void CheckWinningCard(string play)
        {
            play = RemoveHeader(play);

            string[] scratchcard = play.Split('|');
            string[] winningNumbers = scratchcard[0].Split(' ');
            string[] numbers = scratchcard[1].Split(' ');

            float points = 0.5f;

            for (int i = 0;i < winningNumbers.Length;i++)
            {
                if (IsEmptyString(winningNumbers[i]))
                {
                    continue;
                }
                if (numbers.Contains(winningNumbers[i]))
                {
                    points *= 2;
                }
            }
            result += (int)points;
        }

        public static bool IsEmptyString(string number)
        {
            return number == "" || number == " ";
        }

        public static string RemoveHeader(string play)
        {
            return play.Split(':')[1].Remove(0,1);
        }
    }
}
