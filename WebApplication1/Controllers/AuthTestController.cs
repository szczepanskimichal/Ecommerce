using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Utility;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthTestController : Controller
{
    // GET
    [HttpGet]
    [Authorize]
    public ActionResult<string> GetSomething()
    {
        return "You are authorized to see this message.";
    }
    
    [HttpGet("{someValue:int}")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public ActionResult<string> GetSomethingWithRole(int someValue)
    {
        return $"You are authorized to see this message with value: {someValue}.";
    }
    
}