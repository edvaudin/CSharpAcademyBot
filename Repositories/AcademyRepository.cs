using CSharpAcademyBot.Contexts;

namespace CSharpAcademyBot.Repositories;

public class AcademyRepository : IAcademyRepository
{
    private readonly AcademyContext context;
    public AcademyRepository(AcademyContext context) 
    {
        this.context = context;
    }
}
