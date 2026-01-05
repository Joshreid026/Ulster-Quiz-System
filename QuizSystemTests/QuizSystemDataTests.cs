using System.Reflection;
using UlsterQuizSystem;

namespace QuizSystemTests;

[TestClass]
public class QuizSystemDataTests
{
    // ==========================================
    // Test Initialization + Fields
    // ==========================================
    private List<QuizSystemData> systemData;
    private List<Admin> admins;
    private List<Student> students;
    private List<Quiz> quizzes;
    private List<Category> categories;

    [TestInitialize]
    public void Setup()
    {
        //Required Sample Data
        systemData = new List<QuizSystemData>();
        systemData.Add(new QuizSystemData());

        admins = new List<Admin>();
        admins.Add(new Admin());

        students = new List<Student>();
        students.Add(new Student());

        quizzes = new List<Quiz>();
        quizzes.Add(new Quiz());

        categories = new List<Category>();
        categories.Add(new Category());
    }

    // ==========================================
    // Constructor Tests
    // ==========================================
    [TestMethod]
    public void QuizSystemDataDefaultConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        List<Admin> quizSystemDataAdmins = new List<Admin>();
        List<Student> quizSystemDataStudents = new List<Student>();
        List<Quiz> quizSystemDataQuizzes = new List<Quiz>();
        List<Category> quizSystemDataCategories = new List<Category>();

        // Assert
        Assert.IsNotNull(systemData[0].Admins, "Admins should not be null.");
        Assert.IsNotNull(systemData[0].Students, "Students should not be null.");
        Assert.IsNotNull(systemData[0].Quizzes, "Quizzes should not be null.");
        Assert.IsNotNull(systemData[0].Categories, "Categories should not be null.");

        Assert.IsInstanceOfType(systemData[0].Admins, typeof(List<Admin>), "Admins is not of the right instance type.");
        Assert.IsInstanceOfType(systemData[0].Students, typeof(List<Student>), "Students is not of the right instance type.");
        Assert.IsInstanceOfType(systemData[0].Quizzes, typeof(List<Quiz>), "Quizzes is not of the right instance type.");
        Assert.IsInstanceOfType(systemData[0].Categories, typeof(List<Category>), "Categories is not of the right instance type.");

        Assert.AreEqual(0, systemData[0].Admins.Count, "Admins should start empty.");
        Assert.AreEqual(0, systemData[0].Students.Count, "Students should start empty.");
        Assert.AreEqual(0, systemData[0].Quizzes.Count, "Quizzes should start empty.");
        Assert.AreEqual(0, systemData[0].Categories.Count, "Categories should start empty.");
    }

    [TestMethod]
    public void QuizSystemDataParameterisedConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        admins.Add(new Admin("admin", "admin123", "admin@ulster.ac.uk"));
        students.Add(new Student("student", "student123", "student@ulster.ac.uk", "active"));
        categories.Add(new Category("Programming", "Concepts of object-oriented programming and coding principles"));
        quizzes.Add(new Quiz("OOP Fundamentals", "Covers basics of object-oriented programming.", categories[0], new DateTime(2025, 09, 01)));

        // Act
        systemData.Add(new QuizSystemData(admins, students, quizzes, categories));

        // Assert
        Assert.AreEqual(admins, systemData[1].Admins, "Admins was not initialized correctly.");
        Assert.AreEqual(students, systemData[1].Students, "Students was not initialized correctly.");
        Assert.AreEqual(quizzes, systemData[1].Quizzes, "Quizzes was not initialized correctly.");
        Assert.AreEqual(categories, systemData[1].Categories, "Categories was not initialized correctly.");
    }

    // ==========================================
    // Get/Set Tests
    // ==========================================
    [TestMethod]
    public void QuizSystemDataProperties_ShouldSetCorrectly()
    {
        // Arrange
        admins.Add(new Admin("admin", "admin123", "admin@ulster.ac.uk"));
        students.Add(new Student("student", "student123", "student@ulster.ac.uk", "active"));
        quizzes.Add(new Quiz("OOP Fundamentals", "Covers basics of object-oriented programming.", categories[0], new DateTime(2025, 09, 01)));
        categories.Add(new Category("Programming", "Concepts of object-oriented programming and coding principles"));

        // Act
        systemData.Add(new QuizSystemData(admins, students, quizzes, categories));

        // Assert
        Assert.AreEqual(admins, systemData[1].Admins, "Admins was not set correctly.");
        Assert.AreEqual(students, systemData[1].Students, "Students was not set correctly.");
        Assert.AreEqual(quizzes, systemData[1].Quizzes, "Quizzes was not set correctly.");
        Assert.AreEqual(categories, systemData[1].Categories, "Categories was not set correctly.");
    }

    [TestMethod]
    public void QuizSystemDataProperties_Setter_ShouldBePrivate()
    {
        // Arrange
        PropertyInfo adminIDProperty = typeof(QuizSystemData).GetProperty("Admins");
        PropertyInfo studentIDproperty = typeof(QuizSystemData).GetProperty("Students");
        PropertyInfo quizIDproperty = typeof(QuizSystemData).GetProperty("Quizzes");
        PropertyInfo categoryIDproperty = typeof(QuizSystemData).GetProperty("Categories");


        // Act
        MethodInfo adminIDSetter = adminIDProperty.SetMethod;
        MethodInfo studentIDSetter = studentIDproperty.SetMethod;
        MethodInfo quizIDSetter = quizIDproperty.SetMethod;
        MethodInfo categoryIDSetter = categoryIDproperty.SetMethod;

        // Assert
        Assert.IsTrue(adminIDSetter.IsPrivate, "Admins setter should be private.");
        Assert.IsTrue(studentIDSetter.IsPrivate, "Students setter should be private.");
        Assert.IsTrue(quizIDSetter.IsPrivate, "Quizzes setter should be private.");
        Assert.IsTrue(categoryIDSetter.IsPrivate, "Categories setter should be private.");
    }

    // ==========================================
    // Method Tests
    // ==========================================
}
