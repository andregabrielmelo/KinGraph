using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using KinGraph.Core.Aggregates.UserAggregate;

namespace KinGraph.Web.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static UserId? GetUserId(this ClaimsPrincipal principal)
    {
        var sub = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        return int.TryParse(sub, out var id) ? UserId.From(id) : null;
    }
}
