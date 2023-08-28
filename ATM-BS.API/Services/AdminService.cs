using ATM_BS.API.Data;
using ATM_BS.API.Entities;
using ATM_BS.API.Services;

namespace ATM_BS.API.Service
{
    public class AdminService : IAdminService
    {
        private readonly ATMBSDbContext _dbcontext;

        public AdminService(ATMBSDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public Admin AddAdmin(Admin admin)
        {
           var result =  _dbcontext.Admins.Add(admin); //save records to the Admins table
            _dbcontext.SaveChanges();
            return result.Entity;
        }
        public Admin? Validate(string email, string password)
        {
            return _dbcontext.Admins.SingleOrDefault(u => u.Email == email && u.Password == password && u.Enable == true);
        }
        public List<Admin> GetAdmins()
        {
            return _dbcontext.Admins.ToList();
        }
        public Admin GetAdmin(int id)
        {
            Admin admin = _dbcontext.Admins.Find(id);
            return admin;
        }
        public void EditAdmin(Admin admin)
        {
            _dbcontext.Admins.Update(admin);
            _dbcontext.SaveChanges();
        }
        public Admin? CheckEmail(string email)
        {
            return _dbcontext.Admins.SingleOrDefault(u => u.Email == email);
        }
        public Admin? CheckId(int id)
        {
            return _dbcontext.Admins.SingleOrDefault(u => u.Id == id);
        }
    }
}