
namespace Joymg.AoC23
{
    internal static class Utils
    {
        public enum InputType
        {
            Test,
            First,
            Second, 
            Queso,
            Lucia
        }


        private const string inputTest = "input_test";
        private const string input1 = "input_1";
        private const string input2 = "input_2";
        private const string inputQueso = "input_queso";
        private const string inputLucia = "input_lucia";


        public static string[] ReadFile(string folderPath, InputType type)
        {
            string inputPath = GetPath(type);


            return File.ReadAllLines(folderPath + inputPath);
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
                case InputType.Lucia:
                    selectedInput = inputLucia;
                    break;
            }
            return  selectedInput + ".txt";
        }

        public static string RemoveHeader(string line)
        {
            string[] splitLine = line.Split(':');
            while (splitLine[1].StartsWith(" "))
                splitLine[1]= splitLine[1].Remove(0, 1);
            return splitLine[1];
        }
    }
}
