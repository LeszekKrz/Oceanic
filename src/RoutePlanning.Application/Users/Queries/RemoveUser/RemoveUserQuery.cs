using Netcompany.Net.Cqs.Queries;

namespace RoutePlanning.Application.Users.Queries.RemoveUser;
public sealed record RemoveUserQuery(Domain.Users.AuthenticatedUser user, int IdOfUserToDelete) : IQuery<bool>
{
}
