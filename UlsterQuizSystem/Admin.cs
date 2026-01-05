using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UlsterQuizSystem
{
    public class Admin : User
    {
        // ==========================================
        // Fields, Properties, Get/Sets, Constructors
        // ==========================================
        public int AdminID { get; protected set; }
        public DateTime LoginDate { get; set; }
        protected static int adminNextID = 1;

        // Default Constructor
        public Admin() 
            : base()
        {
            AdminID = adminNextID;
            Role = UserRole.Admin;
            LoginDate = DateTime.MinValue;
            adminNextID++;
        }

        // Parameterized Constructor
        public Admin(string username, string password, string email)
            : base(username, password, email, UserRole.Admin)
        {
            AdminID = adminNextID;
            LoginDate = DateTime.MinValue;
            adminNextID++;
        }

        // ==========================================
        // Entry point for Admin Uenu
        // ==========================================
        public void DisplayAdminDashboard(QuizSystemData systemData, Admin admin)
        {
            LoginDate = DateTime.Now;
            bool active = true;

            while (active)
            {
                Console.Clear();
                Console.WriteLine($"--- ADMIN DASHBOARD ( logged in as: {Username} ) ---");
                Console.WriteLine($"Last Login: {LoginDate}");
                Console.WriteLine("1. Manage Quizzes & Questions");
                Console.WriteLine("2. Manage Users");
                Console.WriteLine("3. Manage Categories");
                Console.WriteLine("4. Reports");
                Console.WriteLine("5. Save Quizzes to CSV");
                Console.WriteLine("6. Logout");
                Console.Write("Select: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": ManageQuizzesAndQuestions(systemData.Quizzes, systemData.Categories); break;
                    case "2": ManageUsers(systemData.Students, systemData.Admins); break;
                    case "3": ManageCategories(systemData.Categories, systemData.Quizzes); break;
                    case "4": ViewSystemDataMenu(systemData.Quizzes, systemData.Categories, systemData.Students, systemData.Admins); break;
                    case "5": SaveToCSV(systemData.Quizzes); break;
                    case "6": Logout(); active = false; break;
                    default: Console.WriteLine("Invalid selection."); break;
                }
            }
        }

        // ==========================================
        // Manage Users Methods
        // ==========================================
        public void ManageUsers(List<Student> students, List<Admin> admins)
        {
            Console.Clear();
            Console.WriteLine("--- Manage Users ---");
            Console.WriteLine("1. Add User");
            Console.WriteLine("2. Update Student Details");
            Console.WriteLine("3. Remove User");
            Console.WriteLine("4. List All Users");
            Console.Write("Select: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("\n--- Add New User ---");
                    Console.WriteLine("Enter user role (Student/Admin): ");
                    string roleInputStr = Console.ReadLine();
                    UserRole roleInput;
                    if (!Enum.TryParse(roleInputStr, true, out roleInput) || (roleInput != UserRole.Student && roleInput != UserRole.Admin))
                    {
                        Console.WriteLine("Invalid role. returning...");
                        Console.ReadKey();
                    }
                    else if (roleInput == UserRole.Admin)
                    {
                        Console.Write("Username: "); string username = Console.ReadLine();
                        Console.Write("Password: "); string pass = Console.ReadLine();
                        Console.Write("Email Address: "); string email = Console.ReadLine();

                        AddAdmin(admins, username, pass, email);
                        Console.WriteLine($"Admin '{username}' added successfully.");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Write("Username: "); string username = Console.ReadLine();
                        Console.Write("Password: "); string pass = Console.ReadLine();
                        Console.Write("Email Address: "); string email = Console.ReadLine();

                        AddStudent(students, username, pass, email);
                        Console.WriteLine($"Student '{username}' added successfully.");
                        Console.ReadKey();
                    }
                    break;

                case "2":
                    foreach (var s in students) Console.WriteLine($"ID: {s.ID} | {s.Username}");
                    Console.Write("\nEnter Student ID to update: ");
                    if (int.TryParse(Console.ReadLine(), out int sid))
                    {
                        var student = students.Find(s => s.StudentID == sid);
                        if (student != null)
                        {
                            Console.WriteLine("\n--- Update Student Details (Leave blank to keep current) ---");
                            Console.Write($"Username (Current: {student.Username}): "); string username = Console.ReadLine();
                            Console.Write($"Password (Current: {student.Password}): "); string password = Console.ReadLine();
                            Console.Write($"Email (Current: {student.Email}): "); string email = Console.ReadLine();
                            Console.Write($"Status (Current: {student.Status}) [active/inactive]: "); string status = Console.ReadLine();

                            string result = UpdateStudent(students, sid, username, password, email, status);
                            Console.WriteLine(result);
                        }
                        else
                        {
                            Console.WriteLine("Student not found.");
                            Console.WriteLine("\nPress any key to return...");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID");
                        Console.WriteLine("\nPress any key to return...");
                        Console.ReadKey();
                    }

                    break;

                case "3":
                    foreach (var s in students) Console.WriteLine($"ID: {s.StudentID} | {s.Username}");
                    Console.Write("\nEnter User ID to remove: ");

                    if (int.TryParse(Console.ReadLine(), out int removeID))
                    {
                        int removed = students.RemoveAll(s => s.ID == removeId);
                        Console.WriteLine(removed > 0 ? "Student removed." : "ID not found.");
                        Console.ReadKey();
                    }
                    break;
                case "4":
                    ViewAllUsers(students, admins);
                    Console.WriteLine("\nPress any key to return...");
                    Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("Invalid selection.");
                    Console.WriteLine("\nPress any key to return...");
                    Console.ReadKey();
                    return;
            }
        }

        public static void AddAdmin(List<Admin> admins, string username, string pass, string email)
        {
            admins.Add(new Admin(username, pass, email));
        }

        public static void AddStudent(List<Student> students, string username, string pass, string email)
        {
            students.Add(new Student(username, pass, email, "active"));
        }

        public static string UpdateStudent(List<Student> students, int studentId, string newUsername, string newPassword, string newEmail, string newStatus)
        {
            var student = students.Find(s => s.StudentID == studentId);
            if (student == null)
                return "Student not found.";

            if (!string.IsNullOrWhiteSpace(newUsername))
                student.Username = newUsername;
            if (!string.IsNullOrWhiteSpace(newPassword))
                student.Password = newPassword;
            if (!string.IsNullOrWhiteSpace(newEmail))
                student.Email = newEmail;
            if (!string.IsNullOrWhiteSpace(newStatus))
            {
                newStatus = newStatus.ToLower();
                if (newStatus == "active" || newStatus == "inactive")
                    student.Status = newStatus;
                else
                    return "Invalid status ignored (must be 'active' or 'inactive').";
            }

            return "Student details updated successfully.";
        }

        public static string RemoveStudent(List<Student> students, int removeID)
        {
            int removed = students.RemoveAll(s => s.StudentID == removeID);
            return removed > 0 ? "Student removed." : "ID not found.";
        }

        // ==========================================
        // Manage Quizzes and Questions Methods
        // ==========================================
        private void ManageQuizzesAndQuestions(List<Quiz> quizzes, List<Category> categories)
        {
            Console.Clear();
            Console.WriteLine("--- Manage Quizzes & Questions ---");
            Console.WriteLine("1. Create New Quiz");
            Console.WriteLine("2. Remove Quiz");
            Console.WriteLine("3. Manage Questions (Add/Update/Remove)");
            Console.Write("Select: ");
            string choice = Console.ReadLine();

            switch (choice) { 

                case "1": CreateQuiz(quizzes, categories);
                    break;
                case "2": RemoveQuiz(quizzes);
                    break;
                case "3": ManageQuestionsInQuiz(quizzes);
                    break;
                default:
                    Console.WriteLine("Invalid selection.");
                    Console.WriteLine("\nPress any key to return...");
                    Console.ReadKey();
                    return;
            }
        }

        private void CreateQuiz(List<Quiz> quizzes, List<Category> categories)
        {
            Console.WriteLine("\n--- Create New Quiz ---");
            if (categories.Count == 0)
            {
                Console.WriteLine("Error: No categories exist. Create a category first.");
                Console.WriteLine("\nPress any key to return...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Select a Category for this Quiz:");
            foreach (var c in categories)
                Console.WriteLine($"ID: {c.CategoryID} | Name: {c.CategoryName}");

            Console.Write("Enter Category ID: ");
            if (!int.TryParse(Console.ReadLine(), out int catId) || !categories.Any(c => c.CategoryID == catId))
            {
                Console.WriteLine("Invalid Category ID.");
                Console.WriteLine("Press any key to return...");
                Console.ReadKey();
                return;
            }
            Category selectedCat = categories.Find(c => c.CategoryID == catId);

            Console.Write("Enter Quiz Title: ");
            string title = Console.ReadLine();
            Console.Write("Enter Quiz Description: ");
            string desc = Console.ReadLine();

            Quiz newQuiz = new Quiz(title, desc, selectedCat, DateTime.Now);
            quizzes.Add(newQuiz);

            Console.WriteLine($"Quiz '{title}' created successfully.");
            Console.ReadKey();
        }

        private void RemoveQuiz(List<Quiz> quizzes)
        {
            Console.WriteLine("\n--- Remove Quiz ---");
            foreach (var q in quizzes) Console.WriteLine($"ID: {q.QuizID} | Title: {q.QuizTitle}");

            Console.Write("Enter Quiz ID to remove: ");
            if (int.TryParse(Console.ReadLine(), out int qid))
            {
                var quiz = quizzes.Find(q => q.QuizID == qid);
                if (quiz != null)
                {
                    quizzes.Remove(quiz);
                    Console.WriteLine("Quiz deleted.");
                }
                else Console.WriteLine("Quiz not found.");
            }
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        private void ManageQuestionsInQuiz(List<Quiz> quizzes)
        {
            if (quizzes.Count == 0)
            {
                Console.WriteLine("No quizzes available.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\n--- Select Quiz to Edit ---");
            foreach (var q in quizzes) Console.WriteLine($"{q.QuizID}. {q.QuizTitle}");
            Console.Write("Enter Quiz ID: ");

            if (int.TryParse(Console.ReadLine(), out int qid))
            {
                var quiz = quizzes.Find(q => q.QuizID == qid);
                if (quiz != null)
                {
                    Console.WriteLine($"\nEditing Quiz: {quiz.QuizTitle}");
                    Console.WriteLine("1. Add Question");
                    Console.WriteLine("2. Update Question (Text/Options)");
                    Console.WriteLine("3. Remove Question");
                    Console.Write("Select: ");
                    string subChoice = Console.ReadLine();

                    if (subChoice == "1") AddQuestionToQuizUI(quiz);
                    else if (subChoice == "2") UpdateQuestionInQuiz(quiz);
                    else if (subChoice == "3") RemoveQuestionFromQuizUI(quiz);
                }
                else Console.WriteLine("Quiz not found.");
            }
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        private void AddQuestionToQuizUI(Quiz quizzes)
        {
            Console.Write("\nQuestion Text: "); string text = Console.ReadLine();
            Console.Write("Correct Answer: "); string correct = Console.ReadLine();
            Console.WriteLine("Enter Difficulty Level (Easy/Medium/Hard): "); string difficulty = Console.ReadLine();
            Console.WriteLine("How many questions do you wish to add to the Quiz?"); int questionAmount = Convert.ToInt32(Console.ReadLine());

            List<string> options = new List<string>();
            Console.WriteLine($"Enter {questionAmount} Options:");
            for (int i = 1; i <= questionAmount; i++) { Console.Write($"{i}: "); options.Add(Console.ReadLine()); }

            AddQuestionToQuiz(quizzes, text, options, correct, difficulty);
        }

        public static void AddQuestionToQuiz(Quiz quiz, string text, List<string> options, string correct, string difficulty)
        {
            quiz.QuizQuestions.Add(new Question(text, options, correct, difficulty));
            Console.WriteLine("Question added.");
        }

        private void UpdateQuestionInQuiz(Quiz quiz)
        {
            if (quiz.QuizQuestions.Count == 0) { Console.WriteLine("No questions to update."); return; }

            foreach (var q in quiz.QuizQuestions)
                Console.WriteLine($"ID {q.QuestionID}: {q.QuestionText}");

            Console.Write("Enter Question ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int qid))
            {
                var question = quiz.QuizQuestions.Find(x => x.QuestionID == qid);
                if (question != null)
                {
                    Console.WriteLine("\n--- Update Question Details (Leave blank to keep current) ---");

                    Console.Write($"Question Text (Current: {question.QuestionText}): ");
                    string input = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(input)) question.QuestionText = input;

                    Console.Write($"Correct Answer (Current: {question.QuestionCorrectAnswer}): ");
                    input = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(input)) question.QuestionCorrectAnswer = input;

                    Console.Write($"Question Difficulty (Current: {question.QuestionDifficultyLevel}): ");
                    input = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(input)) question.QuestionDifficultyLevel = input;

                    Console.WriteLine("\n--- Update Options ---");
                    for (int i = 0; i < question.QuestionOptions.Count; i++)
                    {
                        Console.Write($"Option {i + 1} (Current: {question.QuestionOptions[i]}): ");
                        string newOpt = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newOpt))
                        {
                            question.QuestionOptions[i] = newOpt;
                        }
                    }

                    Console.WriteLine("Question updated successfully.");
                }
                else Console.WriteLine("Question ID not found.");
            }
        }

        public void RemoveQuestionFromQuizUI(Quiz quiz)
        {
            foreach (var q in quiz.QuizQuestions) Console.WriteLine($"ID: {q.QuestionID} - {q.QuestionText}");
            Console.Write("Enter ID to remove: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                string result = RemoveQuestionFromQuiz(quiz, id);
                Console.WriteLine(result);
            }
        }

        public static string RemoveQuestionFromQuiz(Quiz quiz, int id)
        {
            int removed = quiz.QuizQuestions.RemoveAll(x => x.QuestionID == id);
            if (removed > 0)
            {
                return "Question Removed.";
            }
            else
            {
                return "ID not found.";
            }
        }

        // ==========================================
        // Manage Categories Methods
        // ==========================================
        public void ManageCategories(List<Category> categories, List<Quiz> quizzes)
        {
            Console.Clear();
            Console.WriteLine("--- Manage Categories ---");
            ViewAllCategories(categories);
            Console.WriteLine("\n1. Add Category");
            Console.WriteLine("2. Remove Category");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Name: "); string name = Console.ReadLine();
                    Console.Write("Desc: "); string desc = Console.ReadLine();
                    Admin.AddCategory(categories, name, desc);
                    Console.WriteLine("Category Added.");
                    break;
                case "2":
                    Console.Write("ID to remove: ");
                    if (int.TryParse(Console.ReadLine(), out int cid))
                    {
                        if (quizzes.Any(q => q.QuizCategory.CategoryID == cid))
                            Console.WriteLine("Cannot delete: Category in use.");
                        else
                        {
                            string result = Admin.RemoveCategory(categories, cid);
                            Console.WriteLine(result);
                        }
                    }
                    break;
                default:
                    Console.WriteLine("Invalid selection.");
                    break;
            }

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        public static void AddCategory(List<Category> categories, string name, string desc)
        {
            categories.Add(new Category(name, desc));
        }

        public static string RemoveCategory(List<Category> categories, int cid)
        {
            int removed = categories.RemoveAll(c => c.CategoryID == cid);
            return removed > 0 ? "Deleted." : "ID not found.";
        }

        // ==========================================
        // Reports for displaying all Data Methods
        // ==========================================
        private void ViewSystemDataMenu(List<Quiz> quizzes, List<Category> categories, List<Student> students, List<Admin> admins)
        {
            Console.Clear();
            Console.WriteLine("--- System Reports ---");
            Console.WriteLine("1. View All Users (Admins & Students)");
            Console.WriteLine("2. View All Quizzes (Summary)");
            Console.WriteLine("3. View All Categories");
            Console.WriteLine("4. View All Questions (Detailed)");
            Console.Write("Select: ");

            switch (Console.ReadLine())
            {
                case "1": ViewAllUsers(students, admins); break;
                case "2": ViewAllQuizzes(quizzes); break;
                case "3": ViewAllCategories(categories); break;
                case "4": ViewAllQuestions(quizzes); break;
            }
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        private void ViewAllQuestions(List<Quiz> quizzes)
        {
            Console.WriteLine("\n--- Master Question List ---");
            foreach (var q in quizzes)
            {
                Console.WriteLine($"\n[Quiz: {q.QuizTitle}]");
                foreach (var quest in q.QuizQuestions)
                {
                    Console.WriteLine($"   Q{quest.QuestionID}: {quest.QuestionText}");
                    Console.WriteLine($"      Correct: {quest.QuestionCorrectAnswer}");
                    Console.WriteLine($"      Options: {string.Join(", ", quest.QuestionOptions)}");
                    Console.WriteLine($"      Difficulty: {quest.QuestionDifficultyLevel}");
                }
            }
        }

        private void ViewAllUsers(List<Student> students, List<Admin> admins)
        {
            if (admins.Any())
            {
                Console.WriteLine("\n[ADMINS]");
                foreach (var a in admins) Console.WriteLine($"ID: {a.AdminID} | User: {a.Username} | Email: {a.Email} | Last Login: {a.LoginDate}");
            }
            Console.WriteLine("\n[STUDENTS]");
            foreach (var s in students)
                Console.WriteLine($"ID: {s.StudentID} | User: {s.Username} | Status: {s.Status} | Email: {s.Email}");
        }

        private void ViewAllQuizzes(List<Quiz> quizzes)
        {
            Console.WriteLine("\n--- All Quizzes ---");
            foreach (var q in quizzes)
            {
                string catName = q.QuizCategory != null ? q.QuizCategory.CategoryName : "Unassigned";
                Console.WriteLine($"ID: {q.QuizID} | Title: {q.QuizTitle} | Category: {catName} | Questions: {q.QuizQuestions.Count} | Creation Date: {q.QuizDate}");
            }
        }

        private void ViewAllCategories(List<Category> categories)
        {
            Console.WriteLine("\n--- All Categories ---");
            foreach (var c in categories)
                Console.WriteLine($"ID: {c.CategoryID} | Name: {c.CategoryName} | Desc: {c.CategoryDescription}");
        }

        // ==========================================
        // Export Quizzes and Questions to CSV Method
        // ==========================================
        private void SaveToCSV(List<Quiz> quizzes)
        {
            try
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("questions.csv"))
                {
                    sw.WriteLine("QuizID,QuizTitle,QuestionID,QuestionText,Answer,Options");

                    foreach (var q in quizzes)
                    {
                        foreach (var quest in q.QuizQuestions)
                        {
                            string optionsString = string.Join("|", quest.QuestionOptions);

                            sw.WriteLine($"{q.QuizID},{q.QuizTitle},{quest.QuestionID},{quest.QuestionText},{quest.QuestionCorrectAnswer},{optionsString}");
                        }
                    }
                }
                Console.WriteLine("Saved to questions.csv");
            }
            catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
            Console.ReadKey();
        }

        // ==========================================
        // Overridden Base User-Class Methods
        // ==========================================
        public override string ToString()
        {
            return $"ID: {AdminID} | " + base.ToString() + $" | LastLoginDate: {LoginDate}";
        }

        // ==========================================
        // Method to Reset ID Counter
        // ==========================================
        public static void ResetAdminNextIDCounter()
        {
            adminNextID = 1;
        }
    }
}

// Class created by Josh Reid
// Citations: Intellisense code suggestions from Visual Studio 2022
// general syntax and code solutions found on StackOverflow and google searches gemini.
