using UlsterQuizSystem;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace QuizSystemTests;

[TestClass]
public class UserTests
{
    [TestMethod]
    public void DefaultConstructor_SetsExpectedDefaults()
    {
        // Arrange
        int userID = 0;
        string userUsername = "default";
        string userPassword = "password";
        string userEmail = "email@mail.com";
        string userRole = "User";

        // Act
        TestUser user = new TestUser();

        // Assert
        Assert.AreEqual(userID, user.ID, "ID was not initialized correctly.");
        Assert.AreEqual(userUsername, user.Username, "Username was not initialized correctly.");
        Assert.AreEqual(userPassword, user.Password, "Password was not initialized correctly.");
        Assert.AreEqual(userEmail, user.Email, "Email was not initialized correctly.");
        Assert.AreEqual(userRole, user.Role, "Role was not initialized correctly.");
    }

    [TestMethod]
    public void ParameterizedConstructor_SetsProvidedValues()
    {
        // Arrange
        int userID = 0;
        string userUsername = "John";
        string userPassword = "Doe";
        string userEmail = "johndoe05@outlook.com";
        string userRole = "User";

        // Act
        TestUser user = new TestUser(userID, userUsername, userPassword, userEmail, userRole);

        // Assert
        Assert.AreEqual(userID, user.ID, "ID was not initialized correctly.");
        Assert.AreEqual(userUsername, user.Username, "Username was not initialized correctly.");
        Assert.AreEqual(userPassword, user.Password, "Password was not initialized correctly.");
        Assert.AreEqual(userEmail, user.Email, "Email was not initialized correctly.");
        Assert.AreEqual(userRole, user.Role, "Role was not initialized correctly.");
    }
}
