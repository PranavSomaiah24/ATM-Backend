using ATM_BS.API.Entities;

namespace ATM_BS.API.Services
{
    public interface IAdminService
    {
        void AddAdmin(Admin admin);
        Admin? Validate(string username, string password);
    }
}