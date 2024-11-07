using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Domain.Booking;
using RoutePlanning.Domain.Users;
using RoutePlanning.Infrastructure.Repositories;

namespace RoutePlanning.Application.Users.Queries.GetAllUsers;
public sealed class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery,IReadOnlyList<User>>
{
    public readonly IUserRepository userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    public async Task<IReadOnlyList<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var authenticatedUser = await userRepository.GetUserById(request.user.Id, cancellationToken);
        if (authenticatedUser == null)
        {
            return new List<User>();
        }
        if (!authenticatedUser.IsAdmin)
        {
            return new List<User>();
        }

        var users = userRepository.GetAllUsers(cancellationToken);

        return await users;
    }
}
