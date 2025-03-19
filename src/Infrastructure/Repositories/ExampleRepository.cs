using Domain.Example.Contracts;
using Domain.Example.Entities;

namespace Infrastructure.Repositories;

public class ExampleRepository(IAppDbContext context) : IExampleRepository
{
    public ExampleEntity Get() => context.Get<ExampleEntity>();
}