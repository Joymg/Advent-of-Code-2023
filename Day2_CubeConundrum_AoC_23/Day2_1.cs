using static Joymg.AoC23.Utils;

namespace Joymg.AoC23.Day2
{
    public class Day2_1
    {

        static string[] inputs;
        const string inputFolderPath = "C:\\OtherProjects\\Advent-of-Code-2023\\Day2_CubeConundrum_AoC_23\\Input\\";


        private static int red = 12;
        private static int green = 13;
        private static int blue = 14;

        private static string redString = "red";
        private static string greenString = "green";
        private static string blueString = "blue";
        private static int result = 0;

        static void Main(string[] args)
        {
            inputs = ReadFile(inputFolderPath, InputType.First);

            for (int i = 0; i < inputs.Length; i++)
            {
                AddValidGame(inputs[i]);
            }

            Console.WriteLine(result);

        }

        private static void AddValidGame(string gameData)
        {

            int gameID = GetGameID(gameData, out string gamePlays);

            string[] plays = gamePlays.Split(';');

            bool isGameValid = true;
            for (int i = 0; i < plays.Length; i++)
            {
                if (!IsValidPlay(plays[i]))
                {
                    isGameValid = false;
                    break;
                }
            }

            if (isGameValid)
            {
                result += gameID;
            }

        }


        private static bool IsValidPlay(string play)
        {
            bool isValid = true;
            string[] colors = play.Split(',');

            for (int i = 0; i < colors.Length; i++)
            {
                string[] value = colors[i].Split(' ');

                if (value[2] == redString)
                {
                    if (int.Parse(value[1])> red)
                    {
                        isValid = false;
                    }
                }
                if (value[2] == greenString)
                {
                    if (int.Parse(value[1]) > green)
                    {
                        isValid = false;
                    }
                }
                if (value[2] == blueString)
                {
                    if (int.Parse(value[1]) > blue)
                    {
                        isValid = false;
                    }
                }
            }
            return isValid;
        }

        private static int GetGameID(string gameData, out string gamePlays)
        {
            string[] data = gameData.Split(':');
            gamePlays = data[1];
            string gameHeader = data[0];
            return int.Parse(gameHeader.Split(' ')[1]);
        }
    }
}