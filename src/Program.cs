// looks weird, but there are currently 2 ways I have setup to catch program exits
// 1. unhandled exceptions
// 2. abrupt program exit
// the purpose of this is to log the session into /logs

public class Program
{
    public static Utility Utility = default!;

    static async Task Main()
    {
        Console.CancelKeyPress += (sender, e) => SaveSession();
        await Run();
    }

    static async Task Run()
    {
        try
        {
            Utility = new Utility();
            Utility.Log(Enums.LogLevel.NONE, "Initializing...");
            DotNetEnv.Env.Load();
            await DiscordBot.Initialize();

            // // keeps program running
            await Task.Delay(-1);
        }

        catch (Exception e)
        {
            Utility.Log(Enums.LogLevel.ERROR, e.ToString());
            SaveSession();
        }
    }

    static void SaveSession()
    {
        Utility.Log(Enums.LogLevel.NONE, "Saving log to file...");

        if (!Directory.Exists("logs"))
            Directory.CreateDirectory("logs");

        using StreamWriter writer = new StreamWriter("./logs/" + DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm") + ".txt", true);
        writer.Write(Utility.LogLine);
    }
}

