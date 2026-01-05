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
    private List<Student> students;

    [TestInitialize]
    public void Setup()
    {
        // Required Sample Data
        Admin.ResetNextIDCounter();
        admins = new List<Admin>();

        Student.ResetNextIDCounter();
        students = new List<Student>();
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
    public void AddAdmin_ShouldAddAdminCorrectly()
    {
        // Arrange
        QuizSystemData systemData = new QuizSystemData();
        string adminUsername = "John";
        string adminPassword = "Doe";
        string adminEmail = "johndoe05@outlook.co.uk";

        // Act
        Admin.AddAdmin(systemData.Admins, adminUsername, adminPassword, adminEmail);

        // Assert
        Assert.AreEqual(1, systemData.Admins.Count, "AddAdmin did not increase list size.");
        Assert.AreEqual(adminUsername, systemData.Admins[0].Username, "AddAdmin added incorrect username.");
        Assert.AreEqual(adminPassword, systemData.Admins[0].Password, "AddAdmin added incorrect password.");
        Assert.AreEqual(adminEmail, systemData.Admins[0].Email, "AddAdmin added incorrect email.");
    }

    [TestMethod]
    public void AddStudent_ShouldAddStudentCorrectly()
    {
        // Arrange
        QuizSystemData systemData = new QuizSystemData();
        string studentUsername = "John";
        string studentPassword = "Doe";
        string studentEmail = "johndoe05@outlook.co.uk";

        // Act
        Admin.AddStudent(systemData.Students, studentUsername, studentPassword, studentEmail);

        // Assert
        Assert.AreEqual(1, systemData.Students.Count, "AddStudent did not increase list size.");
        Assert.AreEqual(studentUsername, systemData.Students[0].Username, "AddStudent added incorrect username.");
        Assert.AreEqual(studentPassword, systemData.Students[0].Password, "AddStudent added incorrect password.");
        Assert.AreEqual(studentEmail, systemData.Students[0].Email, "AddStudent added incorrect email.");
    }

    [TestMethod]
    public void UpdateStudent_ShouldUpdateAllFields_WhenValidInput()
    {
        // Arrange
        students.Add(new Student("student", "student123", "student@ulster.ac.uk", "active"));
        string studentUpdatedUsername = "John";
        string studentUpdatedPassword = "Doe";
        string studentUpdatedEmail = "johndoe05@outlook.co.uk";
        string studentUpdatedActivity = "active";
        string expectedMethodOutput = "Student details updated successfully.";

        // Act
        string actualMethodOutput = Admin.UpdateStudent(students, 1, studentUpdatedUsername, studentUpdatedPassword, studentUpdatedEmail, studentUpdatedActivity);

        // Assert
        Assert.AreEqual(studentUpdatedUsername, students[0].Username, "UpdateStudent failed to update username.");
        Assert.AreEqual(studentUpdatedPassword, students[0].Password, "UpdateStudent failed to update password.");
        Assert.AreEqual(studentUpdatedEmail, students[0].Email, "UpdateStudent failed to update email.");
        Assert.AreEqual(studentUpdatedActivity, students[0].Status, "UpdateStudent failed to update status.");
        Assert.AreEqual(expectedMethodOutput, actualMethodOutput, "UpdateStudent does not output the expected string.");
    }

    [TestMethod]
    public void UpdateStudent_ShouldIgnoreEmptyFields()
    {
        // Arrange
        string studentUsername = "student";
        string studentPassword = "student";
        string studentEmail = "student@ulster.ac.uk";
        string studentActivity = "active";
        string expectedMethodOutput = "Student details updated successfully.";
        students.Add(new Student(studentUsername, studentPassword, studentEmail, studentActivity));

        // Act
        string actualMethodOutput = Admin.UpdateStudent(students, 1, "", null, "", null);

        // Assert
        Assert.AreEqual(studentUsername, students[0].Username, "UpdateStudent failed to update username.");
        Assert.AreEqual(studentPassword, students[0].Password, "UpdateStudent failed to update password.");
        Assert.AreEqual(studentEmail, students[0].Email, "UpdateStudent failed to update email.");
        Assert.AreEqual(studentActivity, students[0].Status, "UpdateStudent failed to update status.");
        Assert.AreEqual(expectedMethodOutput, actualMethodOutput, "UpdateStudent does not output the expected string.");
    }

    [TestMethod]
    public void UpdateStudent_ShouldReturnNotFound_WhenIdDoesNotExist()
    {
        // Arrange
        string expectedMethodOutput = "Student not found.";

        // Act
        string actualMethodOutput = Admin.UpdateStudent(students, 99, "incorrect", "incorrect", "incorrect@incorrect.com", "active");

        // Assert
        Assert.AreEqual(expectedMethodOutput, actualMethodOutput, "UpdateStudent does not output the expected string.");
    }

    [TestMethod]
    public void RemoveStudent_ShouldRemoveStudent_WhenIDExists()
    {
        // Arrange
        students.AddRange(new[]
        {
            new Student("student", "student123", "student@ulster.ac.uk", "active"),
            new Student("studented", "student123456", "studented@ulster.ac.uk", "active")
        });
        string expectedMethodOutput = "Student removed.";

        // Act
        string actualMethodOutput = Admin.RemoveStudent(students, 1);

        // Assert
        Assert.AreEqual(1, students.Count, "RemoveStudent failed to remove a student when ID exists.");
        Assert.AreEqual(expectedMethodOutput, actualMethodOutput, "RemoveStudent does not output the expected string.");
    }

    [TestMethod]
    public void RemoveStudent_ShouldReturnMessage_WhenIDDoesNotExist()
    {
        // Arrange
        students.Add(new Student("student", "student123", "student@ulster.ac.uk", "active"));
        string expectedMethodOutput = "ID not found.";

        // Act
        string actualMethodOutput = Admin.RemoveStudent(students, 99);

        // Assert
        Assert.AreEqual(1, students.Count, "RemoveStudent removed a student when ID doesn't exist.");
        Assert.AreEqual(expectedMethodOutput, actualMethodOutput, "RemoveStudent does not output the expected string.");
    }
}
