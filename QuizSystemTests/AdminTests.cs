using System.Net;
using System.Numerics;
using System.Xml.Linq;
using UlsterQuizSystem;

namespace QuizSystemTests;

[TestClass]
public class AdminTests
{
    [TestMethod]
    public void AdminDefaultConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        int adminID = 1;
        string adminUsername = "default";
        string adminPassword = "password";
        string adminEmail = "email@mail.com";
        UserRole adminRole = UserRole.Admin;
        DateTime adminLogInDate = DateTime.MinValue;

        // Act
        Admin admin = new Admin();

        // Assert
        Assert.AreEqual(adminID, admin.ID, "ID was not initialized correctly.");
        Assert.AreEqual(adminUsername, admin.Username, "Username was not initialized correctly.");
        Assert.AreEqual(adminPassword, admin.Password, "Password was not initialized correctly.");
        Assert.AreEqual(adminEmail, admin.Email, "Email was not initialized correctly.");
        Assert.AreEqual(adminRole, admin.Role, "Role was not initialized correctly.");
        Assert.AreEqual(adminLogInDate, admin.LoginDate, "LoginDate was not initialized correctly.");
    }

    [TestMethod]
    public void AdminParameterisedConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        int adminID = 1;
        string adminUsername = "John";
        string adminPassword = "Doe";
        string adminEmail = "johndoe05@outlook.com";
        UserRole adminRole = UserRole.Admin;
        DateTime adminLogInDate = DateTime.MinValue;

        // Act
        Admin admin = new Admin(adminID, adminUsername, adminPassword, adminEmail);

        // Assert
        Assert.AreEqual(adminID, admin.ID, "ID was not initialized correctly.");
        Assert.AreEqual(adminUsername, admin.Username, "Username was not initialized correctly.");
        Assert.AreEqual(adminPassword, admin.Password, "Password was not initialized correctly.");
        Assert.AreEqual(adminEmail, admin.Email, "Email was not initialized correctly.");
        Assert.AreEqual(adminRole, admin.Role, "Role was not initialized correctly.");
        Assert.AreEqual(adminLogInDate, admin.LoginDate, "LoginDate was not initialized correctly.");
    }
}
