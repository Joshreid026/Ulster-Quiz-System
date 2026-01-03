using System.Net;
using System.Numerics;
using System.Xml.Linq;
using UlsterQuizSystem;

namespace QuizSystemTests;

[TestClass]
public class AdminTests
{
    // ==========================================
    // Test Initialization + Fields
    // ==========================================
    private List<Admin> _admins;

    [TestInitialize]
    public void Setup()
    {
        //Required Sample Data
        _admins = new List<Admin>();
        _admins.Add(new Admin());
    }

    // ==========================================
    // Constructor Tests
    // ==========================================
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

        // Assert
        Assert.AreEqual(adminID, _admins[0].ID, "ID was not initialized correctly.");
        Assert.AreEqual(adminUsername, _admins[0].Username, "Username was not initialized correctly.");
        Assert.AreEqual(adminPassword, _admins[0].Password, "Password was not initialized correctly.");
        Assert.AreEqual(adminEmail, _admins[0].Email, "Email was not initialized correctly.");
        Assert.AreEqual(adminRole, _admins[0].Role, "Role was not initialized correctly.");
        Assert.AreEqual(adminLogInDate, _admins[0].LoginDate, "LoginDate was not initialized correctly.");
    }

    [TestMethod]
    public void AdminParameterisedConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        int adminID = 2;
        string adminUsername = "John";
        string adminPassword = "Doe";
        string adminEmail = "johndoe05@outlook.com";
        UserRole adminRole = UserRole.Admin;
        DateTime adminLogInDate = DateTime.MinValue;

        // Act
        _admins.Add(new Admin(adminID, adminUsername, adminPassword, adminEmail));

        // Assert
        Assert.AreEqual(adminID, _admins[1].ID, "ID was not initialized correctly.");
        Assert.AreEqual(adminUsername, _admins[1].Username, "Username was not initialized correctly.");
        Assert.AreEqual(adminPassword, _admins[1].Password, "Password was not initialized correctly.");
        Assert.AreEqual(adminEmail, _admins[1].Email, "Email was not initialized correctly.");
        Assert.AreEqual(adminRole, _admins[1].Role, "Role was not initialized correctly.");
        Assert.AreEqual(adminLogInDate, _admins[1].LoginDate, "LoginDate was not initialized correctly.");
    }

    // ==========================================
    // Get/Set Tests
    // ==========================================


    // ==========================================
    // Method Tests
    // ==========================================
}
