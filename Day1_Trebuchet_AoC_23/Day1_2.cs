
using System.Text;
using static Joymg.AoC23.Utils;

namespace Joymg.AoC23
{
    internal class Day1_2
    {
        static string[] inputs;
        static int calibrationValuesSum = 0;

        static string[] spelledNumbers = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        static void Main(string[] args)
        {
            inputs = ReadFile(InputType.Second);

            for (int i = 0; i < inputs.Length; i++)
            {
                GetFirstAndLastNumber(inputs[i]);
            }


            Console.WriteLine(calibrationValuesSum);
        }


        private static void GetFirstAndLastNumber(string line)
        {

            line = SwapSpelledNumbers(line);

            char firstNumber = ' ', secondNumber = ' ';

            for (int i = 0; i < line.Length; i++)
            {
                if (firstNumber == ' ' && char.IsNumber(line[i]))
                {
                    firstNumber = line[i];
                }

                if (secondNumber == ' ' && char.IsDigit(line[^(i + 1)]))
                {
                    secondNumber = line[^(i + 1)];
                }

                if (firstNumber != ' ' && secondNumber != ' ')
                {
                    break;
                }
            }
            string result = string.Join("", firstNumber, secondNumber);
            int configurationValue = int.Parse(result);
            calibrationValuesSum += configurationValue;
        }

        private static string SwapSpelledNumbers(string line)
        {
            string substring;
            StringBuilder sb = new StringBuilder();
            sb.Append(line);
            int removedCharacters = 0;

            for (int i = 0; i < line.Length; i++)
            {
                for (int j = 0; j < spelledNumbers.Length; j++)
                {
                    substring = line.Substring(i);
                    if (substring.StartsWith(spelledNumbers[j]))
                    {
                        sb.Insert(i + removedCharacters+1, j + 1);

                        removedCharacters ++;
                        break;
                    }
                }
            }
            return sb.ToString();
        }
    }
}
