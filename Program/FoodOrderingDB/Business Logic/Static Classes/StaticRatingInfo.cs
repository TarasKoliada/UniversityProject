using FoodOrderingDB.Data_Access.Implementation;
using FoodOrderingDB.Data_Access.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.Business_Logic.Static_Classes
{
    class StaticRatingInfo
    {
        public static void GetRating(Site site)
        {
            IDataProvider<Site> provider = new SiteDataProvider();
            foreach (var item in provider.GetContext())
            {
                if (item.Id == site.Id)
                {
                    foreach (var menuType in item.MenuType)
                    {
                        foreach (var dish in menuType.Dish)
                        {
                            foreach (var rating in dish.Rating)
                            {
                                string remarks = Encoding.UTF8.GetString(rating.Remarks);
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"Rating ID: {rating.Id}");
                                Console.ResetColor();
                                Console.WriteLine($"Customer ID: {rating.CustomerId} | Username: {rating.Customer.Username}");
                                Console.WriteLine($"   Dish ID: {rating.DishId}");
                                Console.WriteLine($"   Menu type: {rating.Dish.MenuType.Name}");
                                Console.WriteLine($"   Dish name: {rating.Dish.Name}");
                                Console.WriteLine($"Created: {rating.CreatedDate}");
                                Console.WriteLine($"Score: {rating.Score}");
                                Console.WriteLine($"Remarks: {remarks}\n");
                            }    
                        }
                    }
                }
            }
        }
    }
}
