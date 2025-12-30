using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlsterQuizSystem
{
    public class QuizSystem
    {
        // Master Lists
        private List<Admin> adminUsers;
        private List<Student> studentUsers;
        private List<Quiz> quizzes;
        private List<Category> categories;

        public QuizSystem()
        {
            // Initialize lists using the static methods we created in Admin/Student
            adminUsers = Admin.CreateSampleAdmins();
            studentUsers = Student.CreateSampleCustomers();
            categories = new List<Category>();
            quizzes = new List<Quiz>();
        }

        public void Run()
        {
            LoadQuizData(); // Load categories and quizzes

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Quiz System Main Menu ===");
                Console.WriteLine("1. Admin Login");
                Console.WriteLine("2. Student Login");
                Console.WriteLine("3. Exit");
                Console.Write("Selection: ");
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            PerformAdminLogin();
                            break;
                        case "2":
                            PerformStudentLogin();
                            break;
                        case "3":
                            return;
                        default:
                            Console.WriteLine("Invalid Selection.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"System Error: {ex.Message}");
                    Console.ReadKey();
                }
            }
        }

        private void PerformAdminLogin()
        {
            Console.Write("Username: ");
            string u = Console.ReadLine();
            Console.Write("Password: ");
            string p = Console.ReadLine();

            // Find Admin
            Admin admin = adminUsers.Find(a => a.Username == u && a.Password == p);

            if (admin != null)
            {
                // Pass control to the Admin Menu (handled here to route between methods)
                ShowAdminDashboard(admin);
            }
            else
            {
                Console.WriteLine("Invalid Admin Credentials.");
                Console.ReadKey();
            }
        }

        private void ShowAdminDashboard(Admin admin)
        {
            bool loggedIn = true;
            while (loggedIn)
            {
                Console.Clear();
                Console.WriteLine($"--- Admin Panel: {admin.Username} ---");
                Console.WriteLine("1. Add Question (AddProduct)");
                Console.WriteLine("2. Update Question (UpdateProduct)");
                Console.WriteLine("3. Manage Students (ManageUsers)");
                Console.WriteLine("4. Save to CSV");
                Console.WriteLine("5. Logout");
                Console.Write("Select: ");

                string choice = Console.ReadLine();

                // Here we call the methods WE MOVED into the Admin class.
                // We pass the "quizzes" or "studentUsers" lists so the Admin can edit them.
                switch (choice)
                {
                    case "1":
                        admin.AddProduct(quizzes);
                        break;
                    case "2":
                        admin.UpdateProduct(quizzes);
                        break;
                    case "3":
                        admin.ManageUsers(studentUsers);
                        break;
                    case "4":
                        SaveQuizToCSV();
                        break;
                    case "5":
                        admin.Logout();
                        loggedIn = false;
                        break;
                }
                if (loggedIn) Console.ReadKey(); // Pause to let user read output
            }
        }

        private void PerformStudentLogin()
        {
            // Direct login for simplicity or ask for credentials
            Console.WriteLine("\n--- Student Login ---");
            Console.Write("Username: ");
            string u = Console.ReadLine();

            Student student = studentUsers.Find(s => s.Username == u);
            if (student != null)
            {
                // Call the method WE MOVED into the Student class
                student.DisplayCustomerMenu(quizzes, categories);
            }
            else
            {
                Console.WriteLine("Student not found.");
                Console.ReadKey();
            }
        }

        private void LoadQuizData()
        {
            // Load Categories
            categories.Add(new Category(1, "Programming", "OOP Concepts"));
            categories.Add(new Category(2, "Data Structures", "Lists & Arrays"));

            // Load Quizzes
            var oopQuestions = new List<Question> {
                new Question(1, "What is OOP?", new List<string>{"Object Oriented Programming", "Other"}, "Object Oriented Programming", "Easy"),
                new Question(2, "C# inherits using?", new List<string>{":", "extends"}, ":", "Medium")
            };

            Quiz q1 = new Quiz(1, "OOP Fundamentals", "Basic OOP", categories[0], DateTime.Now);
            q1.QuizQuestions = oopQuestions;
            quizzes.Add(q1);
        }

        public void SaveQuizToCSV()
        {
            // Basic CSV save implementation
            try
            {
                using (StreamWriter sw = new StreamWriter("questions.csv"))
                {
                    sw.WriteLine("QuizID,QuestionID,Text,CorrectAnswer");
                    foreach (var q in quizzes)
                    {
                        foreach (var quest in q.QuizQuestions)
                        {
                            sw.WriteLine($"{q.QuizID},{quest.QuestionID},{quest.QuestionText},{quest.CorrectAnswer}");
                        }
                    }
                }
                Console.WriteLine("Saved to questions.csv");
            }
            catch (Exception ex) { Console.WriteLine("Error saving file: " + ex.Message); }
        }
    }
}
