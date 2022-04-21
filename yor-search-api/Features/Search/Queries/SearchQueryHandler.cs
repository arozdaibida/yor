using MediatR;

using Microsoft.EntityFrameworkCore;

using yor_search_api.Features.Search.Models;
using yor_search_api.Features.Specifications;
using yor_search_api.Infrastructure.Repositories.Contracts;
using yor_search_api.Models;

namespace yor_search_api.Features.Search.Queries
{
    public class SearchQueryHandler : IRequestHandler<SearchQuery, IEnumerable<SearchResponse>>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Tag> _tagRepository;

        public SearchQueryHandler(
            IRepository<User> userRepository,
            IRepository<Tag> tagRepository)
        {
            _userRepository = userRepository
                ?? throw new ArgumentNullException(nameof(userRepository));

            _tagRepository = tagRepository
                ?? throw new ArgumentNullException(nameof(tagRepository));
        }

        public async Task<IEnumerable<SearchResponse>> Handle(SearchQuery request, CancellationToken cancellationToken)
        {
            var tags = _tagRepository.Get(new TagsByNamesSpecification(request.Tags));

            //var currentUser = await _currentUserService.GetCurrentUser(request.Claims, cancellationToken);
            
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
