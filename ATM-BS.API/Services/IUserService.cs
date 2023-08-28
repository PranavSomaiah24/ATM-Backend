using ATM_BS.API.Entities;

namespace ATM_BS.API.Services
{
    public interface IUserService
    {
        Admin? Validate(string username, string password);
        Admin? CheckEmail(string email);
        Admin? CheckId(int id);
    }
}
