using Netcompany.Net.Cqs.Queries;

namespace RoutePlanning.Application.Users.Queries.CreateUser;
public sealed record CreateUserQuery(string username, string password, string email, string address, string number) : IQuery<Domain.Users.AuthenticatedUser?>
{
}
