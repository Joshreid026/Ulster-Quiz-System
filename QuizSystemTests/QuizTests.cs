using UlsterQuizSystem;


namespace QuizSystemTests;

[TestClass]
public class QuizTests
{
    // ==========================================
    // Test Initialization + Fields
    // ==========================================
    private List<Category> _categories;
    private List<Quiz> _quizzes;

    [TestInitialize]
    public void Setup()
    {
        //Required Sample Data
        _categories = new List<Category>();
        _quizzes = new List<Quiz>();

        _categories.Add(new Category());
        _quizzes.Add(new Quiz());
    }

    // ==========================================
    // Constructor Tests
    // ==========================================
    [TestMethod]
    public void QuizDefaultConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        int quizID = 0;
        string quizTitle = "";
        string quizDescription = "";
        Category quizCategory = _categories[0];
        List<Question> quizQuestions = new List<Question>();
        DateTime quizDate = DateTime.MinValue;

        // Assert (Value Types)
        Assert.AreEqual(quizID, _quizzes[0].QuizID, "QuizID was not initialized correctly.");
        Assert.AreEqual(quizTitle, _quizzes[0].QuizTitle, "QuizTitle was not initialized correctly.");
        Assert.AreEqual(quizDescription, _quizzes[0].QuizDescription, "QuizDescription was not initialized correctly.");
        Assert.AreEqual(quizDate, _quizzes[0].QuizDate, "QuizDate was not initialized correctly.");

        // Assert (Reference Types)
        Assert.IsNotNull(_quizzes[0].QuizCategory, "QuizCategory should not be null.");
        Assert.IsInstanceOfType(_quizzes[0].QuizCategory, typeof(Category), "QuizCategory is not of the right instance type.");

        Assert.IsNotNull(_quizzes[0].QuizQuestions, "QuizQuestions should not be null.");
        Assert.AreEqual(0, _quizzes[0].QuizQuestions.Count(), "QuizQuestions should start empty.");
    }

    [TestMethod]
    public void QuizParameterisedConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        int quizID = 1;
        string quizTitle = "title";
        string quizDescription = "description";
        Category quizCategory = _categories[0];
        List<Question> quizQuestions = new List<Question>();
        DateTime quizDate = new DateTime(2026, 1, 1);

        // Act
        _quizzes.Add(new Quiz(quizID, quizTitle, quizDescription, _categories[0], new DateTime(2026, 1, 1)));

        // Assert (Value Types)
        Assert.AreEqual(quizID, _quizzes[1].QuizID, "QuizID was not initialized correctly.");
        Assert.AreEqual(quizTitle, _quizzes[1].QuizTitle, "QuizTitle was not initialized correctly.");
        Assert.AreEqual(quizDescription, _quizzes[1].QuizDescription, "QuizDescription was not initialized correctly.");
        Assert.AreSame(quizCategory, _quizzes[1].QuizCategory, "QuizCategory was not initialized correctly.");
        Assert.AreEqual(quizDate, _quizzes[1].QuizDate, "QuizDate was not initialized correctly.");

        // Assert (Reference Types)
        Assert.IsNotNull(_quizzes[1].QuizQuestions, "QuizQuestions should not be null.");
        Assert.IsInstanceOfType(_quizzes[1].QuizQuestions, typeof(List<Question>), "QuizQuestions is not of the right instance type.");
        Assert.AreEqual(0, _quizzes[1].QuizQuestions.Count(), "QuizQuestions should start empty.");
    }

    // ==========================================
    // Get/Set Tests
    // ==========================================


    // ==========================================
    // Method Tests
    // ==========================================
}
