using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlsterQuizSystem
{
    public class User
    {
        // Encapsulated Fields
        private int _id;
        private string _username;
        private string _password;
        private string _email;
        private string _role;

        // Properties
        public int ID
        {
            get { return _id; }
            private set { _id = value; } // ID should not change after creation
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Role
        {
            get { return _role; }
            protected set { _role = value; }
        }

        // Constructors
        public User() { }

        public User(int id, string username, string password, string email, string role)
        {
            ID = id;
            Username = username;
            Password = password;
            Email = email;
            Role = role;
        }

        // Shared Methods
        public void UpdateProfile(string newEmail, string newPassword)
        {
            Email = newEmail;
            Password = newPassword;
            Console.WriteLine("Profile updated successfully.");
        }

        public void Logout()
        {
            Console.WriteLine($"\nUser {Username} has logged out.");
        }
    }
}
