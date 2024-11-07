using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Infrastructure.Repositories;

namespace RoutePlanning.Application.Users.Queries.RemoveUser;
public sealed class RemoveUserQueryHandler(IUserRepository userRepository) : IQueryHandler<RemoveUserQuery, bool>
{
    public readonly IUserRepository UserRepository = userRepository;

    public async Task<bool> Handle(RemoveUserQuery request, CancellationToken cancellationToken)
    {
        var authenticatedUser = await UserRepository.GetUserById(request.user.Id, cancellationToken);
        if (authenticatedUser == null)
        {
            return false;
        }
        if (!authenticatedUser.IsAdmin)
        {
            return false;
        }

        await UserRepository.RemoveUser(request.IdOfUserToDelete, cancellationToken);

        return true;
    }
}
