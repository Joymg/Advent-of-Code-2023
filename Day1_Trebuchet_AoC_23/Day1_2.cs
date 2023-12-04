
using System.Text;
using static Joymg.AoC23.Utils;

namespace Joymg.AoC23.Day1
{
    internal class Day1_2
    {
        const string inputFolderPath = "C:\\OtherProjects\\Advent-of-Code-2023\\Day1_Trebuchet_AoC_23\\Input\\";
        static string[] inputs;
        static int calibrationValuesSum = 0;

        static string[] spelledNumbers = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        static void Main(string[] args)
        {
            inputs = ReadFile(inputFolderPath, InputType.Second);

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
            int addedCharacters = 0;

            for (int i = 0; i < line.Length; i++)
            {
                for (int j = 0; j < spelledNumbers.Length; j++)
                {
                    substring = line.Substring(i);
                    if (substring.StartsWith(spelledNumbers[j]))
                    {
                        sb.Insert(i + addedCharacters + 1, j + 1);

                        addedCharacters++;
                        break;
                    }
                }
            }
            return sb.ToString();
        }
    }
}
