using Microsoft.EntityFrameworkCore;
using Netcompany.Net.DomainDrivenDesign.Models;
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

    public Task<AuthenticatedUser?> createUser(User user, CancellationToken cancellationToken)
    {
        _context.Set<User>().Add(user);
        _context.SaveChangesAsync(cancellationToken);
        var authenticatedUser = getAuthenticatedUser(user.Username, user.PasswordHash, cancellationToken);

        return authenticatedUser;
    }

    public Task<User?> getUserByNameOfAuthenticatedUser(string username, CancellationToken cancellationToken)
    {
        var user = Queryable
            .Where(u => u.Username == username)
            .SingleOrDefaultAsync(cancellationToken);

        return user;
    }

    public Task<User?> GetUserById(Entity<User, Guid>.EntityId id, CancellationToken cancellationToken)
    {
        var user = Queryable.Where(u => u.Id == id)
            .SingleOrDefaultAsync(cancellationToken);

        return user;
    }

    public async Task<IReadOnlyList<User>> GetAllUsers(CancellationToken cancellationToken)
    {
        var users = await Queryable.ToListAsync(cancellationToken);
        return users;
    }

    public async Task PromoteUser(User.EntityId id, CancellationToken cancellationToken)
    {
        var existingUser = await _context.Set<User>().FindAsync(new object[] { id }, cancellationToken);

        if (existingUser == null)
        {
            throw new ArgumentException("User not found");
        }

        // Set the isEmployee property to true
        existingUser.IsEmployee = true;

        // Save the changes to the database
        _context.Set<User>().Update(existingUser);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveUser(User.EntityId id, CancellationToken cancellationToken)
    {
        var existingUser = await _context.Set<User>().FindAsync(new object[] {id }, cancellationToken);

        if (existingUser == null)
        {
            throw new ArgumentException("User not found");
        }

        // Set the isEmployee property to true
        existingUser.IsEmployee = true;

        // Save the changes to the database
        _context.Set<User>().Remove(existingUser);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

public interface IUserRepository
{
    public Task<AuthenticatedUser?> getAuthenticatedUser(string name, string hashedPassword, CancellationToken cancellationToken);

    public Task<AuthenticatedUser?> createUser(User user, CancellationToken cancellationToken);

    public Task<User?> getUserByNameOfAuthenticatedUser(string username, CancellationToken cancellationToken);

    public Task<User?> GetUserById(Entity<User, Guid>.EntityId id, CancellationToken cancellationToken);

    public Task<IReadOnlyList<User>> GetAllUsers(CancellationToken cancellationToken);

    public Task PromoteUser(User.EntityId id, CancellationToken cancellationToken);

    public Task RemoveUser(User.EntityId id, CancellationToken cancellationToken);
}
