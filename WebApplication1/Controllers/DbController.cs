using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DbController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet("ping")]
    public async Task<IActionResult> Ping()
    {
        var canConnect = await context.Database.CanConnectAsync();
        return Ok(new { canConnect });
    }
}

