using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Domain.Users;
using RoutePlanning.Infrastructure.Repositories;

namespace RoutePlanning.Application.Users.Queries.CreateUser;
public sealed class CreateUserQueryHandler : IQueryHandler<CreateUserQuery, Domain.Users.AuthenticatedUser?>
{

    private readonly IUserRepository userRepository;

    public CreateUserQueryHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    public Task<Domain.Users.AuthenticatedUser?> Handle(CreateUserQuery request, CancellationToken cancellationToken)
    {
        var user = new User(request.username, request.password);
        var authenticateUser = userRepository.createUser(user, cancellationToken);

        return authenticateUser;
    }
}
