using Discord;

public partial class DiscordEvents
{
    [DiscordEvents]
    public void schedule()
    {
        // TODO: rewrite
        TimeSpan sevenDays = new TimeSpan(24) * 7;
        DateTime nowTime = new DateTime();
        DateTime meetingTime = new DateTime(2025, 9, 9, 20, 0, 0, DateTimeKind.Utc);
        TimeSpan result = meetingTime - nowTime;

        _HLUGTimer = new Timer(SendMessages, null, (int)result.TotalMilliseconds, (int)sevenDays.TotalMilliseconds);
    }

    public async void SendMessages(object? state)
    {
        ITextChannel channel = (ITextChannel)await DiscordBot._Client.GetChannelAsync(UInt64.Parse(Environment.GetEnvironmentVariable("DISCORD_TEST_CHANNEL")!));
        await channel.SendMessageAsync($"<@{Environment.GetEnvironmentVariable("DISCORD_TEST_ROLE_ID")}> HLUG starts in 3 hours! See you there!");
    }
}