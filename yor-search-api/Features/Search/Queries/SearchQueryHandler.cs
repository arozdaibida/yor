using MediatR;

using Microsoft.EntityFrameworkCore;

using yor_search_api.Features.Specifications;
using yor_search_api.Infrastructure.Repositories.Contracts;
using yor_search_api.Models;

namespace yor_search_api.Features.Search.Queries
{
    public class SearchQueryHandler : IRequestHandler<SearchQuery, IEnumerable<User>>
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

        public async Task<IEnumerable<User>> Handle(SearchQuery request, CancellationToken cancellationToken)
        {
            var tags = _tagRepository.Get(new TagsByNamesSpecification(request.Tags));


            //TODO add user response mode and map 
            //TODO add specification and or operators and implement search
            var users = _userRepository.Get(
                new SearchSpecification(
                    tags, 
                    request.Gender, 
                    request.Country, 
                    request.City, 
                    request.MinAge, 
                    request.MaxAge));

            return await users.ToListAsync();
        }
    }
}
