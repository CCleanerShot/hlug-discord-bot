using System.Reflection;
using System.Timers;
using Discord.WebSocket;

/// <summary>
/// For usage inside DiscordEvents methods. NOTE: The method must be public!
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public partial class DiscordEventsAttribute : Attribute { }

/// <summary>
/// Create events in the /events folder.
/// </summary>
public partial class DiscordEvents
{
    /// <summary>
    /// The timer for the next time HLUG starts.
    /// </summary>
    public System.Threading.Timer _HLUGTimer = new System.Threading.Timer((object? state) => { }, null, 100, Timeout.Infinite);
    /// <summary>
    /// Client for the discord bot.
    /// </summary>
    DiscordSocketClient _Client;

    public Task Load()
    {
        IEnumerable<MethodInfo> methods = GetType().GetMethods()
            .Where(s => s.GetCustomAttribute<DiscordEventsAttribute>() != null);

        foreach (MethodInfo method in methods)
            method.Invoke(this, null);

        return Task.CompletedTask;
    }

    public DiscordEvents(DiscordSocketClient _Client)
    {
        this._Client = _Client;
    }
}

