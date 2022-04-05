using MediatR;

using Microsoft.EntityFrameworkCore;

using yor_search_api.Features.Search.Models;
using yor_search_api.Features.Specifications;
using yor_search_api.Infrastructure.Repositories.Contracts;

namespace yor_search_api.Features.Search.Queries
{
    public class SearchQueryHandler : IRequestHandler<SearchQuery, IEnumerable<SearchResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITagRepository _tagRepository;

        public SearchQueryHandler(
            IUserRepository userRepository, 
            ITagRepository tagRepository)
        {
            _userRepository = userRepository
                ?? throw new ArgumentNullException(nameof(userRepository));

            _tagRepository = tagRepository
                ?? throw new ArgumentNullException(nameof(tagRepository));
        }

        public async Task<IEnumerable<SearchResponse>> Handle(SearchQuery request, CancellationToken cancellationToken)
        {
            var tags = _tagRepository.Get(new TagsByNamesSpecification(request.Tags));

            var users = _userRepository.Get(
                new SearchSpecification(
                    tags, 
                    request.Gender, 
                    request.Country, 
                    request.City, 
                    request.MinAge, 
                    request.MaxAge))
                .Select(x => SearchResponse.Map(x));

            return await users.ToListAsync();
        }
    }
}
