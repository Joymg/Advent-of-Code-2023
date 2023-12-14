namespace Joymg.AoC23;

public static class Startup
{
    private const int DayToSetup = 13;

    public static void RunDay1()
    {
        StartDayPart1(DayToSetup);
    }


    public static void RunDay2()
    {
        StartDayPart2(DayToSetup);
    }

    private static void StartDayPart1(int dayToSetup)
    {
        string stringDayNumber = $"{dayToSetup:00}";

        string path = $"../../../Puzzles";
        bool dir = Directory.Exists($"{path}/{stringDayNumber}");
        if (!dir)
        {
            Directory.CreateDirectory($"{path}/{stringDayNumber}");
        }

        CreateIfDontExist($"{path}/{stringDayNumber}/example.in1.txt");
        CreateIfDontExist($"{path}/{stringDayNumber}/example.check1.txt");

        CopyTemplateToPart1(stringDayNumber, path);

        DownloadInput(stringDayNumber, App.AOC_YEAR);
    }

    private static void StartDayPart2(int dayToSetup)
    {
        string stringDayNumber = $"{dayToSetup:00}";

        string path = $"../../../Puzzles";
        CreateIfDontExist($"{path}/{stringDayNumber}/example.in2.txt");
        CreateIfDontExist($"{path}/{stringDayNumber}/example.check2.txt");
        CopyCodeFromPart1ToPart2(stringDayNumber, path);
    }

    private static void CopyTemplateToPart1(string stringDayNumber, string puzzlesFolderPath)
    {
        string sourceClass = $"Template";
        string targetClass = $"Day{stringDayNumber}Part1";
        string sourceClassFilePath = $"{puzzlesFolderPath}/{sourceClass}.cs";
        string targetClassFilePath = $"{puzzlesFolderPath}/{stringDayNumber}/{targetClass}.cs";
        CopyClassSource(sourceClass, targetClass, sourceClassFilePath, targetClassFilePath);
    }

    private static void CopyCodeFromPart1ToPart2(string stringDayNumber, string puzzlesFolderPath)
    {
        string sourceClass = $"Day{stringDayNumber}Part1";
        string targetClass = $"Day{stringDayNumber}Part2";
        string sourceClassFilePath = $"{puzzlesFolderPath}/{stringDayNumber}/{sourceClass}.cs";
        string targetClassFilePath = $"{puzzlesFolderPath}/{stringDayNumber}/{targetClass}.cs";
        CopyClassSource(sourceClass, targetClass, sourceClassFilePath, targetClassFilePath);
    }

    private static void CopyClassSource(string sourceClass, string targetClass, string sourceClassFilePath, string targetClassFilePath)
    {
        var lines = File.ReadAllLines(sourceClassFilePath).ToList();
        lines = lines.Select(x =>
        {
            var replace = x.Replace(sourceClass, targetClass);
            return replace;
        }).ToList();
        File.WriteAllLines(targetClassFilePath, lines.ToArray());
    }

    private static void DownloadInput(string day, string aocYear)
    {
        DownloadInput(day, aocYear, $"../../../Puzzles/{day}/input.txt", "input");
    }

    private static void DownloadInput(string day, string aocYear, string filePath, string inputSubPath)
    {
        bool exist = File.Exists(filePath);
        string sessionIdPath = $"../../../sessionid";
        if (!exist)
        {
            if (File.Exists(sessionIdPath))
            {
                string url = $"https://adventofcode.com/{aocYear}/day/{day}/{inputSubPath}";

                try
                {
                    var sessionId = File.ReadAllLines(sessionIdPath);

                    using var handler = new HttpClientHandler();
                    handler.UseCookies = false;

                    using HttpClient client = new HttpClient(handler);
                    Uri uri = new Uri(url);
                    client.BaseAddress = uri;

                    var message = new HttpRequestMessage(HttpMethod.Get, "/test");
                    client.DefaultRequestHeaders.Add("Cookie", sessionId[0]);
                    var result = client.GetAsync(uri).Result;
                    string responseBody = result.Content.ReadAsStringAsync().Result;
                    File.WriteAllLines(filePath, responseBody.Split("\n"));
                }
                catch (Exception e)
                {
                    throw new Exception($"Could not download {url} file for {day} of $aocYear", e);
                }
            }
        }
    }

    private static void CreateIfDontExist(string filepath)
    {
        bool file = File.Exists(filepath);
        if (!file)
        {
            File.Create(filepath);
        }
    }
}