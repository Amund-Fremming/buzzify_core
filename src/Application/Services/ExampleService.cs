using Application.Contracts;
using Domain.Contracts;

namespace Application.Services;

public class ExampleService(IExampleRepository exampleRepository) : IExampleService
{
    public string GetProperty() => exampleRepository.Get().Property;
}