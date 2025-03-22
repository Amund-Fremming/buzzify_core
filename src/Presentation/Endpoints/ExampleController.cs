using Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ExampleController(IExampleService exampleService) : ControllerBase
{
    public IActionResult Example() => Ok(exampleService.GetProperty());
}