using System.Linq.Expressions;
using Netcompany.Net.DomainDrivenDesign.Models;

namespace RoutePlanning.Domain.Users;

public sealed class AuthenticatedUser
{
    public AuthenticatedUser(User.EntityId Id, string Username, bool isAdmin)
    {
        this.Id = Id;
        this.Username = Username;
        IsAdmin = isAdmin;
    }

    public AuthenticatedUser(string username)
    {
        Username = username;
        Id = new Entity<User, Guid>.EntityId();
    }

    public User.EntityId Id { get; set; }
    public string Username { get; set; }
    = string.Empty;
    public bool IsAdmin { get; set; } = false;
    public static Expression<Func<User, AuthenticatedUser>> MapAuthenticatedUser => u => new AuthenticatedUser(u.Id, u.Username, u.IsAdmin);
}
