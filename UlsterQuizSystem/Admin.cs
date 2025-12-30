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

        public Admin(int id, string username, string password, string email)
            : base(id, username, password, email, "Admin")
        {
            LoginDate = DateTime.MinValue;
        }

        // ENTRY POINT FOR ADMIN
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
                    case "1": ManageQuestions(quizzes); break;
                    case "2": ManageUsers(students); break; // Updated method call
                    case "3": ManageCategories(categories, quizzes); break;
                    case "4": ViewSystemDataMenu(quizzes, categories, students, admins); break;
                    case "5": SaveToCSV(quizzes); break;
                    case "6": Logout(); active = false; break;
                    default: Console.WriteLine("Invalid selection."); break;
                }
            }
        }

        // --- UPDATED: MANAGE USERS MODULE ---
        private void ManageUsers(List<Student> students)
        {
            Console.Clear();
            Console.WriteLine("--- Manage Students ---");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Update Student Status");
            Console.WriteLine("3. List All Students");
            Console.WriteLine("4. Remove Student"); // NEW OPTION
            Console.Write("Select: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                // ADD STUDENT (Now with Manual Email)
                Console.WriteLine("\n--- Add New Student ---");
                Console.Write("Username: ");
                string u = Console.ReadLine();

                Console.Write("Password: ");
                string p = Console.ReadLine();

                // NEW: Manual Email Entry
                Console.Write("Email Address: ");
                string email = Console.ReadLine();

                int id = students.Any() ? students.Max(s => s.ID) + 1 : 100;

                // Create student with the manually entered email
                students.Add(new Student(id, u, p, email, "active"));
                Console.WriteLine($"Student '{u}' added successfully.");
            }
            else if (choice == "2")
            {
                // UPDATE STATUS
                Console.Write("Enter Student ID: ");
                if (int.TryParse(Console.ReadLine(), out int sid))
                {
                    var s = students.Find(x => x.ID == sid);
                    if (s != null)
                    {
                        Console.Write($"Current Status: {s.Status}. New (active/inactive): ");
                        string newStatus = Console.ReadLine().ToLower();
                        if (newStatus == "active" || newStatus == "inactive")
                        {
                            s.Status = newStatus;
                            Console.WriteLine("Status updated.");
                        }
                        else Console.WriteLine("Invalid status. Use 'active' or 'inactive'.");
                    }
                    else Console.WriteLine("Student not found.");
                }
            }
            else if (choice == "3")
            {
                // LIST STUDENTS
                // We pass an empty admin list just to satisfy the helper method signature
                ViewAllUsers(students, new List<Admin>());
            }
            else if (choice == "4")
            {
                // REMOVE STUDENT (New Feature)
                Console.WriteLine("\n--- Remove Student ---");
                // Show list first so they know the ID
                foreach (var s in students)
                    Console.WriteLine($"ID: {s.ID} | User: {s.Username} | Email: {s.Email}");

                Console.Write("\nEnter Student ID to remove: ");
                if (int.TryParse(Console.ReadLine(), out int removeId))
                {
                    var studentToRemove = students.Find(s => s.ID == removeId);
                    if (studentToRemove != null)
                    {
                        students.Remove(studentToRemove);
                        Console.WriteLine($"Student '{studentToRemove.Username}' has been removed from the system.");
                    }
                    else
                    {
                        Console.WriteLine("Student ID not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid ID format.");
                }
            }
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        // --- REST OF THE CLASS (Unchanged) ---

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
                default: Console.WriteLine("Invalid."); break;
            }
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        private void ViewAllQuestions(List<Quiz> quizzes)
        {
            Console.WriteLine("\n--- Master Question List ---");
            if (quizzes.Count == 0)
            {
                Console.WriteLine("No quizzes found.");
                return;
            }

            foreach (var q in quizzes)
            {
                Console.WriteLine($"\n[Quiz ID {q.QuizID}: {q.QuizTitle}]");
                foreach (var quest in q.QuizQuestions)
                {
                    Console.WriteLine($"   Q{quest.QuestionID}: {quest.QuestionText}");
                    Console.WriteLine($"      Correct: {quest.QuestionCorrectAnswer}");
                }
            }
        }

        private void ViewAllUsers(List<Student> students, List<Admin> admins)
        {
            Console.WriteLine("\n--- All System Users ---");
            if (admins.Any())
            {
                Console.WriteLine("[ADMINS]");
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

        private void ManageQuestions(List<Quiz> quizzes)
        {
            Console.Clear();
            Console.WriteLine("--- Manage Quizzes/Questions ---");
            ViewAllQuizzes(quizzes);
            Console.WriteLine("\n1. Add Question to Quiz");
            Console.WriteLine("2. Remove Question from Quiz");
            Console.Write("Select: ");
            string choice = Console.ReadLine();

            Console.Write("Enter Quiz ID: ");
            if (int.TryParse(Console.ReadLine(), out int qid))
            {
                var quiz = quizzes.Find(q => q.QuizID == qid);
                if (quiz != null)
                {
                    if (choice == "1") AddQuestionToQuiz(quiz);
                    else if (choice == "2") RemoveQuestionFromQuiz(quiz);
                }
                else Console.WriteLine("Quiz not found.");
            }
            Console.ReadKey();
        }

        private void AddQuestionToQuiz(Quiz quiz)
        {
            Console.Write("Question Text: ");
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

        private void RemoveQuestionFromQuiz(Quiz quiz)
        {
            foreach (var q in quiz.QuizQuestions) Console.WriteLine($"ID: {q.QuestionID} - {q.QuestionText}");
            Console.Write("Enter ID to remove: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                quiz.QuizQuestions.RemoveAll(x => x.QuestionID == id);
                Console.WriteLine("Question removed.");
            }
        }

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
            Console.ReadKey();
        }

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
