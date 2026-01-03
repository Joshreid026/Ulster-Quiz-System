using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlsterQuizSystem
{
    public class Admin : User
    {
        public DateTime LoginDate { get; set; }

        public Admin() : base()
        {
            Role = "admin";
            LoginDate = DateTime.MinValue;
        }

        public Admin(int id, string username, string password, string email)
            : base(id, username, password, email, "Admin")
        {
            Role = "admin";
            LoginDate = DateTime.MinValue;
        }

        // ==========================================
        // Entry point for admin menu
        // ==========================================

        public void DisplayDashboard(List<Quiz> quizzes, List<Category> categories, List<Student> students, List<Admin> admins)
        {
            LoginDate = DateTime.Now;
            bool active = true;

            while (active)
            {
                Console.Clear();
                Console.WriteLine($"--- ADMIN DASHBOARD ({Username}) ---");
                Console.WriteLine($"Last Login: {LoginDate}");
                Console.WriteLine("1. Manage Quizzes & Questions");
                Console.WriteLine("2. Manage Users (Students)");
                Console.WriteLine("3. Manage Categories");
                Console.WriteLine("4. View All System Data (Reports)");
                Console.WriteLine("5. Save Quizzes to CSV");
                Console.WriteLine("6. Logout");
                Console.Write("Select: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": ManageQuizzesAndQuestions(quizzes, categories); break;
                    case "2": ManageUsers(students); break;
                    case "3": ManageCategories(categories, quizzes); break;
                    case "4": ViewSystemDataMenu(quizzes, categories, students, admins); break;
                    case "5": SaveToCSV(quizzes); break;
                    case "6": Logout(); active = false; break;
                    default: Console.WriteLine("Invalid selection."); break;
                }
            }
        }

        // ==========================================
        // Manage users methods
        // ==========================================

        private void ManageUsers(List<Student> students)
        {
            Console.Clear();
            Console.WriteLine("--- Manage Students ---");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Update Student Details");
            Console.WriteLine("3. List All Students");
            Console.WriteLine("4. Remove Student");
            Console.Write("Select: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("\n--- Add New Student ---");
                    Console.Write("Username: "); string username = Console.ReadLine();
                    Console.Write("Password: "); string pass = Console.ReadLine();
                    Console.Write("Email Address: "); string email = Console.ReadLine();

                    int id = students.Any() ? students.Max(s => s.ID) + 1 : 100;
                    students.Add(new Student(id, username, pass, email, "active"));
                    Console.WriteLine($"Student '{username}' added successfully.");
                    break;

                case "2":
                    Console.Write("\nEnter Student ID to update: ");
                    if (int.TryParse(Console.ReadLine(), out int sid))
                    {
                        var s = students.Find(x => x.ID == sid);
                        if (s != null)
                        {
                            Console.WriteLine("\n--- Update Student Details (Leave blank to keep current) ---");

                            Console.Write($"Username (Current: {s.Username}): ");
                            string input = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(input)) s.Username = input;

                            Console.Write($"Password (Current: {s.Password}): ");
                            input = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(input)) s.Password = input;

                            Console.Write($"Email (Current: {s.Email}): ");
                            input = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(input)) s.Email = input;

                            Console.Write($"Status (Current: {s.Status}) [active/inactive]: ");
                            input = Console.ReadLine().ToLower();
                            if (!string.IsNullOrWhiteSpace(input))
                            {
                                if (input == "active" || input == "inactive") s.Status = input;
                                else Console.WriteLine("Invalid status ignored (must be 'active' or 'inactive').");
                            }

                            Console.WriteLine("Student details updated successfully.");
                        }
                        else Console.WriteLine("Student not found.");
                    }
                    break;

                case "3":
                    ViewAllUsers(students, new List<Admin>());
                    break;

                case "4":
                    foreach (var s in students) Console.WriteLine($"ID: {s.ID} | {s.Username}");
                    Console.Write("\nEnter Student ID to remove: ");
                    if (int.TryParse(Console.ReadLine(), out int removeId))
                    {
                        int removed = students.RemoveAll(s => s.ID == removeId);
                        Console.WriteLine(removed > 0 ? "Student removed." : "ID not found.");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid selection.");
                    Console.WriteLine("\nPress any key to return...");
                    Console.ReadKey();
                    return;
            }
        }

        // ==========================================
        // Manage quizzes and questions methods
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

            int newId = quizzes.Any() ? quizzes.Max(q => q.QuizID) + 1 : 1;
            Quiz newQuiz = new Quiz(newId, title, desc, selectedCat, DateTime.Now);
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

                    if (subChoice == "1") AddQuestionToQuiz(quiz);
                    else if (subChoice == "2") UpdateQuestionInQuiz(quiz);
                    else if (subChoice == "3") RemoveQuestionFromQuiz(quiz);
                }
                else Console.WriteLine("Quiz not found.");
            }
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        private void AddQuestionToQuiz(Quiz quiz)
        {
            Console.Write("\nQuestion Text: ");
            string text = Console.ReadLine();
            Console.Write("Correct Answer: ");
            string correct = Console.ReadLine();

            List<string> options = new List<string>();
            Console.WriteLine("Enter 4 Options:");
            for (int i = 1; i <= 4; i++) { Console.Write($"{i}: "); options.Add(Console.ReadLine()); }

            int newId = quiz.QuizQuestions.Any() ? quiz.QuizQuestions.Max(q => q.QuestionID) + 1 : 1;
            quiz.QuizQuestions.Add(new Question(newId, text, options, correct, "Medium"));
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

        private void RemoveQuestionFromQuiz(Quiz quiz)
        {
            foreach (var q in quiz.QuizQuestions) Console.WriteLine($"ID: {q.QuestionID} - {q.QuestionText}");
            Console.Write("Enter ID to remove: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                int count = quiz.QuizQuestions.RemoveAll(x => x.QuestionID == id);
                if (count > 0) Console.WriteLine("Question removed.");
                else Console.WriteLine("ID not found.");
            }
        }

        // ==========================================
        // Manage categories methods
        // ==========================================
        private void ManageCategories(List<Category> categories, List<Quiz> quizzes)
        {
            Console.Clear();
            Console.WriteLine("--- Manage Categories ---");
            ViewAllCategories(categories);
            Console.WriteLine("\n1. Add Category");
            Console.WriteLine("2. Remove Category");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Name: "); string name = Console.ReadLine();
                Console.Write("Desc: "); string desc = Console.ReadLine();
                int id = categories.Any() ? categories.Max(c => c.CategoryID) + 1 : 1;
                categories.Add(new Category(id, name, desc));
                Console.WriteLine("Category Added.");
            }
            else if (choice == "2")
            {
                Console.Write("ID to remove: ");
                if (int.TryParse(Console.ReadLine(), out int cid))
                {
                    if (quizzes.Any(q => q.QuizCategory.CategoryID == cid))
                        Console.WriteLine("Cannot delete: Category in use.");
                    else
                    {
                        categories.RemoveAll(c => c.CategoryID == cid);
                        Console.WriteLine("Deleted.");
                    }
                }
            }
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        // ==========================================
        // Reports for displaying all data methods
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
                }
            }
        }

        private void ViewAllUsers(List<Student> students, List<Admin> admins)
        {
            if (admins.Any())
            {
                Console.WriteLine("\n[ADMINS]");
                foreach (var a in admins) Console.WriteLine($"ID: {a.ID} | User: {a.Username} | Email: {a.Email}");
            }
            Console.WriteLine("\n[STUDENTS]");
            foreach (var s in students)
                Console.WriteLine($"ID: {s.ID} | User: {s.Username} | Status: {s.Status} | Email: {s.Email}");
        }

        private void ViewAllQuizzes(List<Quiz> quizzes)
        {
            Console.WriteLine("\n--- All Quizzes ---");
            foreach (var q in quizzes)
            {
                string catName = q.QuizCategory != null ? q.QuizCategory.CategoryName : "Unassigned";
                Console.WriteLine($"ID: {q.QuizID} | Title: {q.QuizTitle} | Category: {catName} | Questions: {q.QuizQuestions.Count}");
            }
        }

        private void ViewAllCategories(List<Category> categories)
        {
            Console.WriteLine("\n--- All Categories ---");
            foreach (var c in categories)
                Console.WriteLine($"ID: {c.CategoryID} | Name: {c.CategoryName} | Desc: {c.CategoryDescription}");
        }

        // ==========================================
        // Export quizzes and questions to CSV method
        // ==========================================

        private void SaveToCSV(List<Quiz> quizzes)
        {
            try
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter("questions.csv"))
                {
                    sw.WriteLine("QuizID,QuizTitle,QuestionID,QuestionText,Answer");
                    foreach (var q in quizzes)
                        foreach (var quest in q.QuizQuestions)
                            sw.WriteLine($"{q.QuizID},{q.QuizTitle},{quest.QuestionID},{quest.QuestionText},{quest.QuestionCorrectAnswer}");
                }
                Console.WriteLine("Saved to questions.csv");
            }
            catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
            Console.ReadKey();
        }
    }
}

// Class created by Josh Reid