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
        //[TestMethod]
        //public void Run_AdminLogin_ValidCredentials_ShowsDashboard()
        //{
        //    // Arrange - simulate input: select 1, enter credentials, then exit
        //    using (var consoleInput = new StringReader("1\nadmin\nadmin123\n3\n"))
        //    using (var consoleOutput = new StringWriter())
        //    {
        //        // Redirect Console input and output
        //        Console.SetIn(consoleInput);
        //        Console.SetOut(consoleOutput);

        //        // Act
        //        quizSystem.Run();

        //        // Assert
        //        string output = consoleOutput.ToString();
        //        StringAssert.Contains(output, "--- Admin Login ---");
        //        StringAssert.Contains(output, "=== ULSTER UNIVERSITY QUIZ SYSTEM ===");
        //    } // Both consoleInput and consoleOutput are automatically disposed here
        //}

        //[TestMethod]
        //public void AdminLogin_InvalidLoginDetails_ShowsErrorMessage()
        //{
        //    // Arrange - simulate input: select 1, enter credentials, then exit
        //    var consoleInput = new StringReader("1\nwrong\npass\n3");
        //    Console.SetIn(consoleInput);

        //    var consoleOutput = new StringWriter();
        //    Console.SetOut(consoleOutput);

        //    // Act
        //    quizSystem.Run();

        //    // Assert
        //    string output = consoleOutput.ToString();
        //    StringAssert.Contains(output, "Invalid Admin Credentials.");
        //}
    }
}
