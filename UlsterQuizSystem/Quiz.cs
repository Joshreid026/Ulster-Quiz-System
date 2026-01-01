using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlsterQuizSystem
{
    public class Quiz
    {
        public int QuizID { get; private set; }
        public string QuizTitle { get; set; }
        public string QuizDescription { get; set; }
        public Category QuizCategory { get; set; }
        public List<Question> QuizQuestions { get; set; }
        public DateTime QuizDate { get; set; }

        // default constructor
        public Quiz()
        {
            QuizID = 0;
            QuizTitle = "";
            QuizDescription = "";
            QuizCategory = new Category();
            QuizQuestions = new List<Question>();
            QuizDate = DateTime.Now;
        }
        // parameterized constructor
        public Quiz(int id, string title, string desc, Category cat, DateTime date)
        {
            QuizID = id;
            QuizTitle = title;
            QuizDescription = desc;
            QuizCategory = cat;
            QuizDate = date;
            QuizQuestions = new List<Question>();
        }
    }
}

// Class created by Josh Reid
// Citations: Intellisense code suggestions from Visual Studio 2022
// general syntax and code solutions found on StackOverflow and google searches gemini.