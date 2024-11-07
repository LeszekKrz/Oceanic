using Microsoft.EntityFrameworkCore;
using RoutePlanning.Domain.Users;
using RoutePlanning.Infrastructure.Database;

namespace RoutePlanning.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{

    protected readonly RoutePlanningDatabaseContext _context;
    public IQueryable<User> Queryable => GetDbSetWithIncludes();

    protected virtual IQueryable<User> GetDbSetWithIncludes()
    {
        return _context.Set<User>().AsNoTracking();
    }

    public UserRepository(RoutePlanningDatabaseContext context)
    {
        _context = context;
    }

    public async Task<AuthenticatedUser?> getAuthenticatedUser(string name, string hashedPassword, CancellationToken cancellationToken)
    {
        var user = await Queryable
            .Where(u => u.Username.ToLower() == name.ToLower())
            .Where(u => u.PasswordHash == hashedPassword)
            .Select(AuthenticatedUser.MapAuthenticatedUser)
            .SingleOrDefaultAsync(cancellationToken);

        return user; 
    }
}

public interface IUserRepository
{
    public Task<AuthenticatedUser?> getAuthenticatedUser(string name, string hashedPassword, CancellationToken cancellationToken);
}
