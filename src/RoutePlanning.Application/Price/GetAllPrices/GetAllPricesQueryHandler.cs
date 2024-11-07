using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Infrastructure.Repositories;

namespace RoutePlanning.Application.Price.GetAllPrices;
public sealed class GetAllPricesQueryHandler : IQueryHandler<GetAllPricesQuery, IReadOnlyList<Domain.Price.Price>>
{
    public readonly IUserRepository userRepository;
    public readonly IPriceRepository priceRepository;

    public GetAllPricesQueryHandler(IUserRepository userRepository, IPriceRepository priceRepository)
    {
        this.userRepository = userRepository;
        this.priceRepository = priceRepository;
    }
    public async Task<IReadOnlyList<Domain.Price.Price>> Handle(GetAllPricesQuery request, CancellationToken cancellationToken)
    {
        var authenticatedUser = await userRepository.getUserByNameOfAuthenticatedUser(request.user.Username, cancellationToken);
        if (authenticatedUser == null)
        {
            return new List<Domain.Price.Price>();
        }
        if (!authenticatedUser.IsAdmin || !authenticatedUser.IsEmployee)
        {
            return new List<Domain.Price.Price>();
        }

        var prices = await priceRepository.getAllPrices(cancellationToken);

        return prices;
    }
}
