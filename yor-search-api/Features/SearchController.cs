using MediatR;

using Microsoft.AspNetCore.Mvc;
using yor_search_api.Features.Params.Queries;
using yor_search_api.Features.Search.Queries;

namespace yor_search_api.Features
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISender _mediator;

        public SearchController(ISender mediator)
        {
            _mediator = mediator
                ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Search(
            [FromQuery]string[] tags, 
            string gender, 
            string country, 
            string city, 
            int minAge, 
            int maxAge)
        {
            var users = await _mediator.Send(new SearchQuery
            {
                Tags = tags,
                Gender = gender,
                Country = country,
                City = city,
                MinAge = minAge,
                MaxAge = maxAge,
                Claims = User
            });

            return Ok(users);
        }

        [HttpGet]
        [Route("params/tags")]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _mediator.Send(new GetTagsQuery());

            return Ok(tags);
        }
    }
}
