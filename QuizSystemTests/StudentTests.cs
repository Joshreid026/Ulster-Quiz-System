using System.Reflection;
using UlsterQuizSystem;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace QuizSystemTests;

[TestClass]
public class StudentTests
{
    // ==========================================
    // Test Initialization + Fields
    // ==========================================
    private List<Student> _students;

    [TestInitialize]
    public void Setup()
    {
        //Required Sample Data
        _students = new List<Student>();
        _students.Add(new Student());
    }

    // ==========================================
    // Constructor Tests
    // ==========================================
    [TestMethod]
    public void StudentDefaultConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        int studentID = 101;
        string studentUsername = "default";
        string studentPassword = "password";
        string studentEmail = "email@mail.com";
        UserRole studentRole = UserRole.Student;
        string studentStatus = "active";

        // Assert
        Assert.AreEqual(studentID, _students[0].ID, "ID was not initialized correctly.");
        Assert.AreEqual(studentUsername, _students[0].Username, "Username was not initialized correctly.");
        Assert.AreEqual(studentPassword, _students[0].Password, "Password was not initialized correctly.");
        Assert.AreEqual(studentEmail, _students[0].Email, "Email was not initialized correctly.");
        Assert.AreEqual(studentRole, _students[0].Role, "Role was not initialized correctly.");
        Assert.AreEqual(studentStatus, _students[0].Status, "Status was not initialized correctly.");
    }

    [TestMethod]
    public void StudentParameterisedConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        int studentID = 102;
        string studentUsername = "John";
        string studentPassword = "Doe";
        string studentEmail = "johndoe05@outlook.com";
        UserRole studentRole = UserRole.Student;
        string studentStatus = "active";

        // Act
        _students.Add(new Student(studentID, studentUsername, studentPassword, studentEmail, "active"));

        // Assert
        Assert.AreEqual(studentID, _students[1].ID, "ID was not initialized correctly.");
        Assert.AreEqual(studentUsername, _students[1].Username, "Username was not initialized correctly.");
        Assert.AreEqual(studentPassword, _students[1].Password, "Password was not initialized correctly.");
        Assert.AreEqual(studentEmail, _students[1].Email, "Email was not initialized correctly.");
        Assert.AreEqual(studentRole, _students[1].Role, "Role was not initialized correctly.");
        Assert.AreEqual(studentStatus, _students[1].Status, "Status was not initialized correctly.");
    }

    // ==========================================
    // Get/Set Tests
    // ==========================================
    [TestMethod]
    public void StudentID_ShouldSetCorrectly()
    {
        // Arrange
        int studentID = 1;
        string studentUsername = "John";
        string studentPassword = "Doe";
        string studentEmail = "johndoe05@outlook.com";
        string studentStatus = "active";

        // Act
        _students.Add(new Student(studentID, studentUsername, studentPassword, studentEmail, studentStatus));

        // Assert
        Assert.AreEqual(studentID, _students[1].ID, "StudentID was not set correctly.");
    }

    [TestMethod]
    public void StudentID_Setter_ShouldBeProtected()
    {
        // Arrange
        PropertyInfo property = typeof(Student).GetProperty("ID");

        // Act
        MethodInfo setter = property.SetMethod;

        // Assert
        Assert.IsTrue(setter.IsFamily, "StudentID setter should be protected.");
    }

    [TestMethod]
    public void StudentProperties_ShouldSetAndGetValues()
    {
        // Arrange
        string studentUsername = "Johned";
        string studentPassword = "Doed";
        string studentEmail = "johndoe05@outlook.co.uk";
        UserRole studentRole = UserRole.Admin;
        string studentStatus = "inactive";

        // Act
        _students[0].Username = studentUsername;
        _students[0].Password = studentPassword;
        _students[0].Email = studentEmail;
        _students[0].Role = studentRole;
        _students[0].Status = studentStatus;

        // Assert
        Assert.AreEqual(studentUsername, _students[0].Username, "Username was not set correctly.");
        Assert.AreEqual(studentPassword, _students[0].Password, "Password was not set correctly.");
        Assert.AreEqual(studentEmail, _students[0].Email, "Email was not set correctly.");
        Assert.AreEqual(studentRole, _students[0].Role, "Role was not set correctly.");
        Assert.AreEqual(studentStatus, _students[0].Status, "Status was not set correctly.");
    }

    // ==========================================
    // Method Tests
    // ==========================================
}
