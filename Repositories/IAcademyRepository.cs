

using CSharpAcademyBot.Models;

namespace CSharpAcademyBot.Repositories;

public interface IAcademyRepository
{
    User AddNewUser(User user);
    User? GetUserByDiscordId(long discordId);
    void UpdateUserReputation(User user, int delta);
}
