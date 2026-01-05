using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlsterQuizSystem
{
    public class Question
    {
        // ==========================================
        // Fields, Properties, Get/Sets, Constructors
        // ==========================================
        public int QuestionID { get; private set; }
        public string QuestionText { get; set; }
        public List<string> QuestionOptions { get; set; }
        public string QuestionCorrectAnswer { get; set; }
        public string QuestionDifficultyLevel { get; set; }
        private static int questionNextID = 1;

        // Default Constructor
        public Question()
        {
            QuestionID = questionNextID;
            QuestionText = "";
            QuestionOptions = new List<string>();
            QuestionCorrectAnswer = "";
            QuestionDifficultyLevel = "";
            questionNextID++;
        }
    
        // Parameterized Constructor
        public Question(string text, List<string> options, string ans, string diff)
        {
            QuestionID = questionNextID;
            QuestionText = text;
            QuestionOptions = options;
            QuestionCorrectAnswer = ans;
            QuestionDifficultyLevel = diff;
            questionNextID++;
        }

        // ==========================================
        // Method to Reset ID Counter
        // ==========================================
        public static void ResetQuestionNextIDCounter()
        {
            questionNextID = 1;
        }
    }
}

// Class created by Josh Reid
// Citations: Intellisense code suggestions from Visual Studio 2022
// general syntax and code solutions found on StackOverflow and google searches gemini.