using System.Net; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore; 
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.Dto;
using WebApplication1.Utility;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderHeaderController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly ApiResponse _response;

    public OrderHeaderController(ApplicationDbContext db)
    {
        _db = db;
        _response = new ApiResponse();
    }

    [HttpGet]
    public ActionResult<ApiResponse> GetOrders(string userId = "")
    {
        IEnumerable<OrderHeader> orderHeadersList = _db.OrderHeaders.Include(u => u.OrderDetails)
            .ThenInclude(u => u.MenuItem).OrderByDescending(u => u.OrderHeaderId);
        if (!string.IsNullOrEmpty(userId))
            orderHeadersList = orderHeadersList.Where(u => u.ApplicationUserId == userId);
        _response.Result = orderHeadersList.ToList();
        _response.StatusCode = HttpStatusCode.OK;
        return Ok(_response);
    }

    [HttpGet("{orderId:int}")]
    public ActionResult<ApiResponse> GetOrder(int orderId)
    {
        if (orderId == 0)
        {
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.ErrorMessages.Add("Invalid order ID.");
            return BadRequest(_response);
        }

        OrderHeader? orderHeader = _db.OrderHeaders.Include(u => u.OrderDetails)
            .ThenInclude(u => u.MenuItem).FirstOrDefault(u => u.OrderHeaderId == orderId);
        if (orderHeader == null)
        {
            _response.StatusCode = HttpStatusCode.NotFound;
            _response.IsSuccess = false;
            _response.ErrorMessages.Add("Order not found.");
            return NotFound(_response);
        }

        _response.Result = orderHeader;
        _response.StatusCode = HttpStatusCode.OK;
        return Ok(_response);
    }

    [HttpPost]
    public ActionResult<ApiResponse> CreateOrder([FromBody] OrderHeaderCreateDTO orderHeaderDTO)
    {
        try
        {
            if(ModelState.IsValid)
            {
                OrderHeader ordrHeader = new()
                {
                    PickUpName = orderHeaderDTO.PickUpName,
                    PickUpPhoneNumber = orderHeaderDTO.PickUpPhoneNumber,
                    PickUpEmail = orderHeaderDTO.PickUpEmail,
                    OrderDate = DateTime.Now,
                    OrderTotal = orderHeaderDTO.OrderTotal,
                    Status = StaticDetails.StatusConfirmed,
                    TotalItems = orderHeaderDTO.TotalItems,
                    ApplicationUserId = orderHeaderDTO.ApplicationUserId
                };

                _db.OrderHeaders.Add(ordrHeader);
                _db.SaveChanges();

                foreach (var orderDetailDTO in orderHeaderDTO.OrderDetailsDTO)
                {
                    OrderDetailDTO orderDetail = new()
                    {
                        OrderHeaderId = ordrHeader.OrderHeaderId,
                        MenuItemId = orderDetailDTO.MenuItemId,
                        Quantity = orderDetailDTO.Quantity,
                        ItemName = orderDetailDTO.ItemName,
                        Price = orderDetailDTO.Price
                    };

                    _db.OrderDetails.Add(orderDetail);
                }

                _db.SaveChanges();
                _response.Result = ordrHeader;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtAction(nameof(GetOrder), new { orderId = ordrHeader.OrderHeaderId }, _response);
            }

            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.ErrorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return BadRequest(_response);
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
    
    [HttpPut("{orderId:int}")]
    public ActionResult<ApiResponse> UpdateOrder(int orderId, [FromBody] OrderHeaderUpdateDTO orderHeaderDTO)
    {
        try
        {
            if(ModelState.IsValid)
            {
                if(orderId != orderHeaderDTO.OrderHeaderId)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Order ID mismatch.");
                    return BadRequest(_response);
                }

                OrderHeader? orderHeaderFromDb = _db.OrderHeaders.FirstOrDefault(o => o.OrderHeaderId == orderId);
                if (orderHeaderFromDb == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("Order not found.");
                    return NotFound(_response);
                }

                if(!string.IsNullOrEmpty(orderHeaderDTO.PickUpName))
                    orderHeaderFromDb.PickUpName = orderHeaderDTO.PickUpName;
                if(!string.IsNullOrEmpty(orderHeaderDTO.PickUpPhoneNumber))
                    orderHeaderFromDb.PickUpPhoneNumber = orderHeaderDTO.PickUpPhoneNumber;
                if(!string.IsNullOrEmpty(orderHeaderDTO.PickUpEmail))
                    orderHeaderFromDb.PickUpEmail = orderHeaderDTO.PickUpEmail;

                if (!string.IsNullOrEmpty(orderHeaderDTO.Status))
                {
                    if(orderHeaderFromDb.Status.Equals(StaticDetails.StatusConfirmed, StringComparison.InvariantCultureIgnoreCase) &&
                       orderHeaderDTO.Status.Equals(StaticDetails.StatusReadyForPickup, StringComparison.InvariantCultureIgnoreCase))
                    {
                        orderHeaderFromDb.Status = StaticDetails.StatusReadyForPickup;
                    }

                    if(orderHeaderFromDb.Status.Equals(StaticDetails.StatusReadyForPickup, StringComparison.InvariantCultureIgnoreCase) &&
                       orderHeaderDTO.Status.Equals(StaticDetails.StatusCompleted, StringComparison.InvariantCultureIgnoreCase))
                    {
                        orderHeaderFromDb.Status = StaticDetails.StatusCompleted;
                    }

                    if (orderHeaderDTO.Status.Equals(StaticDetails.StatusCancelled, StringComparison.InvariantCultureIgnoreCase))
                    {
                        orderHeaderFromDb.Status = StaticDetails.StatusCancelled;
                    }
                }

                _db.SaveChanges();

                _response.Result = orderHeaderFromDb;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }

            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.ErrorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return BadRequest(_response);
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