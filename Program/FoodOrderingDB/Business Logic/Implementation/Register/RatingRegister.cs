using FoodOrderingDB.Abstractions;
using FoodOrderingDB.Business_Logic.Static_Classes;
using System;
using System.Text;
using System.Collections.Generic;
using FoodOrderingDB.Repositories;

namespace FoodOrderingDB.Business_Logic.Implementation.Register
{
    class RatingRegister : IRegistrable<Rating>
    {
        private readonly UnitOfWork _unitOfWork;
        Customer _customer;
        Site _site;
        public RatingRegister(Customer customer, Site site)
        {
            _customer = customer;
            _site = site;
            _unitOfWork = new UnitOfWork();
        }
        public Rating Register()
        {
            var rating = new Rating();
            rating.CreatedDate = DateTime.Now;

            rating.CustomerId = _customer.Id;

            StaticSiteInfo.ShowSiteMenu(_site.Id);


            var dishes = GetSiteDishes();

            Console.Write("\n\nEnter the Dish id you want to rate: ");
            var parsed = int.TryParse(Console.ReadLine(), out int dishId);
            if (!parsed)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong input format");
                Console.ResetColor();
                Register();
            }

            try
            {
                rating.DishId = dishes[dishId - 1].Id;
            }
            catch (Exception)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There is no such Dish in the menu");
                Console.ResetColor();
                Register();
            }

            bool converted = false;
            do
            {
                Console.Write("Rate the dish from 1 to 5: ");
                parsed = int.TryParse(Console.ReadLine(), out int score);

                if (!parsed)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong input format");
                    Console.ResetColor();
                }

                if (score <= 5 && score > 0)
                {
                    rating.Score = score.ToString();
                    converted = true;
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You can enter numbers from 1 to 5");
                    Console.ResetColor();
                }
            } while (!parsed || !converted);

            Console.Write("\nAdd remarks? y/n : ");
            var setNotes = Console.ReadLine();

            if (setNotes == "y" || setNotes == "Y" || setNotes == "Yes" || setNotes == "YES")
            {
                Console.Write("   Write Notes you want to add: ");
                string notes = Console.ReadLine();
                rating.Remarks = Encoding.ASCII.GetBytes(notes);
            }

            SetInfoToDb(rating);
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" \nThank you for rating\n");
            Console.ResetColor();

            return rating;
        }

        private List<Dish> GetSiteDishes()
        {
            List<Dish> dishes = new List<Dish>();
            foreach (var menuType in _site.MenuType)
            {
                foreach (var dish in menuType.Dish)
                {
                    dishes.Add(dish);
                }
            }
            return dishes;
        }

        public void SetInfoToDb(Rating entity)
        {
            _unitOfWork.Ratings.Add(entity);
        }
    }
}
