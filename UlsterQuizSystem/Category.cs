using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlsterQuizSystem {

    public class Category
{
    public int CategoryID { get; private set; }
    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }

    public Category(int id, string name, string desc)
    {
        CategoryID = id;
        CategoryName = name;
        CategoryDescription = desc;
    }

    public override string ToString() => $"{CategoryID}. {CategoryName} ({CategoryDescription})";
    }

}
