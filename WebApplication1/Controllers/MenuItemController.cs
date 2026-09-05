using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.Dto;

namespace WebApplication1.Controllers;

[Route("api/MenuItem")]
[ApiController]
public class MenuItemController : Controller
{
    //dependency injection !!!
    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment _env;
    private readonly ApiResponse _response; // for API responses

    public MenuItemController(ApplicationDbContext db, IWebHostEnvironment env) //constructor injection
    {
        _db = db;
        _response = new ApiResponse(); // initialize the response object, injected in the constructor!!!
        _env = env;
    }

    // GET
    [HttpGet]
    public IActionResult GetMenuItems()
    {
        List<MenuItem> menuItems = _db.MenuItems.ToList(); // fetch all menu items from the database
        List<OrderDetailDTO> orderDetailsWithRatings = _db.OrderDetails.Where(od => od.Rating.HasValue).ToList(); // fetch order details with ratings
        foreach (var menuItem in menuItems)
        {
            var ratings = orderDetailsWithRatings.Where(od => od.MenuItemId == menuItem.Id)
                .Select(od => od.Rating.Value).ToList(); // get ratings for the current menu item
            double avgRating =
                ratings.Any() ? ratings.Average() : 0; // calculate average rating or set to 0 if no ratings
            menuItem.Rating = avgRating; // set the average rating for the menu item
        }

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
        if (menuItem == null)
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.NotFound;
            return NotFound(_response);
        }
        List<OrderDetailDTO> orderDetailsWithRatings = _db.OrderDetails.Where(od => od.Rating != null && od.MenuItemId==menuItem.Id).ToList(); // fetch order details with ratings

        var ratings = orderDetailsWithRatings.Select(od => od.Rating.Value); // get ratings for the current menu item
        double avgRating = ratings.Any() ? ratings.Average() : 0; // calculate average rating or set to 0 if no ratings
        menuItem.Rating = avgRating; // set the average rating for the menu item
        
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

                var webRootPath = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                if (_env.WebRootPath != null)
                {
                    var imagesPath = Path.Combine(_env.WebRootPath, "images");
                    if (!Directory.Exists(imagesPath)) Directory.CreateDirectory(imagesPath);
                    var filePath = Path.Combine(imagesPath, menuItemCreateDto.File.FileName);
                    if (System.IO.File.Exists(filePath)) System.IO.File.Delete(filePath);
                    // uploading the img !!!!
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await menuItemCreateDto.File.CopyToAsync(stream);
                    }
                }
                // if created, we need to save the menu item in the database !!!
                MenuItem menuItem = new()
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

                _response.Result = menuItemCreateDto;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetMenuItem", new { id = menuItem.Id }, _response);
            }

            _response.IsSuccess = false;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string> { e.ToString() };
            throw;
        }

        return BadRequest(_response);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse>> UpdateMenuItem(int id, [FromForm] MenuItemUpdateDto menuItemUpdateDto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (menuItemUpdateDto.Id != id)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var menuItemFromDb = await _db.MenuItems.FirstOrDefaultAsync(m => m.Id == id);
                if (menuItemFromDb == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                menuItemFromDb.Name = menuItemUpdateDto.Name;
                menuItemFromDb.Description = menuItemUpdateDto.Description;
                menuItemFromDb.Price = menuItemUpdateDto.Price;
                menuItemFromDb.Category = menuItemUpdateDto.Category;
                menuItemFromDb.SpecialTag = menuItemUpdateDto.SpecialTag;
                if (menuItemUpdateDto.File != null && menuItemUpdateDto.File.Length > 0)
                {
                    var webRoot = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    var imagesPath = Path.Combine(webRoot, "images");
                    if (!Directory.Exists(imagesPath)) Directory.CreateDirectory(imagesPath);

                    var filePath = Path.Combine(imagesPath, menuItemUpdateDto.File.FileName);
                    if (System.IO.File.Exists(filePath)) System.IO.File.Delete(filePath);

                    var filePath_OldFile =
                        Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"),
                            menuItemFromDb.Image);
                    if (System.IO.File.Exists(filePath_OldFile)) System.IO.File.Delete(filePath_OldFile);
                    //Upload Image
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await menuItemUpdateDto.File.CopyToAsync(stream);
                    }

                    menuItemFromDb.Image = "images/" + menuItemUpdateDto.File.FileName;
                }


                _db.MenuItems.Update(menuItemFromDb);
                await _db.SaveChangesAsync();

                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }

            _response.IsSuccess = false;
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string> { e.ToString() };
            throw;
        }

        return BadRequest(_response);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse>> DeleteMenuItem(int id)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var menuItemFromDb = await _db.MenuItems.FirstOrDefaultAsync(m => m.Id == id);
                if (menuItemFromDb == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }


                var webRoot = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");


                var filePath_OldFile =
                    Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"),
                        menuItemFromDb.Image);
                if (System.IO.File.Exists(filePath_OldFile)) System.IO.File.Delete(filePath_OldFile);
                _db.MenuItems.Remove(menuItemFromDb);
                await _db.SaveChangesAsync();
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string> { ex.ToString() };
        }

        return BadRequest(_response);
    }
}