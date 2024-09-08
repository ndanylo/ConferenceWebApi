using ConferenceHalls.Application.Commands;
using ConferenceHalls.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceHalls.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConferenceServiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConferenceServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllServicesQuery();
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
            var query = new GetServicesByIdsQuery
            {
                ServiceIds = new [] { id }
            };
            var result = await _mediator.Send(query);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("get-by-hall/{hallId}")]
        public async Task<IActionResult> GetByHallId(Guid hallId)
        {
            var query = new GetServicesByConferenceHallIdQuery(hallId);
            var result = await _mediator.Send(query);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin")] 
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateServiceCommand command)
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
            var command = new DeleteServiceCommand
            {
                ServiceId = id
            };
            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
