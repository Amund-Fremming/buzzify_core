using Domain.Example.Entities;

namespace Domain.Example.Contracts;

public interface IExampleRepository
{
    ExampleEntity Get();
}