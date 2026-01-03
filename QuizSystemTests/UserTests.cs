using System.IO;
using UlsterQuizSystem;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace QuizSystemTests;

[TestClass]
public class UserTests
{
    // ==========================================
    // Test Initialization + Fields
    // ==========================================
    private List<TestUser> _users;

    [TestInitialize]
    public void Setup()
    {
        //Required Sample Data
        _users = new List<TestUser>();
        _users.Add(new TestUser());
        _users.Add(new TestUser(1, "John", "Doe", "johndoe05@outlook.com"));
    }

    // ==========================================
    // Constructor Tests
    // ==========================================
    [TestMethod]
    public void UserDefaultConstructor_SetsExpectedDefaults()
    {
        // Arrange
        int userID = 0;
        string userUsername = "default";
        string userPassword = "password";
        string userEmail = "email@mail.com";
        UserRole userRole = UserRole.User;

        // Assert
        Assert.AreEqual(userID, _users[0].ID, "ID was not initialized correctly.");
        Assert.AreEqual(userUsername, _users[0].Username, "Username was not initialized correctly.");
        Assert.AreEqual(userPassword, _users[0].Password, "Password was not initialized correctly.");
        Assert.AreEqual(userEmail, _users[0].Email, "Email was not initialized correctly.");
        Assert.AreEqual(userRole, _users[0].Role, "Role was not initialized correctly.");
    }

    [TestMethod]
    public void UserParameterizedConstructor_SetsProvidedValues()
    {
        // Arrange
        int userID = 1;
        string userUsername = "John";
        string userPassword = "Doe";
        string userEmail = "johndoe05@outlook.com";
        UserRole userRole = UserRole.User;

        // Act
        TestUser user = new TestUser(userID, userUsername, userPassword, userEmail);

        // Assert
        Assert.AreEqual(userID, _users[1].ID, "ID was not initialized correctly.");
        Assert.AreEqual(userUsername, _users[1].Username, "Username was not initialized correctly.");
        Assert.AreEqual(userPassword, _users[1].Password, "Password was not initialized correctly.");
        Assert.AreEqual(userEmail, _users[1].Email, "Email was not initialized correctly.");
        Assert.AreEqual(userRole, _users[1].Role, "Role was not initialized correctly.");
    }

    // ==========================================
    // Get/Set Tests
    // ==========================================


    // ==========================================
    // Method Tests
    // ==========================================
    [TestMethod()]
    public void LogoutOutputsCorrectMessage()
    {
        // Arrange
        string expectedOutput = "\nUser default logged out.";

        // Redirects Console Output to a StringWriter
        using (var stringWriter = new StringWriter())
        {
            Console.SetOut(stringWriter);

            // Act
            _users[0].Logout();

            // Assert
            string actualOutput = stringWriter.ToString().Trim();
            Assert.AreEqual(expectedOutput.Trim(), actualOutput, "Expected output did not match actual output");
        }
    }

    [TestMethod()]
    public void ToStringReturnCorrectMessage()
    {
        // Arrange
        string expectedReturnValue = "ID: 0 | User: default | Role: User";

        // Act
        string actualReturnValue = _users[0].ToString();

        // Assert
        Assert.AreEqual(expectedReturnValue, actualReturnValue, "Expected return value did not match actual return value");
    }
}
