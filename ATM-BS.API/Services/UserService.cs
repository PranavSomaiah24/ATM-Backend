using ATM_BS.API.Data;
using ATM_BS.API.Entities;

namespace ATM_BS.API.Services
{
    public class UserService : IUserService
    {
        private readonly ATMBSDbContext _dbcontext;

        public UserService(ATMBSDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public Admin? Validate(string email, string password)
        {
            return _dbcontext.Admins.SingleOrDefault(u => u.Email == email && u.Password == password && u.Enable == true);
        }
    }
}
