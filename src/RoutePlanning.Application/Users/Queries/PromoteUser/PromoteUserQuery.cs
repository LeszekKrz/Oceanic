using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Domain.Users;

namespace RoutePlanning.Application.Users.Queries.PromoteUser;
public sealed record PromoteUserQuery(Domain.Users.AuthenticatedUser user, User.EntityId IdOfUserToPromote) : IQuery<bool>
{
}
