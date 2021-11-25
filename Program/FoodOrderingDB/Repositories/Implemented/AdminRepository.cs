using FoodOrderingDB.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.Repositories.Implemented
{
    class AdminRepository : IRepository<Administrator>
    {
        private readonly OrderingContext _orderingContext;
        public AdminRepository(OrderingContext context)
        {
            _orderingContext = context;
        }
        public void Add(Administrator entity)
        {
            if (entity != null)
            {
                _orderingContext.Administrator.Add(entity);
                _orderingContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var admin = Get(id);
            if (admin != null)
            {
                _orderingContext.Administrator.Remove(admin);
                _orderingContext.SaveChanges();
            }
        }

        public Administrator Get(int id)
            => _orderingContext.Administrator.FirstOrDefault(a => a.Id == id);

        public List<Administrator> GetAll()
            => _orderingContext.Administrator.ToList();
    }
}
