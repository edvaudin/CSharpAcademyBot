using CSharpAcademyBot.Contexts;
using CSharpAcademyBot.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharpAcademyBot.Repositories;

public class AcademyRepository : IAcademyRepository
{
    private readonly AcademyContext context;
    public AcademyRepository(AcademyContext context) 
    {
        this.context = context;
    }

    public User AddNewUser(User user)
    {
        context.Users.Add(user);
        context.SaveChanges();
        return user;
    }

    public User? GetUserByDiscordId(long discordId)
    {
        return context.Users.Include(u => u.Reputation).FirstOrDefault(x => x.DiscordId == discordId);
    }

    public void UpdateUserReputation(User user, int delta)
    {
        user.Reputation.Amount += delta;
        context.SaveChanges();
    }
}
