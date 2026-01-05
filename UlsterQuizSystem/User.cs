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
        // Protected Setters for Role ensure they aren't changed externally
        // ===================================================================
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        protected static int nextID = 1;

        // Default Constructor
        public User()
        {
            Username = "default";
            Password = "password";
            Email = "email@mail.com";
            Role = UserRole.User;
        }

        // Parameterized Constructor
        public User(string username, string password, string email, UserRole role)
        {
            Username = username;
            Password = password;
            Email = email;
            Role = role;
        }

        // ==========================================
        // Shared Behaviour for subclasses
        // ==========================================
        public virtual void Logout()
        {
            Console.WriteLine($"\nUser {Username} logged out.");
        }

        public override string ToString()
        {
            return $"User: {Username} | Role: {Role}";
        }
    }
}

// Class created by Josh Reid
// Citations: Intellisense code suggestions from Visual Studio 2022
// general syntax and code solutions found on StackOverflow and google searches gemini.