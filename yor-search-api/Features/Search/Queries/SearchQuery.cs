using MediatR;

using System.Security.Claims;

using yor_search_api.Features.Search.Models;

namespace yor_search_api.Features.Search.Queries
{
    public class SearchQuery : IRequest<IEnumerable<SearchResponse>>
    {
        public IEnumerable<string> Tags { get; set; }

        public string Gender { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public int MinAge { get; set; }

        public int MaxAge { get; set; }

        public ClaimsPrincipal Claims { get; set; }
    }
}
