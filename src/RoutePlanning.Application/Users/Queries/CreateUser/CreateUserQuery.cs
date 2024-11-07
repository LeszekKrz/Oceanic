using Netcompany.Net.Cqs.Queries;

namespace RoutePlanning.Application.Users.Queries.CreateUser;
public sealed record CreateUserQuery(string username, string password) : IQuery<Domain.Users.AuthenticatedUser?>
{
}
