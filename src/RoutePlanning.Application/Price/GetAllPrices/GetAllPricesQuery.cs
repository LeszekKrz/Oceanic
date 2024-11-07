using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Domain.Users;

namespace RoutePlanning.Application.Price.GetAllPrices;
public sealed record GetAllPricesQuery(AuthenticatedUser user) : IQuery<IReadOnlyList<Domain.Price.Price>>
{
}
