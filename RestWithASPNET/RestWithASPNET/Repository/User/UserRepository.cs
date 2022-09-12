using RestWithASPNET.Data.VO;
using RestWithASPNET.Models.Context;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
namespace RestWithASPNET.Repository.User
{
    public class UserRepository : IUserRepository
    {
        private readonly MySQLContext _context;

        public UserRepository(MySQLContext context)
        {
            _context = context;
        }

        public Models.Entites.User ValidateCredentials(UserVO user)
        {

            var pass = ComputeHash(user.Passsword, new SHA256CryptoServiceProvider());
            return _context.Users.FirstOrDefault(u => (u.UserName == user.UserName) && (u.PassWord == pass));
        }

        public Models.Entites.User RefreshUserInfor(Models.Entites.User user)
        {
            if (!Exists(user.Id)) return null;
            try
            {
                var result = _context.Users.FirstOrDefault(u => u.Id.Equals(user.Id));
                _context.Entry(result).CurrentValues.SetValues(user);
                _context.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool RevokeToken(string username)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == username);
            if (user == null) return false;
            user.RefreshToken = null;
            _context.SaveChanges();
            return true;
        }

        private string ComputeHash(string passsword, SHA256CryptoServiceProvider algorithm)
        {
            Byte[] inputByte = Encoding.UTF8.GetBytes(passsword);
            Byte[] hashedBytes = algorithm.ComputeHash(inputByte);

            return BitConverter.ToString(hashedBytes);
        }
        public bool Exists(long id) => _context.Users.Any(x => x.Id.Equals(id));

        public Models.Entites.User ValidateCredentials(string username)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == username);
        }


    }
}
