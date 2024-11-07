using Microsoft.EntityFrameworkCore.Design;
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
        var authenticatedUser = await userRepository.getUserByNameOfAuthenticatedUser(request.User.Username, cancellationToken);
        if (authenticatedUser == null)
        {
            return false;
        }
        if (!authenticatedUser.IsAdmin || !authenticatedUser.IsEmployee)
        {
            return false;
        }

        await priceRepository.ChangePrice(request.Type, request.weight, request.Price, cancellationToken);

        return true;
    }
}
