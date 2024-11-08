using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Domain.Users;

namespace RoutePlanning.Application.Users.Queries.RemoveUser;
public sealed record RemoveUserQuery(Domain.Users.AuthenticatedUser user, User.EntityId IdOfUserToDelete) : IQuery<bool>
{
}
