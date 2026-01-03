using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlsterQuizSystem
{
    public class Question
    {
        public int QuestionID { get; private set; }
        public string QuestionText { get; set; }
        public List<string> QuestionOptions { get; set; }
        public string QuestionCorrectAnswer { get; set; }
        public string QuestionDifficultyLevel { get; set; }

        // Default Constructor
        public Question()
        {
            QuestionID = 0;
            QuestionText = "";
            QuestionOptions = new List<string>();
            QuestionCorrectAnswer = "";
            QuestionDifficultyLevel = "";
        }
    
        // Parameterized Constructor
        public Question(int id, string text, List<string> options, string ans, string diff)
        {
            QuestionID = id;
            QuestionText = text;
            QuestionOptions = options;
            QuestionCorrectAnswer = ans;
            QuestionDifficultyLevel = diff;
        }
    }
}

// Class created by Josh Reid
// Citations: Intellisense code suggestions from Visual Studio 2022
// general syntax and code solutions found on StackOverflow and google searches gemini.