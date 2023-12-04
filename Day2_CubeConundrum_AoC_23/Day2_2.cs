using static Joymg.AoC23.Utils;

namespace Joymg.AoC23.Day2
{
    public class Day2_2
    {

        static string[] inputs;
        const string inputFolderPath = "..\\..\\..\\Day2_CubeConundrum_AoC_23\\Input\\";


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
            Play minimumPlay = new Play();
            for (int i = 0; i < plays.Length; i++)
            {
                if (!IsValidPlay(plays[i], ref minimumPlay))
                {
                    isGameValid = false;
                }
            }


            if (isGameValid)
            {
                minimumPlay.power = minimumPlay.red * minimumPlay.green * minimumPlay.blue;
                result += minimumPlay.power;
            }

        }


        private static bool IsValidPlay(string play, ref Play minimumPlay)
        {
            bool isValid = true;
            string[] colors = play.Split(',');

            for (int i = 0; i < colors.Length; i++)
            {
                string[] value = colors[i].Split(' ');

                int number = int.Parse(value[1]);
                if (value[2] == redString)
                {
                    minimumPlay.red = number > minimumPlay.red ? number : minimumPlay.red;
                    
                }
                if (value[2] == greenString)
                {
                    minimumPlay.green = number > minimumPlay.green ? number : minimumPlay.green;
                    
                }
                if (value[2] == blueString)
                {
                    minimumPlay.blue = number > minimumPlay.blue ? number : minimumPlay.blue;
                    
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

    public struct Play
    {
        public int red;
        public int green;
        public int blue;
        public int power;
        

        public Play() 
        {
            this.red = 0;
            this.green = 0;
            this.blue = 0;
            this.power = 0;
        }

    }
}