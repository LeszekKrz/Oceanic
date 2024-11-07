using Netcompany.Net.Cqs.Queries;

namespace RoutePlanning.Application.Users.Queries.PromoteUser;
public sealed record PromoteUserQuery(Domain.Users.AuthenticatedUser user, int IdOfUserToPromote) : IQuery<bool>
{
}
