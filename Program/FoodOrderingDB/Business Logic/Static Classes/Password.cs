using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.Business_Logic.Static_Classes
{
    public static class Password
    {
        public static string Generate()
        {
            string symbols = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890-@#$%&";
            int passwordSize = 15;
            string result = "";

            var random = new Random();
            int length = symbols.Length;

            for (int i = 0; i < passwordSize; i++)
            {
                result += symbols[random.Next(length)];
            }
            return result;
        }
    }
}
