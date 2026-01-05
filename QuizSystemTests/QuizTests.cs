using System.Reflection;
using UlsterQuizSystem;


namespace QuizSystemTests;

[TestClass]
public class QuizTests
{
    // ==========================================
    // Test Initialization + Fields
    // ==========================================
    private List<Category> categories;
    private List<Quiz> quizzes;

    [TestInitialize]
    public void Setup()
    {
        // Required Sample Data
        Quiz.ResetQuizNextIDCounter();

        categories = new List<Category>();
        quizzes = new List<Quiz>();

        categories.Add(new Category());
    }

    // ==========================================
    // Constructor Tests
    // ==========================================
    [TestMethod]
    public void QuizDefaultConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        int quizID = 1;
        string quizTitle = "";
        string quizDescription = "";
        Category quizCategory = categories[0];
        List<Question> quizQuestions = new List<Question>();
        DateTime quizDate = DateTime.MinValue;

        // Act
        quizzes.Add(new Quiz());

        // Assert (Value Types)
        Assert.AreEqual(quizID, quizzes[0].QuizID, "QuizID was not initialized correctly.");
        Assert.AreEqual(quizTitle, quizzes[0].QuizTitle, "QuizTitle was not initialized correctly.");
        Assert.AreEqual(quizDescription, quizzes[0].QuizDescription, "QuizDescription was not initialized correctly.");
        Assert.AreEqual(quizDate, quizzes[0].QuizDate, "QuizDate was not initialized correctly.");

        // Assert (Reference Types)
        Assert.IsNotNull(quizzes[0].QuizCategory, "QuizCategory should not be null.");
        Assert.IsInstanceOfType(quizzes[0].QuizCategory, typeof(Category), "QuizCategory is not of the right instance type.");

        Assert.IsNotNull(quizzes[0].QuizQuestions, "QuizQuestions should not be null.");
        Assert.AreEqual(0, quizzes[0].QuizQuestions.Count(), "QuizQuestions should start empty.");
    }

    [TestMethod]
    public void QuizParameterisedConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        int quizID = 1;
        string quizTitle = "title";
        string quizDescription = "description";
        Category quizCategory = categories[0];
        List<Question> quizQuestions = new List<Question>();
        DateTime quizDate = new DateTime(2026, 1, 1);

        // Act
        quizzes.Add(new Quiz(quizTitle, quizDescription, categories[0], new DateTime(2026, 1, 1)));

        // Assert (Value Types)
        Assert.AreEqual(quizID, quizzes[0].QuizID, "QuizID was not initialized correctly.");
        Assert.AreEqual(quizTitle, quizzes[0].QuizTitle, "QuizTitle was not initialized correctly.");
        Assert.AreEqual(quizDescription, quizzes[0].QuizDescription, "QuizDescription was not initialized correctly.");
        Assert.AreSame(quizCategory, quizzes[0].QuizCategory, "QuizCategory was not initialized correctly.");
        Assert.AreEqual(quizDate, quizzes[0].QuizDate, "QuizDate was not initialized correctly.");

        // Assert (Reference Types)
        Assert.IsNotNull(quizzes[0].QuizQuestions, "QuizQuestions should not be null.");
        Assert.IsInstanceOfType(quizzes[0].QuizQuestions, typeof(List<Question>), "QuizQuestions is not of the right instance type.");
        Assert.AreEqual(0, quizzes[0].QuizQuestions.Count(), "QuizQuestions should start empty.");
    }

    // ==========================================
    // Get/Set Tests
    // ==========================================
    [TestMethod]
    public void QuizID_ShouldSetCorrectly()
    {
        // Arrange
        int quizID = 1;
        string quizTitle = "title";
        string quizDescription = "description";
        Category quizCategory = categories[0];
        DateTime quizDate = new DateTime(2026, 1, 1);

        // Act
        quizzes.Add(new Quiz(quizTitle, quizDescription, quizCategory, quizDate));

        // Assert
        Assert.AreEqual(quizID, quizzes[0].QuizID, "QuizID was not set correctly.");
    }

    [TestMethod]
    public void QuizID_Setter_ShouldBeProtected()
    {
        // Arrange
        PropertyInfo property = typeof(Quiz).GetProperty("QuizID");

        // Act
        MethodInfo setter = property.SetMethod;

        // Assert
        Assert.IsTrue(setter.IsPrivate, "QuizID setter should be private.");
    }

    [TestMethod]
    public void QuizProperties_ShouldSetAndGetValues()
    {
        // Arrange
        string quizTitle = "title";
        string quizDescription = "description";
        Category quizCategory = categories[0];
        List<Question> quizQuestions = new List<Question>();
        DateTime quizDate = new DateTime(2026, 1, 1);

        // Act
        quizzes.Add(new Quiz());

        quizzes[0].QuizTitle = quizTitle;
        quizzes[0].QuizDescription = quizDescription;
        quizzes[0].QuizCategory = quizCategory;
        quizzes[0].QuizQuestions = quizQuestions;
        quizzes[0].QuizDate = quizDate;

        // Assert
        Assert.AreEqual(quizTitle, quizzes[0].QuizTitle, "QuizTitle was not initialized correctly.");
        Assert.AreEqual(quizDescription, quizzes[0].QuizDescription, "QuizDescription was not initialized correctly.");
        Assert.AreSame(quizCategory, quizzes[0].QuizCategory, "QuizCategory was not initialized correctly.");
        Assert.AreSame(quizQuestions, quizzes[0].QuizQuestions, "QuizQuestions was not initialized correctly.");
        Assert.AreEqual(quizDate, quizzes[0].QuizDate, "QuizDate was not initialized correctly.");
    }

    // ==========================================
    // Method Tests
    // ==========================================
}
