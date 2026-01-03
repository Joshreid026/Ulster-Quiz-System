using UlsterQuizSystem;

namespace QuizSystemTests;

[TestClass]
public class StudentTests
{
    [TestMethod]
    public void StudentDefaultConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        int studentID = 101;
        string studentUsername = "default";
        string studentPassword = "password";
        string studentEmail = "email@mail.com";
        string studentRole = "Student";
        string studentStatus = "inactive";

        // Act
        Student student = new Student();

        // Assert
        Assert.AreEqual(studentID, student.ID, "ID was not initialized correctly.");
        Assert.AreEqual(studentUsername, student.Username, "Username was not initialized correctly.");
        Assert.AreEqual(studentPassword, student.Password, "Password was not initialized correctly.");
        Assert.AreEqual(studentEmail, student.Email, "Email was not initialized correctly.");
        Assert.AreEqual(studentRole, student.Role, "Role was not initialized correctly.");
        Assert.AreEqual(studentStatus, student.Status, "Status was not initialized correctly.");
    }

    [TestMethod]
    public void StudentParameterisedConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        int studentID = 101;
        string studentUsername = "John";
        string studentPassword = "Doe";
        string studentEmail = "johndoe05@outlook.com";
        string studentRole = "Student";
        string studentStatus = "inactive";

        // Act
        Student student = new Student(studentID, studentUsername, studentPassword, studentEmail, studentStatus);

        // Assert
        Assert.AreEqual(studentID, student.ID, "ID was not initialized correctly.");
        Assert.AreEqual(studentUsername, student.Username, "Username was not initialized correctly.");
        Assert.AreEqual(studentPassword, student.Password, "Password was not initialized correctly.");
        Assert.AreEqual(studentEmail, student.Email, "Email was not initialized correctly.");
        Assert.AreEqual(studentRole, student.Role, "Role was not initialized correctly.");
        Assert.AreEqual(studentStatus, student.Status, "Status was not initialized correctly.");
    }
}
