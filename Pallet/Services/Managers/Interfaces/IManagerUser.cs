using Pallet.Database.Entities.Users;

namespace Pallet.Services.Managers.Interfaces;

public interface IManagerUser
{
    public User LoginedUser { get; set; }
    List<User> Users { get; }

    bool Login(string name, string password);

    void LogOut();

    bool IsLogined { get;  }

    enum UserRoleNum : int
    {
        None = 0,
        Worker = 1,
        Manager = 2,
        Admin = 10
    }
}