
using static Joymg.AoC23.Utils;

namespace Joymg.AoC23.Day1
{
    public class Day1_1
    {
        const string inputFolderPath = "C:\\OtherProjects\\Advent-of-Code-2023\\Day1_Trebuchet_AoC_23\\Input\\";
        static string[] inputs;
        static List<int> calibrationValues = new List<int>();

        static void Main(string[] args)
        {
            // Display the number of command line arguments.

            inputs = ReadFile(inputFolderPath,InputType.First);

            for (int i = 0; i < inputs.Length; i++)
            {
                GetFirstAndLastNumber(inputs[i]);
            }

            int result = SumCalibrationValues();

            Console.WriteLine(result);
        }

        private static int SumCalibrationValues()
        {
            int sum = 0;
            calibrationValues.ForEach(value => sum += value);
            return sum;
        }

        private static void GetFirstAndLastNumber(string line)
        {
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
            calibrationValues.Add(int.Parse(result));
        }

    }
}

