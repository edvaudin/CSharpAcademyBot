using CSharpAcademyBot.Services;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace CSharpAcademyBot;

public class ReputationManager
{
	private readonly IAcademyService service;
	public ReputationManager(IAcademyService service)
	{
		this.service = service;
	}

	public async Task UpdateUserReputation(DiscordChannel channel, DiscordUser discordUser, int delta)
	{
		// This is where we will be checking if the user has now met any role requirements.
		service.UpdateUserReputation(discordUser, delta);
		if (service.TryGetUserReputation(discordUser, out int rep))
		{
			await channel.SendMessageAsync($"{discordUser.Username}'s reputation is now {rep}.");
		}
		else
		{
            await channel.SendMessageAsync($"Something went wrong trying to fetch {discordUser.Username}'s reputation.");
        }
	}
}
