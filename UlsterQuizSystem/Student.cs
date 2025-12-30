using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlsterQuizSystem
{
    public class Student : User
    {
        public string Status { get; set; }

        public Student(int id, string username, string password, string email, string status)
            : base(id, username, password, email, "Student")
        {
            Status = status;
        }

        // Static factory method
        public static List<Student> CreateSampleCustomers()
        {
            return new List<Student>
            {
                new Student(101, "student", "student123", "student@ulster.ac.uk", "active"),
                new Student(102, "jane", "jane123", "jane@ulster.ac.uk", "active")
            };
        }

        // The main menu logic for the student
        public void DisplayCustomerMenu(List<Quiz> quizzes, List<Category> categories)
        {
            bool active = true;
            while (active)
            {
                Console.Clear();
                Console.WriteLine($"--- Student Dashboard: {Username} ---");
                Console.WriteLine("1. Play Quiz");
                Console.WriteLine("2. Logout");
                Console.Write("Select: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    SelectAndPlayQuiz(quizzes, categories);
                }
                else if (choice == "2")
                {
                    Logout();
                    active = false;
                }
            }
        }

        // Private helper method just for the Student class
        private void SelectAndPlayQuiz(List<Quiz> quizzes, List<Category> categories)
        {
            Console.WriteLine("\nAvailable Categories:");
            foreach (var c in categories) Console.WriteLine($"{c.CategoryID}. {c.CategoryName}");

            Console.Write("Enter Category ID: ");
            if (int.TryParse(Console.ReadLine(), out int cId))
            {
                var filtered = quizzes.Where(q => q.QuizCategory.CategoryID == cId).ToList();
                if (filtered.Count == 0)
                {
                    Console.WriteLine("No quizzes in this category.");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("Select Quiz:");
                foreach (var q in filtered) Console.WriteLine($"{q.QuizID}. {q.QuizTitle}");

                if (int.TryParse(Console.ReadLine(), out int qId))
                {
                    var quiz = filtered.Find(q => q.QuizID == qId);
                    if (quiz != null) Play(quiz);
                }
            }
            else
            {
                Console.WriteLine("Invalid Input.");
                Console.ReadKey();
            }
        }

        private void Play(Quiz quiz)
        {
            int score = 0;
            Console.WriteLine($"\nStarting {quiz.QuizTitle}...");

            foreach (var q in quiz.QuizQuestions)
            {
                Console.WriteLine($"\n{q.QuestionText}");
                for (int i = 0; i < q.QuestionOptions.Count; i++)
                    Console.WriteLine($"{i + 1}. {q.QuestionOptions[i]}");

                Console.Write("Answer (type option text): ");
                string ans = Console.ReadLine();
                if (ans.Trim().Equals(q.CorrectAnswer.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Correct!");
                    score++;
                }
                else Console.WriteLine($"Wrong. Correct: {q.CorrectAnswer}");
            }
            Console.WriteLine($"\nQuiz Over. Score: {score}/{quiz.QuizQuestions.Count}");
            Console.ReadKey();
        }
    }
}
