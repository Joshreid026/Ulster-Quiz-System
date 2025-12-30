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
        public void DisplayDashboard(List<Quiz> quizzes, List<Category> categories, List<Student> students)
        {
            LoginDate = DateTime.Now; // Update login time on entry
            bool active = true;

            while (active)
            {
                Console.Clear();
                Console.WriteLine($"--- ADMIN DASHBOARD ({Username}) ---");
                Console.WriteLine($"Last Login: {LoginDate}");
                Console.WriteLine("1. Manage Questions");
                Console.WriteLine("2. Manage Users (Students)");
                Console.WriteLine("3. Manage Categories");
                Console.WriteLine("4. Save Quizzes to CSV");
                Console.WriteLine("5. Logout");
                Console.Write("Select: ");

                switch (Console.ReadLine())
                {
                    case "1": ManageQuestions(quizzes); break;
                    case "2": ManageUsers(students); break;
                    case "3": ManageCategories(categories, quizzes); break;
                    case "4": SaveToCSV(quizzes); break;
                    case "5": Logout(); active = false; break;
                }
            }
        }

        // --- MODULE: QUESTION MANAGEMENT ---
        private void ManageQuestions(List<Quiz> quizzes)
        {
            Console.Clear();
            Console.WriteLine("--- Manage Questions ---");
            Console.WriteLine("1. Add Question");
            Console.WriteLine("2. Remove Question");
            Console.Write("Select: ");
            string choice = Console.ReadLine();

            // Select Quiz first
            Console.WriteLine("\nSelect Quiz:");
            foreach (var q in quizzes) Console.WriteLine($"{q.QuizID}. {q.QuizTitle}");
            Console.Write("Quiz ID: ");

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
            Console.Write("Enter Question Text: ");
            string text = Console.ReadLine();
            Console.Write("Enter Correct Answer: ");
            string correct = Console.ReadLine();

            List<string> options = new List<string>();
            Console.WriteLine("Enter 4 Options:");
            for (int i = 1; i <= 4; i++)
            {
                Console.Write($"Option {i}: ");
                options.Add(Console.ReadLine());
            }

            // Auto-generate ID based on existing count
            int newId = quiz.QuizQuestions.Any() ? quiz.QuizQuestions.Max(q => q.QuestionID) + 1 : 1;

            quiz.QuizQuestions.Add(new Question(newId, text, options, correct, "Medium"));
            Console.WriteLine("Question added successfully.");
        }

        private void RemoveQuestionFromQuiz(Quiz quiz)
        {
            foreach (var q in quiz.QuizQuestions)
                Console.WriteLine($"ID {q.QuestionID}: {q.QuestionText}");

            Console.Write("Enter ID to remove: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                quiz.QuizQuestions.RemoveAll(x => x.QuestionID == id);
                Console.WriteLine("Question removed.");
            }
        }

        // --- MODULE: USER MANAGEMENT ---
        private void ManageUsers(List<Student> students)
        {
            Console.Clear();
            Console.WriteLine("--- Manage Students ---");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Update Student Status (Active/Inactive)");
            Console.Write("Select: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Username: "); string u = Console.ReadLine();
                Console.Write("Password: "); string p = Console.ReadLine();
                int id = students.Any() ? students.Max(s => s.ID) + 1 : 100;
                students.Add(new Student(id, u, p, $"{u}@ulster.ac.uk", "active"));
                Console.WriteLine("Student Created.");
            }
            else if (choice == "2")
            {
                foreach (var s in students) Console.WriteLine(s.ToString());
                Console.Write("Enter Student ID: ");
                if (int.TryParse(Console.ReadLine(), out int sid))
                {
                    var student = students.Find(s => s.ID == sid);
                    if (student != null)
                    {
                        Console.Write($"Current Status: {student.Status}. Enter new status (active/inactive): ");
                        student.Status = Console.ReadLine().ToLower();
                        Console.WriteLine("Status Updated.");
                    }
                }
            }
            Console.ReadKey();
        }

        // --- MODULE: CATEGORY MANAGEMENT ---
        private void ManageCategories(List<Category> categories, List<Quiz> quizzes)
        {
            Console.Clear();
            Console.WriteLine("--- Manage Categories ---");
            Console.WriteLine("1. Add Category");
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
                foreach (var c in categories) Console.WriteLine(c.ToString());
                Console.Write("Enter ID to remove: ");
                if (int.TryParse(Console.ReadLine(), out int cid))
                {
                    if (quizzes.Any(q => q.QuizCategory.CategoryID == cid))
                        Console.WriteLine("Cannot delete: Category is in use.");
                    else
                    {
                        categories.RemoveAll(c => c.CategoryID == cid);
                        Console.WriteLine("Category Deleted.");
                    }
                }
            }
            Console.ReadKey();
        }

        // --- MODULE: EXPORT ---
        private void SaveToCSV(List<Quiz> quizzes)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("questions.csv"))
                {
                    sw.WriteLine("QuizID,QuizTitle,QuestionID,QuestionText,Answer");
                    foreach (var q in quizzes)
                    {
                        foreach (var quest in q.QuizQuestions)
                        {
                            sw.WriteLine($"{q.QuizID},{q.QuizTitle},{quest.QuestionID},{quest.QuestionText},{quest.QuestionCorrectAnswer}");
                        }
                    }
                }
                Console.WriteLine("Data saved to questions.csv successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}");
            }
            Console.ReadKey();
        }
    }
}
