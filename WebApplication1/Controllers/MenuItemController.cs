using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[Route("api/MenuItem")]
[ApiController]
public class MenuItemController : Controller
{
    //dependency injection
    private readonly ApplicationDbContext _db; 
    private readonly ApiResponse _response; // for API responses
    public MenuItemController(ApplicationDbContext db) //constructor injection
    {
        _db = db;
        _response = new ApiResponse(); // initialize the response object
    }
    // GET
    [HttpGet]
    public IActionResult GetMenuItems()
    {
        _response.Result = _db.MenuItems.ToList(); // fetch all menu items from the database
        _response.StatusCode = System.Net.HttpStatusCode.OK; // set the status code to 200 OK
        return Ok(_response); 
    }
}