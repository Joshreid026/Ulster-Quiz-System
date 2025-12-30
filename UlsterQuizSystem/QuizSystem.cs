using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlsterQuizSystem
{
    public class QuizSystem
    {
        // Central Data Store
        private List<Admin> _admins;
        private List<Student> _students;
        private List<Quiz> _quizzes;
        private List<Category> _categories;

        public QuizSystem()
        {
            _admins = new List<Admin>();
            _students = new List<Student>();
            _quizzes = new List<Quiz>();
            _categories = new List<Category>();
        }

        public void Run()
        {
            LoadSampleData(); // Setup initial data

            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== ULSTER UNIVERSITY QUIZ SYSTEM ===");
                Console.WriteLine("1. Admin Login");
                Console.WriteLine("2. Student Login");
                Console.WriteLine("3. Exit");
                Console.Write("Select: ");

                switch (Console.ReadLine())
                {
                    case "1": PerformAdminLogin(); break;
                    case "2": PerformStudentLogin(); break;
                    case "3": running = false; break;
                }
            }
        }

        private void PerformAdminLogin()
        {
            Console.Clear();
            Console.WriteLine("--- Admin Login ---");
            Console.Write("Username: "); string u = Console.ReadLine();
            Console.Write("Password: "); string p = Console.ReadLine();

            Admin admin = _admins.Find(a => a.Username == u && a.Password == p);

            if (admin != null)
            {
                // UPDATE: Now passing _admins as the 4th parameter
                admin.DisplayDashboard(_quizzes, _categories, _students, _admins);
            }
            else
            {
                Console.WriteLine("Invalid Admin Credentials.");
                Console.ReadKey();
            }
        }

        private void PerformStudentLogin()
        {
            Console.Clear();
            Console.WriteLine("--- Student Login ---");
            Console.Write("Username: "); string u = Console.ReadLine();

            Student student = _students.Find(s => s.Username == u);

            if (student != null)
            {
                if (student.Status.ToLower() == "active")
                {
                    // DELEGATE control to the Student class
                    student.DisplayStudentMenu(_quizzes, _categories);
                }
                else
                {
                    Console.WriteLine("Account Suspended/Inactive.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("User not found.");
                Console.ReadKey();
            }
        }

        private void LoadSampleData()
        {
            // Categories
            var cat1 = new Category(1, "Programming", "OOP and C#");
            var cat2 = new Category(2, "History", "World Events");
            _categories.Add(cat1);
            _categories.Add(cat2);

            // Users
            _admins.Add(new Admin(1, "admin", "admin123", "admin@ulster.ac.uk"));
            _students.Add(new Student(101, "student", "student123", "student@ulster.ac.uk", "active"));

            // Questions
            var qs = new List<Question>
            {
                new Question(1, "What is OOP?", new List<string>{"Object Oriented Programming","Food"}, "Object Oriented Programming", "Easy"),
                new Question(2, "Access modifier for private?", new List<string>{"public","private"}, "private", "Easy")
            };

            // Quizzes
            var quiz1 = new Quiz(1, "OOP Basics", "Intro Test", cat1, DateTime.Now);
            quiz1.QuizQuestions = qs;
            _quizzes.Add(quiz1);
        }
    }
}
