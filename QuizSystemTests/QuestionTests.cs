using System.Reflection;
using UlsterQuizSystem;

namespace QuizSystemTests;

[TestClass]
public class QuestionTests
{
    // ==========================================
    // Test Initialization + Fields
    // ==========================================
    private List<Question> questions;

    [TestInitialize]
    public void Setup()
    {
        //Required Sample Data
        Question.ResetQuestionNextIDCounter();

        questions = new List<Question>();
    }

    // ==========================================
    // Constructor Tests
    // ==========================================
    [TestMethod]
    public void QuestionDefaultConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        int questionID = 1;
        string questionText = "";
        List<string> questionOptions = new List<string>();
        string questionCorrectAnswer = "";
        string questionDifficultyLevel = "";

        // Act
        questions.Add(new Question());

        // Assert (Value Types)
        Assert.AreEqual(questionID, questions[0].QuestionID, "QuestionID was not initialized correctly.");
        Assert.AreEqual(questionText, questions[0].QuestionText, "QuestionText was not initialized correctly.");
        Assert.AreEqual(questionCorrectAnswer, questions[0].QuestionCorrectAnswer, "QuestionCorrectAnswer was not initialized correctly.");
        Assert.AreEqual(questionDifficultyLevel, questions[0].QuestionDifficultyLevel, "QuestionDifficultyLevel was not initialized correctly.");

        // Assert (Reference Types)
        Assert.IsNotNull(questions[0].QuestionOptions, "QuestionOptions should not be null.");
        Assert.IsInstanceOfType(questions[0].QuestionOptions, typeof(List<string>), "QuestionOptions is not of the right instance type.");
        Assert.AreEqual(0, questions[0].QuestionOptions.Count(), "QuestionOptions should start empty.");
    }

    [TestMethod]
    public void QuestionParameterisedConstructor_ShouldInitialiseProperties()
    {
        // Arrange
        int questionID = 1;
        string questionText = "";
        List<string> questionOptions = new List<string>();
        string questionCorrectAnswer = "";
        string questionDifficultyLevel = "";

        // Act
        questions.Add(new Question(questionText, questionOptions, questionCorrectAnswer, questionDifficultyLevel));

        // Assert (Value Types)
        Assert.AreEqual(questionID, questions[0].QuestionID, "QuestionID was not initialized correctly.");
        Assert.AreEqual(questionText, questions[0].QuestionText, "QuestionText was not initialized correctly.");
        Assert.AreEqual(questionOptions, questions[0].QuestionOptions, "QuestionDifficultyLevel was not initialized correctly.");
        Assert.AreEqual(questionCorrectAnswer, questions[0].QuestionCorrectAnswer, "QuestionCorrectAnswer was not initialized correctly.");
        Assert.AreEqual(questionDifficultyLevel, questions[0].QuestionDifficultyLevel, "QuestionDifficultyLevel was not initialized correctly.");
    }

    // ==========================================
    // Get/Set Tests
    // ==========================================
    [TestMethod]
    public void QuestionID_ShouldSetCorrectly()
    {
        // Arrange
        int questionID = 1;
        string questionText = "What damage does a sword deal?";
        List<string> questionOptions = new List<string> { "Sting", "Slash", "Sash", "Slab" };
        string questionCorrectAnswer = "Slash";
        string questionDifficultyLevel = "Easy";

        // Act
        questions.Add(new Question(questionText, questionOptions, questionCorrectAnswer, questionDifficultyLevel));

        // Assert
        Assert.AreEqual(questionID, questions[0].QuestionID, "QuestionID was not set correctly.");
    }

    [TestMethod]
    public void QuestionID_Setter_ShouldBePrivate()
    {
        // Arrange
        PropertyInfo property = typeof(Question).GetProperty("QuestionID");

        // Act
        MethodInfo setter = property.SetMethod;

        // Assert
        Assert.IsTrue(setter.IsPrivate, "QuestionID setter should be private.");
    }

    [TestMethod]
    public void QuestionProperties_ShouldSetAndGetValues()
    {
        // Arrange
        string questionText = "What damage does a sword deal?";
        List<string> questionOptions = new List<string> { "Sting", "Slash", "Sash", "Slab"} ;
        string questionCorrectAnswer = "Slash";
        string questionDifficultyLevel = "Easy";

        // Act
        questions.Add(new Question());

        questions[0].QuestionText = questionText;
        questions[0].QuestionOptions = questionOptions;
        questions[0].QuestionCorrectAnswer = questionCorrectAnswer;
        questions[0].QuestionDifficultyLevel = questionDifficultyLevel;

        // Assert
        Assert.AreEqual(questionText, questions[0].QuestionText, "QuestionText was not set correctly.");
        Assert.AreSame(questionOptions, questions[0].QuestionOptions, "QuestionDifficultyLevel was not set correctly.");
        Assert.AreEqual(questionCorrectAnswer, questions[0].QuestionCorrectAnswer, "QuestionCorrectAnswer was not set correctly.");
        Assert.AreEqual(questionDifficultyLevel, questions[0].QuestionDifficultyLevel, "QuestionDifficultyLevel was not set correctly.");
    }

    // ==========================================
    // Method Tests
    // ==========================================
}
