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
    private List<Admin> admins;

    [TestInitialize]
    public void Setup()
    {
        // Required Sample Data
        Admin.ResetNextIDCounter();
        admins = new List<Admin>();
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

        // Act
        admins.Add(new Admin());

        // Assert
        Assert.AreEqual(adminID, admins[0].ID, "ID was not initialized correctly.");
        Assert.AreEqual(adminUsername, admins[0].Username, "Username was not initialized correctly.");
        Assert.AreEqual(adminPassword, admins[0].Password, "Password was not initialized correctly.");
        Assert.AreEqual(adminEmail, admins[0].Email, "Email was not initialized correctly.");
        Assert.AreEqual(adminRole, admins[0].Role, "Role was not initialized correctly.");
        Assert.AreEqual(adminLogInDate, admins[0].LoginDate, "LoginDate was not initialized correctly.");
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
        admins.Add(new Admin(adminUsername, adminPassword, adminEmail));

        // Assert
        Assert.AreEqual(adminID, admins[0].ID, "ID was not initialized correctly.");
        Assert.AreEqual(adminUsername, admins[0].Username, "Username was not initialized correctly.");
        Assert.AreEqual(adminPassword, admins[0].Password, "Password was not initialized correctly.");
        Assert.AreEqual(adminEmail, admins[0].Email, "Email was not initialized correctly.");
        Assert.AreEqual(adminRole, admins[0].Role, "Role was not initialized correctly.");
        Assert.AreEqual(adminLogInDate, admins[0].LoginDate, "LoginDate was not initialized correctly.");
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
        admins.Add(new Admin(adminUsername, adminPassword, adminEmail));

        // Assert
        Assert.AreEqual(adminID, admins[0].ID, "AdminID was not set correctly.");
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
        admins.Add(new Admin());

        admins[0].Username = adminUsername;
        admins[0].Password = adminPassword;
        admins[0].Email = adminEmail;
        admins[0].Role = adminRole;
        admins[0].LoginDate = adminLogInDate;

        // Assert
        Assert.AreEqual(adminUsername, admins[0].Username, "Username was not set correctly.");
        Assert.AreEqual(adminPassword, admins[0].Password, "Password was not set correctly.");
        Assert.AreEqual(adminEmail, admins[0].Email, "Email was not set correctly.");
        Assert.AreEqual(adminRole, admins[0].Role, "Role was not set correctly.");
        Assert.AreEqual(adminLogInDate, admins[0].LoginDate, "LoginDate was not set correctly.");
    }

    // ==========================================
    // Method Tests
    // ==========================================
    [TestMethod]
    public void ManageUser_AddUserShouldAddUser()
    {
        // Arrange
        string adminUsername = "Johned";
        string adminPassword = "Doed";
        string adminEmail = "johndoe05@outlook.co.uk";
        UserRole adminRole = UserRole.Student;
        DateTime adminLogInDate = DateTime.MaxValue;

        // Act
        admins.Add(new Admin());

        admins[0].Username = adminUsername;
        admins[0].Password = adminPassword;
        admins[0].Email = adminEmail;
        admins[0].Role = adminRole;
        admins[0].LoginDate = adminLogInDate;

        // Assert
        Assert.AreEqual(adminUsername, admins[0].Username, "Username was not set correctly.");
        Assert.AreEqual(adminPassword, admins[0].Password, "Password was not set correctly.");
        Assert.AreEqual(adminEmail, admins[0].Email, "Email was not set correctly.");
        Assert.AreEqual(adminRole, admins[0].Role, "Role was not set correctly.");
        Assert.AreEqual(adminLogInDate, admins[0].LoginDate, "LoginDate was not set correctly.");
    }
}
