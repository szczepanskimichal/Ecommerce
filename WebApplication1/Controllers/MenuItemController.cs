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
    [HttpGet("{id:int}", Name = "GetMenuItem")]
    public IActionResult GetMenuItem(int id)
    {
        if (id == 0)
        {
            _response.IsSuccess = false; // set success to false if not found
            _response.StatusCode = System.Net.HttpStatusCode.NotFound; // set status code to 404 Not Found
            return BadRequest(_response); // return 400 response with the API response object
        }
        MenuItem? menuItem  = _db.MenuItems.FirstOrDefault(m => m.Id == id);
        _response.Result = menuItem; // set the result to the found menu item
        _response.StatusCode = System.Net.HttpStatusCode.OK; // set status code to 200 OK
        return Ok(_response); // return 200 response with the API response object
    }
}