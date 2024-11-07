using Netcompany.Net.Cqs.Queries;

namespace RoutePlanning.Application.Price.ChangePrice;
public sealed class ChangePriceQueryHandler :IQueryHandler<ChangePriceQuery, bool>
{
    public Task<bool> Handle(ChangePriceQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
