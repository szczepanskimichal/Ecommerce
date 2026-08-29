using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.Dto;
using WebApplication1.Utility;
namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderDetailsController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly ApiResponse _response;

    public OrderDetailsController(ApplicationDbContext db)
    {
        _db = db;
        _response = new ApiResponse();
    }
   [HttpPut("{orderDetailId:int}")]
    public ActionResult<ApiResponse> UpdateOrder(int orderDetailId, [FromBody] OrderDetailsUpdateDTO orderDetailsUpdateDto)
    {
        try
        {
            if(ModelState.IsValid)
            {
                if(orderDetailId != orderDetailsUpdateDto.OrderDetailId)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("invalid Id");
                    return BadRequest(_response);
                }
                OrderDetailDTO? orderDetailsFromDb = _db.OrderDetails.FirstOrDefault(o => o.OrderDetailId == orderDetailId);
                if (orderDetailsFromDb == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("Order detail not found.");
                    return NotFound(_response);
                }

                orderDetailsFromDb.Rating = orderDetailsUpdateDto.Rating;
                _db.SaveChanges();

                _response.Result = orderDetailsFromDb;
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            else
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(_response);
            }
        }
        catch (Exception e)
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.ErrorMessages.Add(e.Message);
            return StatusCode((int)HttpStatusCode.InternalServerError, _response);
        }
        return Ok(_response);
    }
}