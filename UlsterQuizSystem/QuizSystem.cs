using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var cat1 = new Category(1, "Programming", "Concepts of object-oriented programming and coding principles");
            var cat2 = new Category(2, "Data Structures", "Arrays, lists, stacks, queues, trees, and their applications");
            var cat3 = new Category(3, "Software Design", "Design patterns, architecture principles, and system modelling");
            var cat4 = new Category(4, "Web Development", "HTML, CSS, JavaScript, and client-server interactions");
            var cat5 = new Category(5, "Database Systems", "SQL queries, relational models, normalization, and transactions");
            var cat6 = new Category(6, "Cybersecurity Basics", "Encryption, authentication, and common security threats");
            var cat7 = new Category(7, "Computer Networks", "Protocols, IP addressing, routing, and network layers");
            _categories.Add(cat1);
            _categories.Add(cat2);
            _categories.Add(cat3);
            _categories.Add(cat4);
            _categories.Add(cat5);
            _categories.Add(cat6);
            _categories.Add(cat7);

            // Users
            _admins.Add(new Admin(1, "admin", "admin123", "admin@ulster.ac.uk"));
            _students.Add(new Student(101, "student", "student123", "student@ulster.ac.uk", "active"));

            // Questions
            var qs = new List<Question>
            {
                new Question(1, "What does OOP stand for? ", new List<string>{"Object-Oriented Programming", "Operational Output Processing", "Open Order Protocol", "Overloaded Operator Procedure"}, "Object-Oriented Programming", "Easy"),
                new Question(2, "Which of the following is NOT a core principle of OOP?", new List<string>{"Encapsulation", "Polymorphism", "Abstraction", "Compilation"}, "Compilation", "Easy"),
                new Question(3, "What is encapsulation in object-oriented programming?", new List<string>{"Binding data and methods", "Inheritance", "Overloading", "Creating objects"}, "Binding data and methods", "Medium"),
                new Question(4, "Which keyword is used in C# to inherit a class?", new List<string>{"extends", "inherits", ":", "base"}, ":", "Medium"),
                new Question(5, "What is the purpose of a constructor in a class?", new List<string>{"To destroy objects", "To initialize objects", "To inherit methods", "To override properties"}, "To initialize objects", "Easy"),
                new Question(6, "Which concept allows multiple methods with the same name but different parameters?", new List<string>{"Inheritance", "Polymorphism", "Overloading", "Encapsulation"}, "Overloading", "Medium"),
                new Question(7, "What is the base class for all classes in C#?", new List<string>{"System.Object", "BaseClass", "RootClass", "MainClass"}, "System.Object", "Hard"),
                new Question(8, "What is the difference between a class and an object?", new List<string>{"Class is an instance, object is a blueprint", "Class is a blueprint, object is an instance", "They are the same", "Object inherits class"}, "Class is a blueprint, object is an instance", "Medium"),
                new Question(9, "Which access modifier makes a member accessible only within its own class?", new List<string>{"public", "private", "protected", "internal"}, "private", "Easy"),
                new Question(10, "What is polymorphism in OOP?", new List<string>{"Ability to hide data", "Ability to inherit methods", "Ability to take many forms", "Ability to override constructors"}, "Ability to take many forms", "Medium")
            };

            // Quizzes
            var quiz1 = new Quiz(1, "OOP Fundamentals", "Covers basics of object-oriented programming.", cat1, new DateTime(2025,09,01));
            quiz1.QuizQuestions = qs;

            var quiz2 = new Quiz(2, "Data Structures", "Focuses on arrays, lists, stacks, queues, trees, and their applications.", cat2, new DateTime(2025, 09, 01));
            var quiz3 = new Quiz(3, "Software Design", "Includes design patterns, architecture principles, and system modelling.", cat3, new DateTime(2025, 09, 01));
            var quiz4 = new Quiz(4, "Web Development", "HTML, CSS, JavaScript and client-server interactions", cat4, new DateTime(2025, 09, 07));
            var quiz5 = new Quiz(5, "Database Systems", "SQL queries, relational models, normalization, and transactions.", cat5, new DateTime(2025, 09, 07));
            var quiz6 = new Quiz(6, "Cybersecurity Basics", "Encryption, authentication, and common security threats.", cat6, new DateTime(2025, 09, 11));
            var quiz7 = new Quiz(7, "Computer Networks", "Protocols, IP addressing, routing, and network layers.", cat7, new DateTime(2025, 09, 13));

            _quizzes.Add(quiz1);
            _quizzes.Add(quiz2);
            _quizzes.Add(quiz3);
            _quizzes.Add(quiz4);
            _quizzes.Add(quiz5);
            _quizzes.Add(quiz6);
            _quizzes.Add(quiz7);



        }
    }
}
