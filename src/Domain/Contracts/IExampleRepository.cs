using Domain.Entities;

namespace Domain.Contracts;

public interface IExampleRepository
{
    ExampleEntity Get();
}