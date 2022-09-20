using Pallet.Database.Entities.Users;
using Pallet.Database.Repositories.Interfaces;
using Pallet.Services.Managers.Interfaces;
using System.Security.Cryptography;

namespace Pallet.Services.Managers
{
    public class ManagerUser : IManagerUser
    {
        private readonly IDbRepository<User> _UserRepository;

        public User? LoginedUser { get; set; }

        public IQueryable<User> Users => _UserRepository.Items;

        public ManagerUser(IDbRepository<User> UserRepository)
        {
            _UserRepository = UserRepository;
            "ManagerUser init --------------".CheckStage();
            Login("administrator", "btadmin");
        }

        public bool Login(string name, string password)
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

            string tempHashCode;

            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Computing Hash - returns here byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // now convert byte array to a string
                StringBuilder stringbuilder = new();
                for (int i = 0; i < bytes.Length; i++)
                    stringbuilder.Append(bytes[i].ToString("X2"));

                tempHashCode = stringbuilder.ToString();
            }
            try
            {
                LoginedUser = Users.First(x => x.Name == name && x.Hashcode == tempHashCode);
            }
            catch { return false; }
            return true;
        }

        public void LogOut() => LoginedUser = null;

        public bool IsLogined => LoginedUser != null || !string.IsNullOrEmpty(LoginedUser?.Name);

        public enum UserRoleNum
        {
            None = 0,
            Worker = 1,
            Manager = 2,
            Admin = 10
        }
    }
}