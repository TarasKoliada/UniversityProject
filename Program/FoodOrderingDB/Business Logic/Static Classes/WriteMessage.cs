using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.Business_Logic.Static_Classes
{
    public static class WriteMessage
    {

        public static void Write(string msg, ConsoleColor color, bool clear = true)
        {
            if (clear == true)
            {
                Console.Clear();
            }
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
            
    }
}
