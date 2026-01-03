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
        int adminID = 0;
        string adminUsername = "default";
        string adminPassword = "password";
        string adminEmail = "email@mail.com";
        string adminRole = "admin";
        DateTime adminLogInDate = DateTime.MinValue;

        // Act
        Admin admin = new Admin();

        // Assert
        Assert.AreEqual(adminID, admin.ID);
        Assert.AreEqual(adminUsername, admin.Username);
        Assert.AreEqual(adminPassword, admin.Password);
        Assert.AreEqual(adminEmail, admin.Email);
        Assert.AreEqual(adminRole, admin.Role);
        Assert.AreEqual(adminLogInDate, admin.LoginDate);
    }

    [TestMethod]
    public void AdminParameterisedConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        int adminID = 0;
        string adminUsername = "John";
        string adminPassword = "Doe";
        string adminEmail = "johndoe05@outlook.com";
        string adminRole = "admin";
        DateTime adminLogInDate = DateTime.MinValue;

        // Act
        Admin admin = new Admin(adminID, adminUsername, adminPassword, adminEmail);

        // Assert
        Assert.AreEqual(adminID, admin.ID);
        Assert.AreEqual(adminUsername, admin.Username);
        Assert.AreEqual(adminPassword, admin.Password);
        Assert.AreEqual(adminEmail, admin.Email);
        Assert.AreEqual(adminRole, admin.Role);
        Assert.AreEqual(adminLogInDate, admin.LoginDate);
    }
}
