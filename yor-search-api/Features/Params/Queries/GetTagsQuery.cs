using MediatR;

using yor_search_api.Features.Params.Models;

namespace yor_search_api.Features.Params.Queries
{
    public class GetTagsQuery : IRequest<IEnumerable<TagResponse>>
    {
    }
}
