using MediatR;

using Microsoft.AspNetCore.Mvc;

using yor_request_api.Features.DatingRequest.Commands;
using yor_request_api.Features.DatingRequest.Models;
using yor_request_api.Features.DatingRequest.Queries;

namespace yor_request_api.Features.DatingRequest
{
    [Route("api/request")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly ISender _mediator;

        public RequestController(ISender mediator)
        {
            _mediator = mediator
                ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [Route("new")]
        public async Task<IActionResult> SendRequest([FromBody]CreateRequestRequest body)
        {
            var command = new CreateRequestCommand
            {
                RecipientId = body.RecipientId,
                SenderId = body.SenderId,
            };

            try
            {
                await _mediator.Send(command);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("received")]
        public async Task<IActionResult> ReceivedRequests([FromQuery]Guid userId)
        {
            var query = new GetUserSentRequestsQuery
            {
                UserId = userId
            };

            try
            {
                var requests = await _mediator.Send(query);
                
                return Ok(requests);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("sent")]
        public async Task<IActionResult> SentRequests([FromQuery]Guid userId)
        {
            var query = new GetUserSentRequestsQuery
            {
                UserId =userId
            };

            try
            {
                var requests = await _mediator.Send(query);

                return Ok(requests);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            return Ok();
        }
    }
}
