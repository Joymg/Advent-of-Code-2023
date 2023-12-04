using System.Numerics;
using System.Text;
using static Joymg.AoC23.Utils;

namespace Joymg.AoC23.Day3
{
    public class Day3_1
    {
        static string[] inputs;
        const string inputFolderPath = "C:\\OtherProjects\\Advent-of-Code-2023\\Day3_GearRatios_AoC_23\\Input\\";
        static int result;

        static void Main(string[] args)
        {
            inputs = ReadFile(inputFolderPath, InputType.First);

            CalculateGearRatio(inputs);

            Console.WriteLine(result);

        }

        public static void CalculateGearRatio(string[] inputs)
        {
            int inputWidth = inputs[0].Length;
            int inputHeight = inputs.Length;

            bool hasSymbolAdjacent = false;
            bool hasNumberEnded = false;
            StringBuilder foundNumber = new StringBuilder();

            //Iterate for each character
            for (int i = 0; i < inputWidth; i++)
            {
                for (int j = 0; j < inputHeight; j++)
                {
                    char currentChar = inputs[i][j];
                    if (char.IsDigit(currentChar))
                    {
                        //if a symbl was previously found near the number dont override the value
                        hasSymbolAdjacent = CheckNeighboursAreSymbol(inputWidth, inputHeight, i, j) ? true : hasSymbolAdjacent;
                        hasNumberEnded = false;
                        foundNumber.Append(currentChar);
                    }
                    else
                    {
                        //if the character is not a number
                        if (!hasNumberEnded)
                        {
                            //and has a symbol add it yo found number
                            if (hasSymbolAdjacent)
                            {
                                result += int.Parse(foundNumber.ToString());
                            }

                            //and reset values
                            foundNumber.Clear();
                            hasNumberEnded = true;
                            hasSymbolAdjacent = false;
                        }
                    }
                }
            }
        }

        private static bool CheckNeighboursAreSymbol(int width, int height, int x, int y)
        {
            Vector2 neighbourCoordinates;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    neighbourCoordinates = new Vector2(x + i, y + j);
                    if (!CoordinatesAreInBounds(width, height, neighbourCoordinates))
                    {
                        continue;
                    }

                    if (IsCharASymbol(inputs[x + i][y + j]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool CoordinatesAreInBounds(int width, int height, Vector2 neighbourCoordinates)
        {
            return neighbourCoordinates.X >= 0
                && neighbourCoordinates.Y >= 0
                && neighbourCoordinates.X < width
                && neighbourCoordinates.Y < height;
        }

        public static bool IsCharASymbol(char character)
        {
            //If the character is not a number and not a point is a valid symbol
            return !char.IsDigit(character) && character != '.';
        }
    }
}
