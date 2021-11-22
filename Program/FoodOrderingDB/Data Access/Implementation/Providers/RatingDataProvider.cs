using FoodOrderingDB.Data_Access.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.Data_Access.Implementation.Providers
{
    class RatingDataProvider : IDataProvider<Rating>
    {
        OrderingContext context = new OrderingContext();
        Rating _rating;
        public RatingDataProvider(Rating rating)
        {
            _rating = rating;
        }
        public DbSet<Rating> GetContext()
        {
            return context.Rating;
        }

        public Rating GetEntity()
        {
            return _rating;
        }

        public void SaveData()
        {
            context.SaveChanges();
        }
    }
}
