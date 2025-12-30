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
