using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Domain.Users;
using RoutePlanning.Infrastructure.Repositories;

namespace RoutePlanning.Application.Users.Queries.AuthenticatedUser;

public sealed class AuthenticatedUserQueryHandler : IQueryHandler<AuthenticatedUserQuery, Domain.Users.AuthenticatedUser?>
{
    private readonly IUserRepository userRepository;

    public AuthenticatedUserQueryHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<Domain.Users.AuthenticatedUser?> Handle(AuthenticatedUserQuery request, CancellationToken cancellationToken)
    {
        // Note that it is considered bad practise to roll your own security like this. Use establised frameworks and services for authentication instead.
        var hashedPassword = User.ComputePasswordHash(request.Password);

        var authenticatedUser = await userRepository.getAuthenticatedUser(request.Username, hashedPassword, cancellationToken);

        return authenticatedUser;
    }
}
