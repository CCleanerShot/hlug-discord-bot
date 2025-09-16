using Discord;

public partial class DiscordEvents
{
    [DiscordEvents]
    public void schedule()
    {
        TimeSpan sevenDays = new TimeSpan(24) * 7;
        DayOfWeek desiredDay = DayOfWeek.Tuesday; // atm is tuesday
        double desiredHour = 23; // atm is 23, for 23 utc
        double day = 1000 * 60 * 60 * 24; // milliseconds * seconds * minutes * hours

        long nowSeconds = DateTime.UtcNow.Ticks / 10000;// https://learn.microsoft.com/en-us/dotnet/api/system.datetime.ticks?view=net-9.0#remarks
        DateTime meetingTime = new DateTime().AddDays(Math.Floor(nowSeconds / day));

        if (meetingTime.DayOfWeek != desiredDay)
            if (meetingTime.DayOfWeek > desiredDay)
                meetingTime = meetingTime.AddDays(7 - (meetingTime.DayOfWeek - desiredDay));
            else
                meetingTime = meetingTime.AddDays(desiredDay - meetingTime.DayOfWeek);

        meetingTime = meetingTime.AddHours(desiredHour);

        TimeSpan result = meetingTime - DateTime.UtcNow;
        _HLUGTimer = new Timer(SendMessages, null, (int)result.TotalMilliseconds, (int)sevenDays.TotalMilliseconds);
    }

    public async void SendMessages(object? state)
    {
        ITextChannel channel = (ITextChannel)await DiscordBot._Client.GetChannelAsync(UInt64.Parse(Environment.GetEnvironmentVariable("DISCORD_TEST_CHANNEL")!));
        await channel.SendMessageAsync($"<@&{Environment.GetEnvironmentVariable("DISCORD_TEST_ROLE_ID")}> HLUG starts in 3 hours! See you there!");
    }
}