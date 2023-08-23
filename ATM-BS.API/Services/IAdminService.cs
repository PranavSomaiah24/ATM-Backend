using ATM_BS.API.Entities;

namespace ATM_BS.API.Services
{
    public interface IAdminService
    {
        Admin AddAdmin(Admin admin);
        Admin? Validate(string username, string password);
    }
}