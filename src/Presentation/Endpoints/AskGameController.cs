using Domain.Entities.Ask;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Endpoints;

[Route("api/v1/[controller]")]
public class AskGameController : ReadControllerBase<AskGame>;