using UlsterQuizSystem;

namespace QuizSystemTests
{
    [TestClass]
    public sealed class QuizSystemTests
    {
        // ==========================================
        // Test Initialization + Fields
        // ==========================================
        private QuizSystem quizSystem;

        [TestInitialize]
        public void Setup()
        {
            // Required Sample Data
            quizSystem = new QuizSystem();
        }

        // ==========================================
        // Constructor Tests
        // ==========================================
        [TestMethod]
        public void QuizSystemConstructor_ShouldInitializeListsCorrectly()
        {
            //Assert
            //Assert.IsNotNull(quizSystem._admins, "_admins is not initialized.");
            //Assert.IsNotNull(quizSystem._students, "_students is not initialized.");
            //Assert.IsNotNull(quizSystem._quizzes, "_quizzes is not initialized.");
            //Assert.IsNotNull(quizSystem._categories, "_categories is not initialized.");
        }

        // ==========================================
        // Get/Set Tests
        // ==========================================


        // ==========================================
        // Method Tests
        // ==========================================
    }
}
