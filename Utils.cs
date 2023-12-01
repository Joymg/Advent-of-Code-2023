
namespace Joymg.AoC23
{
    internal static class Utils
    {
        public enum InputType
        {
            Test,
            First,
            Second, 
            Queso
        }

        private const string folderPath = "D:\\OtherProjects\\Advent-of-Code-2023\\AdventOfCode2023\\Day 1_Trebuchet_AoC_23\\Inputs\\";

        private const string inputTest = "input_test";
        private const string input1 = "input_1";
        private const string input2 = "input_2";
        private const string inputQueso = "input_queso";


        public static string[] ReadFile(InputType type)
        {
            string fullPath = GetPath(type);

            return File.ReadAllLines(fullPath);
        }

        private static string GetPath(InputType type)
        {
            string selectedInput = "";
            switch (type)
            {
                case InputType.Test:
                    selectedInput = inputTest;
                    break;
                case InputType.First:
                    selectedInput = input1;
                    break;
                case InputType.Second:
                    selectedInput = input2;
                    break;
                case InputType.Queso:
                    selectedInput = inputQueso;
                    break;
            }
            return folderPath + selectedInput + ".txt";
        }
    }
}
