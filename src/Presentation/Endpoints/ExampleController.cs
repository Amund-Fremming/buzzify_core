using Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Endpoints;

[ApiController]
[Route("api/v1/[controller]")]
public class ExampleController(IExampleService exampleService) : ControllerBase
{
    public IActionResult Example() => Ok(exampleService.GetProperty());
}