using MediatR;
using Microsoft.AspNetCore.Mvc;
using yor_request_api.Features.DatingConcurrence.Commands;
using yor_request_api.Features.DatingConcurrence.Models;
using yor_request_api.Features.DatingConcurrence.Queries;

namespace yor_request_api.Features.DatingConcurrence
{
    [Route("api/concurrence")]
    [ApiController]
    public class ConcurrenceController : ControllerBase
    {
        private readonly ISender _mediator;

        public ConcurrenceController(ISender mediator)
        {
            _mediator = mediator
                ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get([FromQuery]Guid userId)
        {
            var query = new GetUserConcurrencesQuery
            {
                UserId = userId
            };

            IEnumerable<ConcurrenceResponse> concurrences;
            try
            {
                concurrences = await _mediator.Send(query);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return Ok(concurrences);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromQuery]ConcurrenceIdRequest request)
        {
            var command = new DeleteConcurrenceCommand
            {
                ConcurrenceId = request.ConcurrenceId
            };

            try
            {
                await _mediator.Send(command);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
