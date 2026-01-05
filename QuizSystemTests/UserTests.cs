using System.IO;
using System.Reflection;
using UlsterQuizSystem;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace QuizSystemTests;

[TestClass]
public class UserTests
{
    // ==========================================
    // Test Initialization + Fields
    // ==========================================
    private List<TestUser> users;

    [TestInitialize]
    public void Setup()
    {
        // Required Sample Data
        users = new List<TestUser>();
    }

    // ==========================================
    // Constructor Tests
    // ==========================================
    [TestMethod]
    public void UserDefaultConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        string userUsername = "default";
        string userPassword = "password";
        string userEmail = "email@mail.com";
        UserRole userRole = UserRole.User;

        // Act
        users.Add(new TestUser());

        // Assert
        Assert.AreEqual(userUsername, users[0].Username, "Username was not initialized correctly.");
        Assert.AreEqual(userPassword, users[0].Password, "Password was not initialized correctly.");
        Assert.AreEqual(userEmail, users[0].Email, "Email was not initialized correctly.");
        Assert.AreEqual(userRole, users[0].Role, "Role was not initialized correctly.");
    }

    [TestMethod]
    public void UserParameterizedConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        string userUsername = "John";
        string userPassword = "Doe";
        string userEmail = "johndoe05@outlook.com";
        UserRole userRole = UserRole.User;

        // Act
        users.Add(new TestUser(userUsername, userPassword, userEmail));

        // Assert
        Assert.AreEqual(userUsername, users[0].Username, "Username was not initialized correctly.");
        Assert.AreEqual(userPassword, users[0].Password, "Password was not initialized correctly.");
        Assert.AreEqual(userEmail, users[0].Email, "Email was not initialized correctly.");
        Assert.AreEqual(userRole, users[0].Role, "Role was not initialized correctly.");
    }

    // ==========================================
    // Get/Set Tests
    // ==========================================
    [TestMethod]
    public void UserProperties_ShouldSetAndGetValues()
    {
        // Arrange
        string userUsername = "John";
        string userPassword = "Doe";
        string userEmail = "johndoe05@outlook.com";
        UserRole userRole = UserRole.User;

        // Act
        users.Add(new TestUser());

        users[0].Username = userUsername;
        users[0].Password = userPassword;
        users[0].Email = userEmail;
        users[0].Role = userRole;

        // Assert
        Assert.AreEqual(userUsername, users[0].Username, "Username was not set correctly.");
        Assert.AreEqual(userPassword, users[0].Password, "Password was not set correctly.");
        Assert.AreEqual(userEmail, users[0].Email, "Email was not set correctly.");
        Assert.AreEqual(userRole, users[0].Role, "Role was not set correctly.");
    }

    // ==========================================
    // Method Tests
    // ==========================================
    [TestMethod()]
    public void LogoutOutputsCorrectMessage()
    {
        // Arrange
        string expectedOutput = "\nUser default logged out.";
        users.Add(new TestUser());

        // Redirects Console Output to a StringWriter
        using (var stringWriter = new StringWriter())
        {
            Console.SetOut(stringWriter);

            // Act
            users[0].Logout();

            // Assert
            string actualOutput = stringWriter.ToString().Trim();
            Assert.AreEqual(expectedOutput.Trim(), actualOutput, "Expected output did not match actual output.");
        }
    }

    [TestMethod()]
    public void ToStringReturnCorrectMessage()
    {
        // Arrange
        string expectedReturnValue = "User: default | Role: User";
        users.Add(new TestUser());

        // Act
        string actualReturnValue = users[0].ToString();

        // Assert
        Assert.AreEqual(expectedReturnValue, actualReturnValue, "Expected return value did not match actual return value.");
    }
}
