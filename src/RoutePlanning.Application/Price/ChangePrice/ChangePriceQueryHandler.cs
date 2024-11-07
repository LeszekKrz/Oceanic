using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Infrastructure.Repositories;

namespace RoutePlanning.Application.Price.ChangePrice;
public sealed class ChangePriceQueryHandler :IQueryHandler<ChangePriceQuery, bool>
{
    public readonly IUserRepository userRepository;
    public readonly IPriceRepository priceRepository;

    public ChangePriceQueryHandler(IUserRepository userRepository, IPriceRepository priceRepository)
    {
        this.userRepository = userRepository;
        this.priceRepository = priceRepository;
    }
    public async Task<bool> Handle(ChangePriceQuery request, CancellationToken cancellationToken)
    {
        var authenticatedUser = await userRepository.GetUserById(request.user.Id, cancellationToken);
        if (authenticatedUser == null)
        {
            return false;
        }
        if (!authenticatedUser.IsAdmin)
        {
            return false;
        }

        await priceRepository.ChangePrice(request.Type, request.Price, cancellationToken);

        return true;
    }
}
