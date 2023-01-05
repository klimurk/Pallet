using Pallet.InternalDatabase.Entities.Users;
using System.Collections.ObjectModel;

namespace Pallet.Services.Managers.Interfaces;

/// <summary>
/// User manager.
/// </summary>
public interface IManagerUser
{
    /// <summary>
    /// Gets or sets the logined user.
    /// </summary>
    public User LoginedUser { get; }

    /// <summary>
    /// Query of users from database.
    /// </summary>
    ObservableCollection<User> Users { get; }

    /// <summary>
    /// Login by login and password.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="password">The password.</param>
    /// <returns>A bool.</returns>
    Task<bool> Login(string name, string password);

    /// <summary>
    /// Logout.
    /// </summary>
    Task LogOut();

    event EventHandler LoginedUserChanged;

    public Task<bool> RegisterNewUser(string name, string password, string role, string description = "");

    public Task<bool> ModifyUser(string oldName, string name, string password, string role, string description = "");

    public Task<bool> DeleteUser(User user);

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