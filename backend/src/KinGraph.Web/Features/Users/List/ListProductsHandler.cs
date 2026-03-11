using KinGraph.UseCases.Users;
using KinGraph.UseCases.Users.List;

namespace KinGraph.Web.Features.Users.List;

public record ListUsersQuery(int? Page = 1, int? PerPage = Constants.DEFAULT_PAGE_SIZE)
    : IQuery<Result<UseCases.PagedResult<UserDto>>>;

public class ListUsersHandler(IListUsersQueryService query)
    : IQueryHandler<ListUsersQuery, Result<UseCases.PagedResult<UserDto>>>
{
    private readonly IListUsersQueryService _query = query;

    public async ValueTask<Result<UseCases.PagedResult<UserDto>>> Handle(
        ListUsersQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await _query.ListAsync(
            request.Page ?? 1,
            request.PerPage ?? Constants.DEFAULT_PAGE_SIZE
        );

        return Result.Success(result);
    }
}
