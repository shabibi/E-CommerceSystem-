using E_CommerceSystem.Models;
using E_CommerceSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace E_CommerceSystem.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class OrderController: ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("PlaceOrder")]
        public IActionResult PlaceOrder([FromBody] List<OrderItemDTO> items)
        {
            try
            {
                if (items == null || !items.Any())
                {
                    return BadRequest("Order items cannot be empty.");
                }

                // Retrieve the Authorization header from the request
                var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                // Decode the token to check user role
                var userRole = GetUserRoleFromToken(token);

                // Extract user ID 
                int uid = int.Parse(userRole.Sub);

                // Only allow user to place order
                if (userRole.UniqueName != "user")
                {
                    return BadRequest("You are not authorized to perform this action.");
                }
                _orderService.PlaceOrder(items, uid);

                return Ok("Order placed successfully.");
            }
            catch (Exception ex)
            {
                // Return a generic error response
                return StatusCode(500, $"An error occurred while placing order. {(ex.Message)}");

            }

        }

        private (string? Sub, string? UniqueName) GetUserRoleFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            if (handler.CanReadToken(token))
            {
                var jwtToken = handler.ReadJwtToken(token);

                // Extract the 'sub' claim
                var subClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub");

                // Extract the 'unique_name' claim
                var uniqueNameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "unique_name");

                return (subClaim?.Value, uniqueNameClaim?.Value); // Return both values as a tuple
            }

            throw new UnauthorizedAccessException("Invalid or unreadable token.");
        }
    }
}
