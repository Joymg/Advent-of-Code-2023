
using static Joymg.AoC23.Utils;
using static Joymg.AoC23.Day4.Day4_1;

namespace Joymg.AoC23.Day4
{
    internal class Day5_2
    {
        static string[] inputs;
        const string inputFolderPath = "..\\..\\..\\Day4_Scratchcards_AoC_23\\Input\\";
        static int result;
        static int[] CountOfEachCard;

        static void Main(string[] args)
        {
            inputs = ReadFile(inputFolderPath, InputType.First);

            CountOfEachCard = new int[inputs.Length];

            for (int i = 0; i < CountOfEachCard.Length; i++)
            {
                CountOfEachCard[i] = 1;
            }

            for (int i = 0; i < inputs.Length; i++)
            {
                CountTotalCards(inputs[i]);
            }

            for(int i = 0;i < CountOfEachCard.Length; i++)
            {
                result += CountOfEachCard[i];
            }

            Console.WriteLine(result);

        }

        private static void CountTotalCards(string cardData)
        {
            int matchCount = 0;
            
            string[] headerAndPlay = cardData.Split(':');

            int card =int.Parse(headerAndPlay[0].Remove(0, 5));


            string[] scratchcard = cardData.Split('|');
            string[] winningNumbers = scratchcard[0].Split(' ');
            string[] numbers = scratchcard[1].Split(' ');

            for (int i = 0;i < winningNumbers.Length;i++) 
            {
                if (IsEmptyString(winningNumbers[i]))
                {
                    continue;
                }

                if (numbers.Contains(winningNumbers[i]))
                {
                    matchCount++;
                }
            }

            for (int i = card; i< matchCount+card; i++)
            {
                CountOfEachCard[i] += 1 * CountOfEachCard[card-1];
            }
        }
    }
}
