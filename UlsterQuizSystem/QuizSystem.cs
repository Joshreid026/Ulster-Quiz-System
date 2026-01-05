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
        // ==========================================
        // Central Data Store
        // ==========================================
        private QuizSystemData systemData;

        public QuizSystem()
        {
            systemData = new QuizSystemData();
            systemData.LoadSampleData();
        }

        // ==========================================
        // Setup Initial Data and Main Loop
        // ==========================================
        public void Run()
        {
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
            Console.Write("Username: "); string username = Console.ReadLine();
            Console.Write("Password: "); string password = Console.ReadLine();

            Admin admin = systemData.PerformAdminLogin(username, password);

            if (admin != null)
            {
                DisplayAdminDashboard(admin);
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
            Console.Write("Username: "); string username = Console.ReadLine();
            Console.Write("Password: "); string password = Console.ReadLine();

            Student student = systemData.PerformStudentLogin(username, password);

            if (student != null)
            {
                if (student.Status.ToLower() == "active")
                {
                    DisplayStudentDashboard(student);
                }
                else
                {
                    Console.WriteLine("Account is inactive.");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("User not found.");
                Console.ReadKey();
            }
        }

        private void DisplayAdminDashboard(Admin admin)
        {
            admin.LoginDate = DateTime.Now;
            bool active = true;

            while (active)
            {
                Console.Clear();
                Console.WriteLine($"--- ADMIN DASHBOARD ( logged in as: {admin.Username} ) ---");
                Console.WriteLine($"Last Login: {admin.LoginDate}");
                Console.WriteLine("1. Manage Quizzes & Questions");
                Console.WriteLine("2. Manage Users");
                Console.WriteLine("3. Manage Categories");
                Console.WriteLine("4. Reports");
                Console.WriteLine("5. Save Quizzes to CSV");
                Console.WriteLine("6. Logout");
                Console.Write("Select: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": admin.ManageQuizzesAndQuestions(systemData.Quizzes, systemData.Categories); break;
                    case "2": admin.ManageUsers(systemData.Students, systemData.Admins); break;
                    case "3": admin.ManageCategories(systemData.Categories, systemData.Quizzes); break;
                    case "4": admin.ViewSystemDataMenu(systemData.Quizzes, systemData.Categories, systemData.Students, systemData.Admins); break;
                    case "5": admin.SaveToCSV(systemData.Quizzes); break;
                    case "6": admin.Logout(); active = false; break;
                    default: Console.WriteLine("Invalid selection."); break;
                }
            }
        }

        public void DisplayStudentDashboard(Student student)
        {
            bool active = true;
            while (active)
            {
                Console.Clear();
                Console.WriteLine($"--- STUDENT MENU ( Logged in as: {student.Username} ) ---");
                Console.WriteLine("1. Play Quiz (Filter by Category)");
                Console.WriteLine("2. Logout");
                Console.Write("Select: ");

                string choice = Console.ReadLine();
                if (choice == "1") student.FilterAndPlay(systemData.Quizzes, systemData.Categories);
                else if (choice == "2") { student.Logout(); active = false; }
            }
        }

        // ==========================================
        // Entry Point
        // ==========================================
        static void Main(string[] args)
        {
            QuizSystem quizSystem = new QuizSystem();
            quizSystem.Run();
        }
    }
}

// Class created by Josh Reid
// Citations: Intellisense code suggestions from Visual Studio 2022
// general syntax and code solutions found on StackOverflow and google searches gemini.
