using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Domain.Users;

namespace RoutePlanning.Application.Users.Queries.GetAllUsers;
public sealed record GetAllUsersQuery(Domain.Users.AuthenticatedUser user) : IQuery<IReadOnlyList<User>>
{
}
