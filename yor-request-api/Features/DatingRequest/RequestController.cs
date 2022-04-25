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
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        [Route("received")]
        public async Task<IActionResult> ReceivedRequests([FromQuery]Guid userId)
        {
            var query = new GetUserReceivedRequestsQuery
            {
                UserId = userId
            };

            IEnumerable<RequestResponse> requests;
            try
            {
                requests = await _mediator.Send(query);                
            }
            catch
            {
                return BadRequest();
            }

            return Ok(requests);
        }

        [HttpGet]
        [Route("sent")]
        public async Task<IActionResult> SentRequests([FromQuery]Guid userId)
        {
            var query = new GetUserSentRequestsQuery
            {
                UserId =userId
            };

            IEnumerable<RequestResponse> requests;
            try
            {
                requests = await _mediator.Send(query);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(requests);
        }

        [HttpPost]
        [Route("accept")]
        public async Task<IActionResult> AcceptRequest([FromBody]RequestIdRequest request)
        {
            var command = new AcceptRequestCommand
            {
                RequestId = request.RequestId
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

        [HttpPost]
        [Route("reject")]
        public async Task<IActionResult> RejectRequest([FromBody]RequestIdRequest request)
        {
            var command = new RejectRequestCommand
            {
                RequestId = request.RequestId
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

        [HttpPost]
        [Route("cancel")]
        public async Task<IActionResult> CancelRequest([FromBody]RequestIdRequest request)
        {
            var command = new CancelRequestCommand
            {
                RequestId = request.RequestId
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
