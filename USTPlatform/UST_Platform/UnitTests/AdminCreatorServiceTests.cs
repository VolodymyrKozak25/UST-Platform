using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace AdminCreatorServiceTests
{
    public class AdminCreatorServiceTests
    {
        // Arrange
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly AdminCreatorService _adminCreatorService;

        public AdminCreatorServiceTests()
        {
            // Create a mock user manager with the necessary methods
            var userStoreMock = new Mock<IUserStore<User>>();
            _userManagerMock = new Mock<UserManager<User>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((User)null);
            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(x => x.GenerateEmailConfirmationTokenAsync(It.IsAny<User>())).ReturnsAsync("token");
            _userManagerMock.Setup(x => x.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            // Create an instance of the service under test
            _adminCreatorService = new AdminCreatorService();
        }

        // Test the first overload of CreateAdminAsync with single strings
        [Fact]
        public async Task CreateAdminAsync_WithSingleStrings_ShouldCreateAdmin()
        {
            // Arrange
            var firstName = "John";
            var lastName = "Doe";
            var middleName = "Smith";
            var password = "Password123!";
            var email = "john.doe@example.com";

            // Act
            await _adminCreatorService.CreateAdminAsync(_userManagerMock.Object, firstName, lastName, middleName, password, email);

            // Assert
            // Verify that the user manager methods were called with the expected arguments
            _userManagerMock.Verify(x => x.FindByEmailAsync(email), Times.Once);
            _userManagerMock.Verify(x => x.CreateAsync(It.Is<User>(u => u.FirstName == firstName && u.LastName == lastName && u.MiddleName == middleName && u.Email == email), password), Times.Once);
            _userManagerMock.Verify(x => x.AddToRoleAsync(It.Is<User>(u => u.FirstName == firstName && u.LastName == lastName && u.MiddleName == middleName && u.Email == email), "Admin"), Times.Once);
            _userManagerMock.Verify(x => x.GenerateEmailConfirmationTokenAsync(It.Is<User>(u => u.FirstName == firstName && u.LastName == lastName && u.MiddleName == middleName && u.Email == email)), Times.Once);
            _userManagerMock.Verify(x => x.ConfirmEmailAsync(It.Is<User>(u => u.FirstName == firstName && u.LastName == lastName && u.MiddleName == middleName && u.Email == email), "token"), Times.Once);
        }

        // Test the second overload of CreateAdminAsync with single user
        [Fact]
        public async Task CreateAdminAsync_WithSingleUser_ShouldCreateAdmin()
        {
            // Arrange
            var admin = new User
            {
                UserName = "jane.doe",
                FirstName = "Jane",
                LastName = "Doe",
                MiddleName = "Smith",
                Email = "jane.doe@example.com"
            };
            var password = "Password123!";

            // Act
            await _adminCreatorService.CreateAdminAsync(_userManagerMock.Object, admin, password);

            // Assert
            // Verify that the user manager methods were called with the expected arguments
            _userManagerMock.Verify(x => x.FindByEmailAsync(admin.Email), Times.Once);
            _userManagerMock.Verify(x => x.CreateAsync(admin, password), Times.Once);
            _userManagerMock.Verify(x => x.AddToRoleAsync(admin, "Admin"), Times.Once);
            _userManagerMock.Verify(x => x.GenerateEmailConfirmationTokenAsync(admin), Times.Once);
            _userManagerMock.Verify(x => x.ConfirmEmailAsync(admin, "token"), Times.Once);
        }

        // Test the third overload of CreateAdminAsync with list of admins
        [Fact]
        public async Task CreateAdminAsync_WithListOfAdmins_ShouldCreateAdmins()
        {
            // Arrange
            var admins = new List<(User, string)>
            {
                (new User { UserName = "bob.smith", FirstName = "Bob", LastName = "Smith", MiddleName = "Jones", Email = "bob.smith@example.com" }, "Password123!"),
                (new User { UserName = "alice.jones", FirstName = "Alice", LastName = "Jones", MiddleName = "Smith", Email = "alice.jones@example.com" }, "Password123!")
            };

            // Act
            await _adminCreatorService.CreateAdminAsync(_userManagerMock.Object, admins);

            // Assert
            // Verify that the user manager methods were called with the expected arguments for each admin
            foreach (var admin in admins)
            {
                _userManagerMock.Verify(x => x.FindByEmailAsync(admin.Item1.Email), Times.Once);
                _userManagerMock.Verify(x => x.CreateAsync(admin.Item1, admin.Item2), Times.Once);
                _userManagerMock.Verify(x => x.AddToRoleAsync(admin.Item1, "Admin"), Times.Once);
                _userManagerMock.Verify(x => x.GenerateEmailConfirmationTokenAsync(admin.Item1), Times.Once);
                _userManagerMock.Verify(x => x.ConfirmEmailAsync(admin.Item1, "token"), Times.Once);
            }
        }
    }
}
