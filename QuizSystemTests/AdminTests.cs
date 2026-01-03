using System.Net;
using System.Numerics;
using System.Reflection;
using System.Xml.Linq;
using UlsterQuizSystem;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

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
    [TestMethod]
    public void AdminID_ShouldSetCorrectly()
    {
        // Arrange
        int adminID = 1;
        string adminUsername = "John";
        string adminPassword = "Doe";
        string adminEmail = "johndoe05@outlook.com";

        // Act
        _admins.Add(new Admin(adminID, adminUsername, adminPassword, adminEmail));

        // Assert
        Assert.AreEqual(adminID, _admins[1].ID, "AdminID was not set correctly.");
    }

    [TestMethod]
    public void AdminID_Setter_ShouldBeProtected()
    {
        // Arrange
        PropertyInfo property = typeof(Admin).GetProperty("ID");

        // Act
        MethodInfo setter = property.SetMethod;

        // Assert
        Assert.IsTrue(setter.IsFamily, "AdminID setter should be protected.");
    }

    [TestMethod]
    public void AdminProperties_ShouldSetAndGetValues()
    {
        // Arrange
        string adminUsername = "Johned";
        string adminPassword = "Doed";
        string adminEmail = "johndoe05@outlook.co.uk";
        UserRole adminRole = UserRole.Student;
        DateTime adminLogInDate = DateTime.MaxValue;

        // Act
        _admins[0].Username = adminUsername;
        _admins[0].Password = adminPassword;
        _admins[0].Email = adminEmail;
        _admins[0].Role = adminRole;
        _admins[0].LoginDate = adminLogInDate;

        // Assert
        Assert.AreEqual(adminUsername, _admins[0].Username, "Username was not set correctly.");
        Assert.AreEqual(adminPassword, _admins[0].Password, "Password was not set correctly.");
        Assert.AreEqual(adminEmail, _admins[0].Email, "Email was not set correctly.");
        Assert.AreEqual(adminRole, _admins[0].Role, "Role was not set correctly.");
        Assert.AreEqual(adminLogInDate, _admins[0].LoginDate, "LoginDate was not set correctly.");
    }

    // ==========================================
    // Method Tests
    // ==========================================
}
