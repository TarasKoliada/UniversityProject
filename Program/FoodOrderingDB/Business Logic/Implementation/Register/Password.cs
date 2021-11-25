using System;

namespace FoodOrderingDB.Business_Logic.Static_Classes
{
    public static class Password
    {
        private const string SYMBOLS = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890-@#$%&";
        private const int PASS_SIZE = 15;
        public static string Generate()
        {
            string result = "";

            var random = new Random();
            int length = SYMBOLS.Length;

            for (int i = 0; i < PASS_SIZE; i++)
            {
                result += SYMBOLS[random.Next(length)];
            }
            return result;
        }
    }
}
