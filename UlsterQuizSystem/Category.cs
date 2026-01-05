using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlsterQuizSystem {

    public class Category
    {
        // ==========================================
        // Fields, Properties, Get/Sets, Constructors
        // ==========================================
        public int CategoryID { get; private set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        private static int categoryNextID = 1;

        // Default Constructor
        public Category()
        {
            CategoryID = categoryNextID;
            CategoryName = "Default";
            CategoryDescription = "Default Description";
            categoryNextID++;
        }

        // Parameterized Constructor
        public Category(string name, string desc)
        {
            CategoryID = categoryNextID;
            CategoryName = name;
            CategoryDescription = desc;
            categoryNextID++;
        }

        // ==========================================
        // Methods
        // ==========================================
        public static void ResetCategoryNextIDCounter()
        {
            categoryNextID = 1;
        }

        // Defines how Category is displayed
        public override string ToString() => $"{CategoryID}. {CategoryName} ({CategoryDescription})";

    }
}

// Class created by Josh Reid
// Citations: Intellisense code suggestions from Visual Studio 2022
// general syntax and code solutions found on StackOverflow and google searches gemini.