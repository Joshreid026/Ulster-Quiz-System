using System.Collections.Generic;
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
    private List<Category> categories;
    private List<Quiz> quizzes;

    [TestInitialize]
    public void Setup()
    {
        // Required Sample Data
        Admin.ResetAdminNextIDCounter();
        admins = new List<Admin>();

        Student.ResetStudentNextIDCounter();
        students = new List<Student>();

        Category.ResetCategoryNextIDCounter();
        categories = new List<Category>();

        Quiz.ResetQuizNextIDCounter();
        quizzes = new List<Quiz>();
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
        Assert.AreEqual(adminID, admins[0].AdminID, "ID was not initialized correctly.");
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
        Assert.AreEqual(adminID, admins[0].AdminID, "ID was not initialized correctly.");
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
        Assert.AreEqual(adminID, admins[0].AdminID, "AdminID was not set correctly.");
    }

    [TestMethod]
    public void AdminID_Setter_ShouldBeProtected()
    {
        // Arrange
        PropertyInfo property = typeof(Admin).GetProperty("AdminID");

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
    public void UpdateStudent_ShouldReturnNotFound_WhenIDDoesNotExist()
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

    [TestMethod]
    public void AddQuestionToQuiz_ShouldAddQuestionsCorrectly()
    {
        // Arrange
        categories.Add(new Category("Programming", "Concepts of object-oriented programming and coding principles"));
        quizzes.Add(new Quiz("OOP Fundamentals", "Covers basics of object-oriented programming.", categories[0], new DateTime(2025, 09, 01)));
        List<string> options = new List<string> { "Object-Oriented Programming", "Operational Output Processing", "Open Order Protocol", "Overloaded Operator Procedure" };

        // Act
        Admin.AddQuestionToQuiz(quizzes[0], "What does OOP stand for?", options, "A", "Easy");

        // Assert
        Assert.AreEqual(1, quizzes[0].QuizQuestions.Count, "AddQuestionToQuiz did not increase list size.");
        Assert.AreEqual("What does OOP stand for?", quizzes[0].QuizQuestions[0].QuestionText, "AddQuestionToQuiz did not add correct question(s).");
    }

    [TestMethod]
    public void AddCategory_ShouldAddCategoryCorrectly()
    {
        // Arrange
        QuizSystemData systemData = new QuizSystemData();
        string categoryName = "Default";
        string categoryDescription = "Default Description";

        // Act
        Admin.AddCategory(systemData.Categories, categoryName, categoryDescription);

        // Assert
        Assert.AreEqual(1, systemData.Categories.Count, "AddAdmin did not increase list size.");
        Assert.AreEqual(categoryName, systemData.Categories[0].CategoryName, "AddAdmin added incorrect category name.");
        Assert.AreEqual(categoryDescription, systemData.Categories[0].CategoryDescription, "AddAdmin added incorrect category description.");
    }

    [TestMethod]
    public void RemoveCategory_ShouldRemoveCategory_WhenIDExists()
    {
        // Arrange
        categories.AddRange(new[]
        {
            new Category("Programming", "Concepts of object-oriented programming and coding principles"),
            new Category("Data Structures", "Arrays, lists, stacks, queues, trees, and their applications"),
        });
        string expectedMethodOutput = "Deleted.";

        // Act
        string actualMethodOutput = Admin.RemoveCategory(categories, 1);

        // Assert
        Assert.AreEqual(1, categories.Count, "RemoveCategory failed to remove a category when ID exists.");
        Assert.AreEqual(expectedMethodOutput, actualMethodOutput, "RemoveCategory does not output the expected string.");
    }

    [TestMethod]
    public void RemoveCategory_ShouldReturnMessage_WhenIDDoesNotExist()
    {
        // Arrange
        categories.Add(new Category("Programming", "Concepts of object-oriented programming and coding principles"));
        string expectedMethodOutput = "ID not found.";

        // Act
        string actualMethodOutput = Admin.RemoveCategory(categories, 99);

        // Assert
        Assert.AreEqual(1, categories.Count, "RemoveCategory removed a category when ID doesn't exist.");
        Assert.AreEqual(expectedMethodOutput, actualMethodOutput, "RemoveCategory does not output the expected string.");
    }

    [TestMethod]
    public void ResetAdminNextIDCounter_ShouldResetAdminIdToOne()
    {
        // Arrange
        admins.AddRange(new[]
        {
            new Admin("admin", "admin123", "admin@ulster.ac.uk"),
            new Admin("admined", "admin123456", "admined@ulster.ac.uk")
        });

        // Act
        Admin.ResetAdminNextIDCounter();
        admins.Add(new Admin("admineded", "admin123456789", "admineded@ulster.ac.uk"));

        // Assert
        Assert.AreEqual(1, admins[2].AdminID, "ResetAdminNextIDCounter did not reset AdminID.");
    }
}
