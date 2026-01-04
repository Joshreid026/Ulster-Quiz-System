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
        private List<Admin> admins;

        [TestInitialize]
        public void Setup()
        {
            // Required Sample Data
            quizSystem = new QuizSystem();

            admins = new List<Admin>();
            admins.Add(new Admin(1, "John", "Doe", "email@mail.com"));
        }

        // ==========================================
        // Constructor Tests
        // ==========================================

        // ==========================================
        // Get/Set Tests
        // ==========================================


        // ==========================================
        // Method Tests
        // ==========================================
    }
}
