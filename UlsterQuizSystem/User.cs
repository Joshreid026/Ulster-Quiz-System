using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlsterQuizSystem
{
    public abstract class User
    {
        // Encapsulation: Protected setters for ID/Role ensure they aren't changed externally
        public int ID { get; protected set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; protected set; }

        public User(int id, string username, string password, string email, string role)
        {
            ID = id;
            Username = username;
            Password = password;
            Email = email;
            Role = role;
        }

        // Shared behavior
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
