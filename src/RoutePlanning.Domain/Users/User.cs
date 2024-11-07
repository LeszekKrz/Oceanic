using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Netcompany.Net.DomainDrivenDesign.Models;

namespace RoutePlanning.Domain.Users;

[DebuggerDisplay("{Username}")]
public sealed class User : AggregateRoot<User>
{
    public User(string username, string passwordHash)
    {
        Username = username;
        PasswordHash = passwordHash;
        IsAdmin = true;
        IsEmployee = true;
    }
    public User(string username, string passwordHash, string email, string address, string phoneNumber)
    {
        Username = username;
        PasswordHash = passwordHash;
        Email = email;
        Address = address;
        PhoneNumber = phoneNumber;
        IsAdmin = false;
        IsEmployee = false;
    }

    public string Username { get; set; }

    public string PasswordHash { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public bool IsEmployee { get; set; }

    public bool IsAdmin { get; set; }

    public static string ComputePasswordHash(string password) => Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
}
