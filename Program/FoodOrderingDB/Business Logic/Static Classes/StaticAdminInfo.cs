using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.Business_Logic.Static_Classes
{
    class StaticAdminInfo
    {
        public static void GetFullInfo(Administrator admin)
        {
            Console.WriteLine("Your profile Info: ");
            Console.WriteLine($"\n Id: {admin.Id}");
            Console.WriteLine($" Name: {admin.Name}");
            Console.WriteLine($" Surname: {admin.Surname}");
            Console.WriteLine($" Middle name: {admin.MiddleName}");
            Console.WriteLine($" Phone number: {admin.Contact}");
            Console.WriteLine($" Related site - Id: {admin.SiteId} | Name: {admin.Site.Name}");
            Console.WriteLine($" Preferences: {admin.Preferences}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"   Login: ");
            Console.ResetColor();
            Console.WriteLine($"{admin.Login}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"   Password: ");
            Console.ResetColor();
            Console.WriteLine($"{admin.Password}\n\n");
        }
    }
}
