using RoutePlanning.Domain.Users;

namespace RoutePlanning.Application.Price.ChangePrice;
public sealed record ChangePriceQuery(AuthenticatedUser User, string Type, float Price)
{
}
