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
        private StringWriter consoleOutput;
        private StringReader consoleInput;

        [TestInitialize]
        public void Setup()
        {
            // Required Sample Data
            quizSystem = new QuizSystem();

            admins = new List<Admin>();
            admins.Add(new Admin(1, "John", "Doe", "email@mail.com"));

            consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);
        }

        [TestCleanup]
        public void Cleanup()
        {
            consoleOutput.Dispose();
            if (consoleInput != null) consoleInput.Dispose();
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
        [TestMethod]
        public void Run_AdminLogin_ValidCredentials_ShowsDashboard()
        {
            // Arrange - simulate input: select 1, enter credentials, then exit
            var simulatedInput = new StringReader("1\nadmin1\npass123\n3\n");
            Console.SetIn(simulatedInput);

            var consoleOutput = new StringWriter();
            Console.SetOut(new StreamWriter(consoleOutput) { AutoFlush = true });

            _quizSystem._admins = new List<Admin>
    {
        new Admin { Username = "admin1", Password = "pass123" }
    };

            // Act
            _quizSystem.Run();

            // Assert
            string output = consoleOutput.ToString();
            StringAssert.Contains(output, "--- Admin Login ---");
            StringAssert.Contains(output, "=== ULSTER UNIVERSITY QUIZ SYSTEM ===");
        }

        //[TestMethod]
        //public void AdminLogin_InvalidLoginDetails_ShowsErrorMessage()
        //{
        //    // Arrange
        //    consoleInput = new StringReader("wronguser\nwrongpass\n");
        //    Console.SetIn(consoleInput);

        //    // Act
        //    quizSystem.PerformAdminLogin();

        //    // Assert
        //    string output = consoleOutput.ToString();
        //    StringAssert.Contains(output, "Invalid Admin Credentials.");
        //}
    }
}
