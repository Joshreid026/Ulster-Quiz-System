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
                admin.DisplayAdminDashboard(systemData, admin);
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
                    student.DisplayStudentDashboard(systemData, student);
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
