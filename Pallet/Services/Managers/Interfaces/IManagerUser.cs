using Pallet.Database.Entities.Users;

namespace Pallet.Services.Managers.Interfaces;

/// <summary>
/// User manager.
/// </summary>
public interface IManagerUser
{
    /// <summary>
    /// Gets or sets the logined user.
    /// </summary>
    public User LoginedUser { get; protected set; }

    /// <summary>
    /// List of users from database.
    /// </summary>
    List<User> Users { get; }

    /// <summary>
    /// Login by login and password.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="password">The password.</param>
    /// <returns>A bool.</returns>
    bool Login(string name, string password);

    /// <summary>
    /// Logout.
    /// </summary>
    void LogOut();

    /// <summary>
    /// Is logined.
    /// </summary>
    bool IsLogined { get; }

    /// <summary>
    /// The user role numbers (see in database).
    /// </summary>
    enum UserRoleNum
    {
        None = 0,
        Worker = 1,
        Manager = 2,
        Admin = 10
    }
}