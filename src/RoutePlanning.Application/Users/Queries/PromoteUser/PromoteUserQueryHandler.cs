using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Infrastructure.Repositories;

namespace RoutePlanning.Application.Users.Queries.PromoteUser;
public sealed class PromoteUserQueryHandler : IQueryHandler<PromoteUserQuery, bool>
{
    public readonly IUserRepository userRepository;

    public PromoteUserQueryHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    public async Task<bool> Handle(PromoteUserQuery request, CancellationToken cancellationToken)
    {
        var authenticatedUser = await userRepository.getUserByNameOfAuthenticatedUser(request.user.Username, cancellationToken);
        if (authenticatedUser == null)
        {
            return false;
        }
        if (!authenticatedUser.IsAdmin)
        {
            return false;
        }

        await userRepository.PromoteUser(request.IdOfUserToPromote, cancellationToken);

        return true;
    }
}
