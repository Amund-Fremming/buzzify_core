using Domain.Contracts;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class ExampleRepository : IExampleRepository
{
    public ExampleEntity Get() => new();
}