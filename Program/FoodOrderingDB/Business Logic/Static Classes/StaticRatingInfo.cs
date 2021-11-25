using FoodOrderingDB.Repositories;
using System;
using System.Linq;
using System.Text;

namespace FoodOrderingDB.Business_Logic.Static_Classes
{
    class StaticRatingInfo
    {
        public static void GetRating(Site site)
        {
            var unit = new UnitOfWork();
            var ratings = unit.Ratings.GetAll().Where(r => r.Dish.MenuType.SiteId == site.Id);

            foreach (var rating in ratings)
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
