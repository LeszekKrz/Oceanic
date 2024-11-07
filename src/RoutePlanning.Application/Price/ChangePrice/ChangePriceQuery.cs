using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Domain.Users;

namespace RoutePlanning.Application.Price.ChangePrice;
public sealed record ChangePriceQuery(AuthenticatedUser User, string weight, string Type, float Price) : IQuery<bool>
{
}
