using Moq;
using ATM_BS.API.Controllers;
using ATM_BS.API.Services;
using ATM_BS.API.Entities;
using ATM_BS.API.Data;
using ATM_BS.API.Service;
using Microsoft.EntityFrameworkCore;
using ATM_BSUnitTest.UnitTesting;
namespace ATM_BSUnitTest.UnitTesting
{
    public class UnitTestAdminService
    {
        private DbContextOptions<ATMBSDbContext> dbContextOptions;
        private ATMBSDbContext db;
        private AdminService? adminService;
        public UnitTestAdminService()
        {

            dbContextOptions = new DbContextOptionsBuilder<ATMBSDbContext>().UseSqlServer(Variables.ConnectionString).Options;
           // dbContextOptions = new DbContextOptionsBuilder<ATMBSDbContext>().UseInMemoryDatabase(dbName).Options;
            db = new ATMBSDbContext(dbContextOptions);
        }

        [Fact]
        public void TestAdmin()
        {
            Admin admin = new Admin
            {
                Id = 222222,
                Name = "Stephen",
                Email = "stephen@gmail.com",
                Password = "Abc@1234",
                Enable = true,
            };

            adminService = new AdminService(db);
            
            admin = adminService.AddAdmin(admin);
            Assert.NotNull(admin);


            admin = adminService.Validate(admin.Name, admin.Password);
            Assert.NotNull(admin);
            admin = adminService.Validate("ygciyg","Abc@1234");
            Assert.Null(admin);

        }

        private List<Admin> GetAdminsData()
        {
            List<Admin> adminsData = new List<Admin>
        {
            new Admin
            {
                Id = 111111,
                Name = "Stephen",
                Email = "stephen@gmail.com",
                Password = "Abc@1234"
            },
            new Admin
            {
                Id = 222222,
                Name = "Stacy",
                Email = "stacy@gmail.com",
                Password = "Abc@1234"
            },
             new Admin
            {
                Id = 333333,
                Name = "Krishna",
                Email = "krishna@gmail.com",
                Password = "Abc@1234"
            },
        };
            return adminsData;
        }
    }
}