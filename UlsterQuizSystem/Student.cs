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
            : base(id, username, password, email, UserRole.Student)
        {
            Status = status;
        }

        // ==========================================
        // Entry point for student menu
        // ==========================================
        public void DisplayStudentMenu(List<Quiz> quizzes, List<Category> categories)
        {
            bool active = true;
            while (active)
            {
                Console.Clear();
                Console.WriteLine($"--- STUDENT MENU ( Logged in as: {Username} ) ---");
                Console.WriteLine("1. Play Quiz (Filter by Category)");
                Console.WriteLine("2. Logout");
                Console.Write("Select: ");

                string choice = Console.ReadLine();
                if (choice == "1") FilterAndPlay(quizzes, categories);
                else if (choice == "2") { Logout(); active = false; }
            }
        }

        private void FilterAndPlay(List<Quiz> quizzes, List<Category> categories)
        {
            Console.Clear();
            Console.WriteLine("--- Select Category ---");
            foreach (var c in categories) Console.WriteLine(c.ToString());

            Console.Write("Enter Category ID: ");
            if (int.TryParse(Console.ReadLine(), out int catId))
            {
                var filteredQuizzes = quizzes.Where(q => q.QuizCategory.CategoryID == catId).ToList();

                if (!filteredQuizzes.Any())
                {
                    Console.WriteLine("No quizzes available in this category.");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("\n--- Available Quizzes ---");
                foreach (var q in filteredQuizzes) Console.WriteLine($"{q.QuizID}. {q.QuizTitle}");

                Console.Write("Enter Quiz ID to play: ");
                if (int.TryParse(Console.ReadLine(), out int qId))
                {
                    var selectedQuiz = filteredQuizzes.Find(q => q.QuizID == qId);
                    if (selectedQuiz != null) PlayGameLoop(selectedQuiz);
                    else Console.WriteLine("Invalid Quiz ID.");
                }
            }
            else Console.WriteLine("Invalid Input.");

            Console.ReadKey();
        }

        private void PlayGameLoop(Quiz quiz)
        {
            if (quiz.QuizQuestions.Count == 0)
            {
                Console.WriteLine("This quiz has no questions yet!");
                return;
            }

            int score = 0;
            Console.WriteLine($"\nStarting Quiz: {quiz.QuizTitle}");

            foreach (var q in quiz.QuizQuestions)
            {
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine($"Q: {q.QuestionText}");
                for (int i = 0; i < q.QuestionOptions.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {q.QuestionOptions[i]}");
                }

                Console.Write("Type the answer text exactly: ");
                string answer = Console.ReadLine();

                if (string.Equals(answer.Trim(), q.QuestionCorrectAnswer.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("CORRECT!");
                    score++;
                }
                else
                {
                    Console.WriteLine($"WRONG! The correct answer was: {q.QuestionCorrectAnswer}");
                }
            }

            Console.WriteLine($"\n*** QUIZ FINISHED ***");
            Console.WriteLine($"You scored {score} out of {quiz.QuizQuestions.Count}");
        }

        public override string ToString()
        {
            return base.ToString() + $" | Status: {Status}";
        }
    }
}

// Class created by Josh Reid
// Citations: Intellisense code suggestions from Visual Studio 2022
// general syntax and code solutions found on StackOverflow and google searches gemini.
