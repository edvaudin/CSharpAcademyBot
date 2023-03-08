using DSharpPlus.Entities;

namespace CSharpAcademyBot.Services;

public interface IAcademyService
{
    public void UpdateUserReputation(DiscordUser discordUser, int delta);
    public bool TryGetUserReputation(DiscordUser discordUser, out int rep);
}
