namespace ApiParams.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SumController : ControllerBase
{
    private readonly IMediator _mediator;

    public SumController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("sum")]
    public async Task<IActionResult> GetSum([FromQuery] int a, [FromQuery] int b)
    {
        var query = new SumQuery { A = a, B = b };
        var result = await _mediator.Send(query);
        return Ok(new { result });
    }
}
