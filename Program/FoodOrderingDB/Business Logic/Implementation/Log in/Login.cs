using System;

namespace FoodOrderingDB.Business_Logic.Static_Classes
{
    public static class Login
    {
        public static string Generate(string name, string surname)
        {
            var random = new Random();
            string stringNum = Convert.ToString(random.Next(1, 20));
            return name + surname + stringNum;
        }
    }
}
