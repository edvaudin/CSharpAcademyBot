using CSharpAcademyBot.Models;
using CSharpAcademyBot.Repositories;
using DSharpPlus.Entities;

namespace CSharpAcademyBot.Services;

public class AcademyService : IAcademyService
{
    private readonly IAcademyRepository repository;
    public AcademyService(IAcademyRepository repository) 
    { 
        this.repository = repository;
    }

    public void UpdateUserReputation(DiscordUser discordUser, int delta)
    {
        User? user = repository.GetUserByDiscordId((long)discordUser.Id);
        user ??= AddNewUser(discordUser);
        repository.UpdateUserReputation(user, delta);
    }

    public bool TryGetUserReputation(DiscordUser discordUser, out int rep)
    {
        User? user = repository.GetUserByDiscordId((long)discordUser.Id);
        if (user == null)
        {
            rep = 0;
            return false;
        }
        else
        {
            rep = user.Reputation.Amount;
            return true;
        }
    }

    private User AddNewUser(DiscordUser discordUser)
    {
        User user = new()
        {
            DiscordId = (long)discordUser.Id,
            Name = discordUser.Username,
            Reputation = new UserReputation() { Amount = 0 }
        };
        return repository.AddNewUser(user);
    }
}
