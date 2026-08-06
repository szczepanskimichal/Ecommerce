using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.Dto;

namespace WebApplication1.Controllers;

[Route("api/MenuItem")]
[ApiController]
public class MenuItemController : Controller
{
    //dependency injection
    private readonly ApplicationDbContext _db;
    private readonly ApiResponse _response; // for API responses
    private readonly IWebHostEnvironment _env;

    public MenuItemController(ApplicationDbContext db, IWebHostEnvironment env) //constructor injection
    {
        _db = db;
        _response = new ApiResponse(); // initialize the response object
        _env = env;
    }

    // GET
    [HttpGet]
    public IActionResult GetMenuItems()
    {
        _response.Result = _db.MenuItems.ToList(); // fetch all menu items from the database
        _response.StatusCode = HttpStatusCode.OK; // set the status code to 200 OK
        return Ok(_response);
    }

    [HttpGet("{id:int}", Name = "GetMenuItem")] // ID!!!!!!
    public IActionResult GetMenuItem(int id)
    {
        if (id == 0)
        {
            _response.IsSuccess = false; // set success to false if not found
            _response.StatusCode = HttpStatusCode.NotFound; // set status code to 404 Not Found
            return BadRequest(_response); // return 400 response with the API response object
        }

        MenuItem? menuItem = _db.MenuItems.FirstOrDefault(m => m.Id == id);
        _response.Result = menuItem; // set the result to the found menu item
        _response.StatusCode = HttpStatusCode.OK; // set status code to 200 OK
        return Ok(_response); // return 200 response with the API response object
    }

[HttpPost]
    public async Task<ActionResult<ApiResponse>> CreateMenuItem([FromForm] MenuItemCreateDto menuItemCreateDto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (menuItemCreateDto.File == null || menuItemCreateDto.File.Length == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string> { "File is required." };
                    return BadRequest(_response);
                }
                var webRoot = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                var imagesPath = Path.Combine(webRoot, "images");
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }
                var filePath = Path.Combine(imagesPath, menuItemCreateDto.File.FileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                // uploading the img to root folder!!!!
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await menuItemCreateDto.File.CopyToAsync(stream);
                }
                
                MenuItem menuItem = new ()
                {
                    Name = menuItemCreateDto.Name,
                    Description = menuItemCreateDto.Description,
                    Price = menuItemCreateDto.Price,
                    Category = menuItemCreateDto.Category,
                    SpecialTag = menuItemCreateDto.SpecialTag,
                    Image = "images/" + menuItemCreateDto.File.FileName
                };
                _db.MenuItems.Add(menuItem);
                await _db.SaveChangesAsync();
                
                _response.Result = menuItem;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetMenuItem", new { id = menuItem.Id }, _response);
            }
            else
            {
                _response.IsSuccess = false;
            }
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string> { e.ToString() };
            throw;
        }
        return BadRequest(_response);
    }
}