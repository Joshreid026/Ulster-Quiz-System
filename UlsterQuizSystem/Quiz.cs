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