using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UlsterQuizSystem;

namespace QuizSystemTests
{
    // Dummy Concrete Class made to allow for functionality of abstract User class to be tested
    internal class TestUser : User
    {
        // Default Constructor
        public TestUser()
            : base()
        {
            ID = 0;
        }

        // Parameterized Constructor
        public TestUser(int id, string username, string password, string email)
            : base(id, username, password, email, UserRole.User)
        {

        }
    }
}
