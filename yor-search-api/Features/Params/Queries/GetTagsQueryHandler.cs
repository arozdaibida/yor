using MediatR;

using Microsoft.EntityFrameworkCore;

using yor_search_api.Features.Params.Models;
using yor_search_api.Features.Specifications;
using yor_search_api.Infrastructure.Repositories.Contracts;

namespace yor_search_api.Features.Params.Queries
{
    public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, IEnumerable<TagResponse>>
    {
        private readonly ITagRepository _tagRepository;

        public GetTagsQueryHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository
                ?? throw new ArgumentNullException(nameof(tagRepository));
        }

        public async Task<IEnumerable<TagResponse>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
        {
            var tags = _tagRepository
                .Get(new TagsSpecification())
                .Select(x =>
                    new TagResponse
                    {
                        Id = x.Id,
                        Name = x.Name,
                    });

            return await tags.ToListAsync();
        }
    }
}
