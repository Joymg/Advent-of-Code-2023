using static Joymg.AoC23.Utils;
using static Joymg.AoC23.Day3.Day3_1;
using System.Text;
using System.Numerics;

namespace Joymg.AoC23.Day3
{
    public class Day3_2
    {
        static string[] inputs;
        const string inputFolderPath = "C:\\OtherProjects\\Advent-of-Code-2023\\Day3_GearRatios_AoC_23\\Input\\";
        static int result;

        static void Main(string[] args)
        {
            inputs = ReadFile(inputFolderPath, InputType.First);

            FindValidGears(inputs);

            Console.WriteLine(result);

        }

        public static void FindValidGears(string[] inputs)
        {
            int inputWidth = inputs[0].Length;
            int inputHeight = inputs.Length;

            int gearRatio;

            //Iterate for each character
            for (int i = 0; i < inputWidth; i++)
            {
                for (int j = 0; j < inputHeight; j++)
                {
                    char currentChar = inputs[i][j];
                    if (IsCharAGear(currentChar))
                    {
                        gearRatio = CalculateGearRatio(inputWidth, inputHeight, i, j);
                        result += gearRatio;
                    }
                }
            }
        }

        private static bool IsCharAGear(char currentChar)
        {
            return currentChar.Equals('*');
        }

        private static int CalculateGearRatio(int width, int height, int x, int y)
        {
            Vector2 neighbourCoordinates;
            int firstGear = 0, secondGear = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    neighbourCoordinates = new Vector2(x + i, y + j);
                    if (!CoordinatesAreInBounds(width, height, neighbourCoordinates))
                    {
                        continue;
                    }

                    char neighbour = inputs[x + i][y + j];
                    if (char.IsDigit(neighbour))
                    {
                        //Maximum 2 numbers around a gear
                        int gear = GetWholeNumberWithCharacterOnIndex(inputs[x + i], (int)neighbourCoordinates.Y);
                        //if is the first number detected
                        if (firstGear == 0)
                        {
                            firstGear = gear;
                        }
                        //if 2 characters of the same number are detected
                        //if is the same as the first one, is part of the first one, do nothing
                        //if is different from the firstNumber, set it to second, and return value
                        else if (gear != firstGear)
                        {
                            secondGear = gear;
                            return firstGear * secondGear;
                        }
                    }
                }
            }
            return firstGear * secondGear;
        }

        private static int GetWholeNumberWithCharacterOnIndex(string line, int position)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(line[position]);

            //Get numbers in previous position of the detected number
            for (int i = position - 1; i >= 0; i--)
            {
                if (char.IsDigit(line[i]))
                {
                    //Append the number to the start of the text
                    stringBuilder.Insert(0, line[i]);
                }
                else
                {
                    break;
                }
            }
            //Get numbers in next positions of the detected number
            for (int i = position + 1; i < line.Length; i++)
            {
                if (char.IsDigit(line[i]))
                {
                    //Append the number to the end of the string
                    stringBuilder.Append(line[i]);
                }
                else
                {
                    break;
                }
            }

            return int.Parse(stringBuilder.ToString());
        }
    }
}
