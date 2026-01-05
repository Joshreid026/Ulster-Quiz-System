using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlsterQuizSystem
{

    public enum UserRole
    {
        Admin,
        User,
        Student
    }

    public abstract class User
    {
        // ===================================================================
        // Protected Setters for ID/Role ensure they aren't changed externally
        // ===================================================================
        public int ID { get; protected set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        protected static int nextID = 1;

        // Default Constructor
        public User()
        {
            ID = nextID;
            Username = "default";
            Password = "password";
            Email = "email@mail.com";
            Role = UserRole.User;
            nextID++;
        }

        // Parameterized Constructor
        public User(string username, string password, string email, UserRole role)
        {
            ID = nextID;
            Username = username;
            Password = password;
            Email = email;
            Role = role;
            nextID++;
        }

        // ==========================================
        // Shared Behaviour for subclasses
        // ==========================================
        public static void ResetNextIDCounter()
        {
            nextID = 1;
        }

        public virtual void Logout()
        {
            Console.WriteLine($"\nUser {Username} logged out.");
        }

        public override string ToString()
        {
            return $"ID: {ID} | User: {Username} | Role: {Role}";
        }
    }
}

// Class created by Josh Reid
// Citations: Intellisense code suggestions from Visual Studio 2022
// general syntax and code solutions found on StackOverflow and google searches gemini.