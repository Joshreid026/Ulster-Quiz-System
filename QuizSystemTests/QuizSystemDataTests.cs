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

        Admin.ResetAdminNextIDCounter();
        Student.ResetStudentNextIDCounter();
        Quiz.ResetQuizNextIDCounter();
        Category.ResetCategoryNextIDCounter();

        admins = new List<Admin>();
        students = new List<Student>();
        quizzes = new List<Quiz>();
        categories = new List<Category>();
    }

    // ==========================================
    // Constructor Tests
    // ==========================================
    [TestMethod]
    public void QuizSystemDataDefaultConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        systemData.Add(new QuizSystemData());

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
        Assert.AreEqual(admins, systemData[0].Admins, "Admins was not initialized correctly.");
        Assert.AreEqual(students, systemData[0].Students, "Students was not initialized correctly.");
        Assert.AreEqual(quizzes, systemData[0].Quizzes, "Quizzes was not initialized correctly.");
        Assert.AreEqual(categories, systemData[0].Categories, "Categories was not initialized correctly.");
    }

    // ==========================================
    // Get/Set Tests
    // ==========================================
    [TestMethod]
    public void QuizSystemDataProperties_ShouldSetCorrectly()
    {
        // Arrange
        admins.Add(new Admin());
        students.Add(new Student());
        quizzes.Add(new Quiz());
        categories.Add(new Category());

        admins.Add(new Admin("admin", "admin123", "admin@ulster.ac.uk"));
        students.Add(new Student("student", "student123", "student@ulster.ac.uk", "active"));
        quizzes.Add(new Quiz("OOP Fundamentals", "Covers basics of object-oriented programming.", categories[0], new DateTime(2025, 09, 01)));
        categories.Add(new Category("Programming", "Concepts of object-oriented programming and coding principles"));

        // Act
        systemData.Add(new QuizSystemData(admins, students, quizzes, categories));

        // Assert
        Assert.AreEqual(admins, systemData[0].Admins, "Admins was not set correctly.");
        Assert.AreEqual(students, systemData[0].Students, "Students was not set correctly.");
        Assert.AreEqual(quizzes, systemData[0].Quizzes, "Quizzes was not set correctly.");
        Assert.AreEqual(categories, systemData[0].Categories, "Categories was not set correctly.");
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
    [TestMethod]
    public void LoadSampleData_ShouldAddDataToAllLists()
    {
        // Arrange
        systemData.Add(new QuizSystemData());

        // Act
        systemData[0].LoadSampleData();

        // Assert
        Assert.IsTrue(systemData[0].Categories.Any(), "LoadSampleData did not add anything to systemData.Categories.");
        Assert.IsTrue(systemData[0].Admins.Any(), "LoadSampleData did not add anything to systemData.Admins.");
        Assert.IsTrue(systemData[0].Students.Any(), "LoadSampleData did not add anything to systemData.Students.");
        Assert.IsTrue(systemData[0].Quizzes.Any(), "LoadSampleData did not add anything to systemData.Quizzes.");
    }

    [TestMethod]
    public void LoadSampleData_ShouldAddExpectedNumberOfItems()
    {
        // Arrange
        systemData.Add(new QuizSystemData());

        // Act
        systemData[0].LoadSampleData();

        // Assert
        Assert.AreEqual(7, systemData[0].Categories.Count, "LoadSampleData added more items to systemData.Categories than it should've.");
        Assert.AreEqual(2, systemData[0].Admins.Count, "LoadSampleData added more items to systemData.Categories than it should've.");
        Assert.AreEqual(2, systemData[0].Students.Count, "LoadSampleData added more items to systemData.Categories than it should've.");
        Assert.AreEqual(7, systemData[0].Quizzes.Count, "LoadSampleData added more items to systemData.Categories than it should've.");
    }

    [TestMethod]
    public void LoadSampleData_ShouldAssignQuestionsToFirstQuiz()
    {
        // Arrange
        systemData.Add(new QuizSystemData());

        // Act
        systemData[0].LoadSampleData();

        // Assert
        Assert.IsNotNull(systemData[0].Quizzes[0].QuizQuestions);
        Assert.AreEqual(10, systemData[0].Quizzes[0].QuizQuestions.Count);
    }
}
