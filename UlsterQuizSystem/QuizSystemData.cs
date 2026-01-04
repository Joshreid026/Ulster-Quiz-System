using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlsterQuizSystem
{
    public class QuizSystemData
    {
        // ==========================================
        // Fields, Properties, Get/Sets, Constructors
        // ==========================================
        public List<Admin> Admins { get; private set; }
        public List<Student> Students { get; private set; }
        public List<Quiz> Quizzes { get; private set; }
        public List<Category> Categories { get; private set; }

        public QuizSystemData()
        {
            Admins = new List<Admin>();
            Students = new List<Student>();
            Quizzes = new List<Quiz>();
            Categories = new List<Category>();
        }

        // ==========================================
        // Methods
        // ==========================================
        public Admin PerformAdminLogin(string username, string password)
        {
            Admin admin = Admins.Find(a => a.Username == username && a.Password == password);

            if (admin != null)
            {
                return admin;
            }
            else
            {
                return null;
            }
        }

        public Student PerformStudentLogin(string username, string password)
        {
            Student student = Students.Find(s => s.Username == username && s.Password == password);

            if (student != null)
            {
                return student;
            }
            else
            {
                return null;
            }
        }

        public void LoadSampleData()
        {
            // ==========================================
            // Default Categories
            // ==========================================
            Categories.AddRange(new[]
            {
                new Category(1, "Programming", "Concepts of object-oriented programming and coding principles"),
                new Category(2, "Data Structures", "Arrays, lists, stacks, queues, trees, and their applications"),
                new Category(3, "Software Design", "Design patterns, architecture principles, and system modelling"),
                new Category(4, "Web Development", "HTML, CSS, JavaScript, and client-server interactions"),
                new Category(5, "Database Systems", "SQL queries, relational models, normalization, and transactions"),
                new Category(6, "Cybersecurity Basics", "Encryption, authentication, and common security threats"),
                new Category(7, "Computer Networks", "Protocols, IP addressing, routing, and network layers")
            });

            // ==========================================
            // Default Users
            // ==========================================

            Admins.AddRange(new[]
            {
                new Admin(1, "admin", "admin123", "admin@ulster.ac.uk"),
                new Admin(2, "admined", "admin123456", "admined@ulster.ac.uk")
            });

            Students.AddRange(new[]
            {
                new Student(101, "student", "student123", "student@ulster.ac.uk", "active"),
                new Student(102, "studented", "student123456", "studented@ulster.ac.uk", "active")
            });

            // ==========================================
            // Default Questions
            // ==========================================

            var oopQuestions = new List<Question>
            {
                new Question(1, "What does OOP stand for? ", new List<string>{"Object-Oriented Programming", "Operational Output Processing", "Open Order Protocol", "Overloaded Operator Procedure"}, "Object-Oriented Programming", "Easy"),
                new Question(2, "Which of the following is NOT a core principle of OOP?", new List<string>{"Encapsulation", "Polymorphism", "Abstraction", "Compilation"}, "Compilation", "Easy"),
                new Question(3, "What is encapsulation in object-oriented programming?", new List<string>{"Binding data and methods", "Inheritance", "Overloading", "Creating objects"}, "Binding data and methods", "Medium"),
                new Question(4, "Which keyword is used in C# to inherit a class?", new List<string>{"extends", "inherits", ":", "base"}, ":", "Medium"),
                new Question(5, "What is the purpose of a constructor in a class?", new List<string>{"To destroy objects", "To initialize objects", "To inherit methods", "To override properties"}, "To initialize objects", "Easy"),
                new Question(6, "Which concept allows multiple methods with the same name but different parameters?", new List<string>{"Inheritance", "Polymorphism", "Overloading", "Encapsulation"}, "Overloading", "Medium"),
                new Question(7, "What is the base class for all classes in C#?", new List<string>{"System.Object", "BaseClass", "RootClass", "MainClass"}, "System.Object", "Hard"),
                new Question(8, "What is the difference between a class and an object?", new List<string>{"Class is an instance and object is a blueprint", "Class is a blueprint and object is an instance", "They are the same", "Object inherits class"}, "Class is a blueprint and object is an instance", "Medium"),
                new Question(9, "Which access modifier makes a member accessible only within its own class?", new List<string>{"public", "private", "protected", "internal"}, "private", "Easy"),
                new Question(10, "What is polymorphism in OOP?", new List<string>{"Ability to hide data", "Ability to inherit methods", "Ability to take many forms", "Ability to override constructors"}, "Ability to take many forms", "Medium")
            };

            // ==========================================
            // Default Quizzes
            // ==========================================
            Quizzes.AddRange(new[]
            {
                new Quiz(1, "OOP Fundamentals", "Covers basics of object-oriented programming.", Categories[0], new DateTime(2025, 09, 01)),
                new Quiz(2, "Data Structures", "Focuses on arrays, lists, stacks, queues, trees, and their applications.", Categories[1], new DateTime(2025, 09, 01)),
                new Quiz(3, "Software Design", "Includes design patterns, architecture principles, and system modelling.", Categories[2], new DateTime(2025, 09, 01)),
                new Quiz(4, "Web Development", "HTML, CSS, JavaScript and client-server interactions", Categories[3], new DateTime(2025, 09, 07)),
                new Quiz(5, "Database Systems", "SQL queries, relational models, normalization, and transactions.", Categories[4], new DateTime(2025, 09, 07)),
                new Quiz(6, "Cybersecurity Basics", "Encryption, authentication, and common security threats.", Categories[5], new DateTime(2025, 09, 11)),
                new Quiz(7, "Computer Networks", "Protocols, IP addressing, routing, and network layers.", Categories[6], new DateTime(2025, 09, 13))
            });

            Quizzes[0].QuizQuestions = oopQuestions;
        }
    }
}
