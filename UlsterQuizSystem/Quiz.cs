using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlsterQuizSystem
{
    public class Quiz
    {
        // ==========================================
        // Fields, Properties, Get/Sets, Constructors
        // ==========================================
        public int QuizID { get; private set; }
        public string QuizTitle { get; set; }
        public string QuizDescription { get; set; }
        public Category QuizCategory { get; set; }
        public List<Question> QuizQuestions { get; set; }
        public DateTime QuizDate { get; set; }
        private static int quizNextID = 1;

        // Default Constructor
        public Quiz()
        {
            QuizID = quizNextID;
            QuizTitle = "";
            QuizDescription = "";
            QuizCategory = new Category();
            QuizQuestions = new List<Question>();
            QuizDate = DateTime.MinValue;
            quizNextID++;
        }

        // Parameterized Constructor
        public Quiz(string title, string desc, Category cat, DateTime date)
        {
            QuizID = quizNextID;
            QuizTitle = title;
            QuizDescription = desc;
            QuizCategory = cat;
            QuizDate = date;
            QuizQuestions = new List<Question>();
            quizNextID++;
        }

        // ==========================================
        // Methods
        // ==========================================
        public static void ResetQuizNextIDCounter()
        {
            quizNextID = 1;
        }
    }
}

// Class created by Josh Reid
// Citations: Intellisense code suggestions from Visual Studio 2022
// general syntax and code solutions found on StackOverflow and google searches gemini.