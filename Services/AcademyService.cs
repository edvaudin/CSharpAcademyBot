using CSharpAcademyBot.Repositories;

namespace CSharpAcademyBot.Services;

public class AcademyService : IAcademyService
{
    private readonly IAcademyRepository repository;
    public AcademyService(IAcademyRepository repository) 
    { 
        this.repository = repository;
    }
}
