using Microsoft.EntityFrameworkCore;
using Pallet.InternalDatabase.Context;
using Pallet.InternalDatabase.Entities.Users;
using Pallet.Services.Managers.Interfaces;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;

namespace Pallet.Services.Managers
{
    public class ManagerUser : IManagerUser
    {
        private readonly DbSet<User> _dbSet;
        private readonly InternalDbContext _dbContext;

        public User? LoginedUser
        {
            get => _LoginedUser;
            private set
            {
                _LoginedUser = value;
                OnLoginedUserChanged();
            }
        }

        private User? _LoginedUser;

        public ObservableCollection<User> Users { get; private set; }

        public event EventHandler LoginedUserChanged;

        private void OnLoginedUserChanged() => LoginedUserChanged?.Invoke(this, new());

        public ManagerUser(
            InternalDbContext internalDbContext
            )
        {
            _dbContext = internalDbContext;
            _dbSet = internalDbContext.Users;
            Users = new(_dbSet);
            Users.CollectionChanged += Users_CollectionChanged;
        }

        private void Users_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems is not null)
            {
                foreach (User item in e.OldItems)
                {
                    item.UserChanged -= Item_UserChanged;
                    _dbSet.Remove(item);
                    _dbContext.SaveChanges();
                }
            }

            if (e.NewItems is not null)
            {
                foreach (User item in e.NewItems)
                {
                    item.UserChanged += Item_UserChanged;
                    _dbSet.AddAsync(item);
                    _dbContext.SaveChanges();
                }
            }
        }

        private void Item_UserChanged(object? sender, EventArgs e)
        {
            _dbSet.Update(sender as User);

            _dbContext.SaveChanges();
        }

        public async Task<bool> RegisterNewUser(string name, string password, string role, string description = "")
        {
            description ??= "";
            User newUser = new()
            {
                Name = name,
                Hashcode = GetHashCode(password),
                Description = description,
                AdminRegisteredName = LoginedUser.Name,
                RoleNum = (int)Enum.Parse(typeof(UserRoleNum), role),
                RegistrationTime = DateTime.Now
            };
            try
            {
                Users.Add(newUser);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ModifyUser(string oldName, string name, string password, string role, string description = "")
        {
            try
            {
                User editableUser = Users.First(s => s.Name == oldName);
                editableUser.Name = name;
                editableUser.Description = description;
                editableUser.Hashcode = GetHashCode(password);
                editableUser.RoleNum = (int)Enum.Parse(typeof(UserRoleNum), role);
                editableUser.AdminRegisteredName = LoginedUser.Name;
                editableUser.RegistrationTime = DateTime.Now;
            }
            catch { return false; }
            return true;
        }

        public async Task<bool> DeleteUser(User user)
        {
            if (!Users.Any(s => s.Name == user.Name)) return false;
            Users.Remove(Users.First(s => s.Name == user.Name));
            return true;
        }

        public async Task<bool> Login(string name, string password)
        {
            if (name == "administrator" && password == "btadmin")
            {
                LoginedUser = new User()
                {
                    Name = name,
                    RoleNum = 15,
                    Description = "Built-In Administrator"
                };
                return true;
            }

            //Create a SHA256 hash from string

            try
            {
                LoginedUser = Users.First(x => x.Name == name && x.Hashcode == GetHashCode(password));
            }
            catch { return false; }
            return true;
        }

        private string GetHashCode(string value)
        {
            using SHA256 sha256Hash = SHA256.Create();
            // Computing Hash - returns here byte array
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(value));

            // now convert byte array to a string
            StringBuilder stringbuilder = new();
            for (int i = 0; i < bytes.Length; i++)
                stringbuilder.Append(bytes[i].ToString("X2"));

            return stringbuilder.ToString();
        }

        public async Task LogOut() => LoginedUser = null;

        public enum UserRoleNum
        {
            None = 0,
            Worker = 1,
            Manager = 2,
            Admin = 10
        }
    }
}