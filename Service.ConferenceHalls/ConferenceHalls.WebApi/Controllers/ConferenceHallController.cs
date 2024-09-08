using MediatR;
using Microsoft.AspNetCore.Mvc;
using ConferenceHalls.Application.Queries;
using ConferenceHalls.Application.ConferenceHalls.Commands;
using Microsoft.AspNetCore.Authorization;

namespace ConferenceHalls.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConferenceHallController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConferenceHallController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllConferenceHallsQuery();
            var result = await _mediator.Send(query);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetConferenceHallsByIdsQuery
            {
                Ids = new[] { id }
            };
            
            var result = await _mediator.Send(query);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin")] 
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateConferenceHallCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
        
        [Authorize(Roles = "Admin")] 
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteConferenceHallCommand(id);
            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("ChangeRentPrice")]
        public async Task<IActionResult> UpdateRentPrice([FromBody] UpdateRentPriceCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddServices")]
        public async Task<IActionResult> AddServices([FromBody] AddServicesToConferenceHallCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
