using Moq;
using ATM_BS.API.Controllers;
using ATM_BS.API.Services;
using ATM_BS.API.Entities;

namespace ATM_BSUnitTest.UnitTesting
{
    public class UnitTestAdminService
    {
        //private readonly Mock<IadminService> adminService;
        private readonly Mock<IAdminService> adminService;
        public UnitTestAdminService()
        {
            adminService = new Mock<IAdminService>();
        }

        [Fact]
        public void Test_AddAdmin()
        {
            var admin = new Admin
            {
                Id = 123456,
                Name = "John",
                Email = "john@gmail.com",
                Password = "Abc@1234"
            };
            var adminList = GetAdminsData();
            adminList.Add(admin);
            adminService.Setup(x => x.AddAdmin(admin)).Returns(adminList[3]);
            var result = adminService.Object;
            var adminResult = result.AddAdmin(adminList[3]);
            Assert.NotNull(adminResult);
            Assert.NotEqual(adminList[2].Id, adminResult.Id);
            Assert.True(adminList[3].Id == adminResult.Id);

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