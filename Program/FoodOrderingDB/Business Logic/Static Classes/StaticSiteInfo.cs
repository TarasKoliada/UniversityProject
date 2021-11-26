using FoodOrderingDB.Repositories;
using System;

namespace FoodOrderingDB.Business_Logic.Static_Classes
{
    public static class StaticSiteInfo
    {
        private static readonly UnitOfWork _unitOfWork = new UnitOfWork();
        public static Site ChooseSite()
        {

            int iterator = 0;
            int siteId;

            Console.WriteLine("\nChoose site to visit: \n");

            foreach (var sites in _unitOfWork.Sites.GetAll())
            {
                Console.Write($"  {++iterator})");
                WriteMessage.Write($" {sites.Name}", ConsoleColor.Blue, false);
            }

            Console.Write("\nYour choise: ");
            var parsed = int.TryParse(Console.ReadLine(), out siteId);
            Console.Clear();

            if (!parsed)
            {
                Console.WriteLine("You can only enter site Id's");
                ChooseSite();
            }
            var site = _unitOfWork.Sites.Get(siteId);

            if (site == null)
            {
                Console.WriteLine("\nThere is no such site in the list, choose other");
                ChooseSite();
            }

            Console.Write($"\nGreat choise! Welcome to ");
            WriteMessage.Write($"{site.Name}", ConsoleColor.Blue, false);
            return site;

        }


        public static void ShowSiteMenu(int id)
        {
            var site = _unitOfWork.Sites.Get(id);

            var menuCounter = 0;
            var dishCounter = 0;
            Console.WriteLine();
            foreach (var menuTypes in site.MenuType)
            {
                Console.WriteLine($"\n {++menuCounter}) {menuTypes.Name}");
                foreach (var dishes in menuTypes.Dish)
                {
                    Console.Write($"    {++dishCounter}. ");
                    WriteMessage.Write($"{dishes.Name} ", ConsoleColor.Yellow, false);
                    Console.Write($" | Weight: {dishes.Weight} | ");
                    WriteMessage.Write($" Price: {dishes.Price}$", ConsoleColor.Green, false);
                }
            }
        }

        public static void GetInfo(int id)
        {
            var site = _unitOfWork.Sites.Get(id);

            Console.WriteLine($"\n  Id: {site.Id}");
            Console.WriteLine($"  Name: {site.Name}");
            Console.WriteLine($"  Description: {site.Description}");
            Console.WriteLine($"  Location: {site.LocationAdress}");
            Console.WriteLine($"  Contact info: {site.ContactInfo}");
            WriteMessage.Write("\n  Administrators: ", ConsoleColor.Yellow, false);
            foreach (var admin in site.Administrator)
            {
                Console.WriteLine($"  ID: {admin.Id} | Full Name: {admin.Name}  {admin.MiddleName}  {admin.Surname} |  Hotline: {admin.Contact}");
            }
        }

    }
}
