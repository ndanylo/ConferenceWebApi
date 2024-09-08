using Microsoft.AspNetCore.Mvc;
using MediatR;
using Bookings.Application.Commands;
using Bookings.Application.Queries;
using Bookings.WebApi.DI;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Bookings.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateBooking(CreateBookingRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = Guid.Parse(userIdClaim.Value);

            var command = new CreateBookingCommand
            {
                UserId = userId,
                ConferenceHallId = request.ConferenceHallId,
                Date = request.Date,
                StartTime = request.StartTime,
                Duration = request.Duration,
                SelectedServices = request.SelectedServices
            };
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok();
            }

            return BadRequest(result.Error);
        }

        [Authorize]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteBooking(Guid id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = Guid.Parse(userIdClaim.Value);

            var result = await _mediator.Send(new DeleteBookingCommand 
            { 
                BookingId = id,
                UserId = userId
            });
            
            if (result.IsSuccess)
            {
                return Ok();
            }

            return NotFound(result.Error);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("get-by-user/{userId}")]
        public async Task<IActionResult> GetBookingsByUserId(Guid userId)
        {
            var result = await _mediator.Send(new GetBookingsByUserIdQuery { UserId = userId });
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return NotFound(result.Error);
        }

        [Authorize]
        [HttpGet("get-my-bookings")]
        public async Task<IActionResult> GetMyBookings()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = Guid.Parse(userIdClaim.Value);

            var result = await _mediator.Send(new GetBookingsByUserIdQuery 
            { 
                UserId = userId
            });
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return NotFound(result.Error);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet("get-by-date/{conferenceHallId}/{date}")]
        public async Task<IActionResult> GetBookingsByDate(DateTime date)
        {
            var result = await _mediator.Send(new GetBookingsByDateQuery
            {
                Date = date
            });

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return NotFound(result.Error);
        }

        [HttpGet("check-availability")]
        public async Task<IActionResult> CheckAvailability(
            [FromQuery] Guid conferenceHallId, 
            [FromQuery] DateTime date, 
            [FromQuery] TimeSpan startTime, 
            [FromQuery] TimeSpan duration
        )
        {
            var result = await _mediator.Send(new CheckAvailabilityQuery
            {
                ConferenceHallId = conferenceHallId,
                Date = date,
                StartTime = startTime,
                Duration = duration
            });

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }

        [HttpGet("search-available-halls")]
        public async Task<IActionResult> SearchAvailableHalls(
            [FromQuery] DateTime date,
            [FromQuery] TimeSpan startTime,
            [FromQuery] TimeSpan duration,
            [FromQuery] int capacity)
        {
            var query = new SearchAvailableHallsQuery
            {
                Date = date,
                StartTime = startTime,
                Duration = duration,
                Capacity = capacity
            };

            var result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }
    }
}
