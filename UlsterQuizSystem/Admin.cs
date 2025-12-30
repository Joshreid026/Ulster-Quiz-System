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
            LoginDate = DateTime.Now;
        }

        // Static method to generate sample admins (Factory pattern) 
        public static List<Admin> CreateSampleAdmins()
        {
            return new List<Admin>
            {
                new Admin(1, "admin", "admin123", "admin@ulster.ac.uk")
            };
        }

        // "AddProduct" interpreted as "Add Question/Quiz" per spec context
        public void AddProduct(List<Quiz> quizzes)
        {
            Console.WriteLine("\n--- Add New Question (AddProduct) ---");
            Console.WriteLine("Select a Quiz ID to add a question to:");
            foreach (var q in quizzes) Console.WriteLine($"{q.QuizID}. {q.QuizTitle}");

            if (int.TryParse(Console.ReadLine(), out int qId))
            {
                var quiz = quizzes.Find(q => q.QuizID == qId);
                if (quiz != null)
                {
                    Console.Write("Enter Question Text: ");
                    string text = Console.ReadLine();
                    Console.Write("Enter Correct Answer: ");
                    string correct = Console.ReadLine();

                    List<string> options = new List<string>();
                    Console.WriteLine("Enter 4 options:");
                    for (int i = 0; i < 4; i++)
                    {
                        Console.Write($"Option {i + 1}: ");
                        options.Add(Console.ReadLine());
                    }

                    // Generate ID
                    int newId = quiz.QuizQuestions.Any() ? quiz.QuizQuestions.Max(x => x.QuestionID) + 1 : 1;

                    quiz.QuizQuestions.Add(new Question(newId, text, options, correct, "Medium"));
                    Console.WriteLine("Question added successfully.");
                }
                else Console.WriteLine("Quiz not found.");
            }
        }

        // "UpdateProduct" - Updates a question
        public void UpdateProduct(List<Quiz> quizzes)
        {
            Console.WriteLine("\n--- Update Question ---");
            // Simplified for brevity: In a real app, you would select quiz -> select question -> edit
            Console.WriteLine("Feature implementation: Admin selects a question and edits text.");
        }

        // "RemoveProduct" - Removes a question
        public void RemoveProduct(List<Quiz> quizzes)
        {
            Console.WriteLine("\n--- Remove Question ---");
            // Implementation logic stub
            Console.WriteLine("Feature implementation: Admin selects a question ID to delete.");
        }

        // "ManageUsers" - Add/Remove Students
        public void ManageUsers(List<Student> students)
        {
            Console.WriteLine("\n--- Manage Users ---");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Remove Student");
            Console.Write("Choice: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Username: ");
                string u = Console.ReadLine();
                Console.Write("Password: ");
                string p = Console.ReadLine();
                int newId = students.Any() ? students.Max(s => s.ID) + 1 : 100;

                students.Add(new Student(newId, u, p, $"{u}@ulster.ac.uk", "active"));
                Console.WriteLine("Student added.");
            }
            else if (choice == "2")
            {
                Console.Write("Enter Student ID to remove: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    var s = students.Find(stu => stu.ID == id);
                    if (s != null)
                    {
                        students.Remove(s);
                        Console.WriteLine("Student removed.");
                    }
                    else Console.WriteLine("Student not found.");
                }
            }
        }
    }
}
