using FoodOrderingDB.Repositories.Abstraction;
using System.Collections.Generic;
using System.Linq;

namespace FoodOrderingDB.Repositories.Implemented
{
    class RatingRepository : IRepository<Rating>
    {
        private readonly OrderingContext _orderingContext;
        public RatingRepository(OrderingContext context)
        {
            _orderingContext = context;
        }
        public void Add(Rating entity)
        {
            if (entity != null)
            {
                _orderingContext.Rating.Add(entity);
                _orderingContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var rating = Get(id);
            if (rating != null)
            {
                _orderingContext.Rating.Remove(rating);
                _orderingContext.SaveChanges();
            }
        }

        public Rating Get(int id)
            => _orderingContext.Rating.FirstOrDefault(r => r.Id == id);

        public List<Rating> GetAll()
            => _orderingContext.Rating.ToList();
    }
}
