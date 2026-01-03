using UlsterQuizSystem;

namespace QuizSystemTests;

[TestClass]
public class QuestionTests
{
    // ==========================================
    // Test Initialization + Fields
    // ==========================================
    private List<Question> _questions;

    [TestInitialize]
    public void Setup()
    {
        //Required Sample Data
        _questions = new List<Question>();
        _questions.Add(new Question());
    }

    // ==========================================
    // Constructor Tests
    // ==========================================
    [TestMethod]
    public void QuestionDefaultConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        int questionID = 0;
        string questionText = "";
        List<string> questionOptions = new List<string>();
        string questionCorrectAnswer = "";
        string questionDifficultyLevel = "";

        // Assert (Value Types)
        Assert.AreEqual(questionID, _questions[0].QuestionID, "QuestionID was not initialized correctly.");
        Assert.AreEqual(questionText, _questions[0].QuestionText, "QuestionText was not initialized correctly.");
        Assert.AreEqual(questionCorrectAnswer, _questions[0].QuestionCorrectAnswer, "QuestionCorrectAnswer was not initialized correctly.");
        Assert.AreEqual(questionDifficultyLevel, _questions[0].QuestionDifficultyLevel, "QuestionDifficultyLevel was not initialized correctly.");

        // Assert (Reference Types)
        Assert.IsNotNull(_questions[0].QuestionOptions, "QuestionOptions should not be null.");
        Assert.IsInstanceOfType(_questions[0].QuestionOptions, typeof(List<string>), "QuestionOptions is not of the right instance type.");
        Assert.AreEqual(0, _questions[0].QuestionOptions.Count(), "QuestionOptions should start empty.");
    }

    [TestMethod]
    public void QuestionParameterisedConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        int questionID = 0;
        string questionText = "";
        List<string> questionOptions = new List<string>();
        string questionCorrectAnswer = "";
        string questionDifficultyLevel = "";

        // Act
        _questions.Add(new Question(questionID, questionText, questionOptions, questionCorrectAnswer, questionDifficultyLevel));

        // Assert (Value Types)
        Assert.AreEqual(questionID, _questions[1].QuestionID, "QuestionID was not initialized correctly.");
        Assert.AreEqual(questionText, _questions[1].QuestionText, "QuestionText was not initialized correctly.");
        Assert.AreEqual(questionOptions, _questions[1].QuestionOptions, "QuestionDifficultyLevel was not initialized correctly.");
        Assert.AreEqual(questionCorrectAnswer, _questions[1].QuestionCorrectAnswer, "QuestionCorrectAnswer was not initialized correctly.");
        Assert.AreEqual(questionDifficultyLevel, _questions[1].QuestionDifficultyLevel, "QuestionDifficultyLevel was not initialized correctly.");
    }

    // ==========================================
    // Get/Set Tests
    // ==========================================


    // ==========================================
    // Method Tests
    // ==========================================
}
