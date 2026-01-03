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
        string studentStatus = "inactive";

        // Act
        Student student = new Student(studentID, studentUsername, studentPassword, studentEmail, studentStatus);

        // Assert
        Assert.AreEqual(studentID, student.ID);
        Assert.AreEqual(studentUsername, student.Username);
        Assert.AreEqual(studentPassword, student.Password);
        Assert.AreEqual(studentEmail, student.Email);
        Assert.AreEqual(studentStatus, student.Status);
    }

    [TestMethod]
    public void StudentParameterisedConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        int studentID = 101;
        string studentUsername = "John";
        string studentPassword = "Doe";
        string studentEmail = "johndoe05@outlook.com";
        string studentStatus = "inactive";

        // Act
        Student student = new Student(studentID, studentUsername, studentPassword, studentEmail, studentStatus);

        // Assert
        Assert.AreEqual(studentID, student.ID);
        Assert.AreEqual(studentUsername, student.Username);
        Assert.AreEqual(studentPassword, student.Password);
        Assert.AreEqual(studentEmail, student.Email);
        Assert.AreEqual(studentStatus, student.Status);
    }
}
