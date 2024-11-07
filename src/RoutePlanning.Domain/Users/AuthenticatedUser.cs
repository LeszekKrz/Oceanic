using System.Linq.Expressions;

namespace RoutePlanning.Domain.Users;

public sealed record AuthenticatedUser(User.EntityId Id, string Username)
{
    public static Expression<Func<User, AuthenticatedUser>> MapAuthenticatedUser => u => new AuthenticatedUser(u.Id, u.Username);
}
