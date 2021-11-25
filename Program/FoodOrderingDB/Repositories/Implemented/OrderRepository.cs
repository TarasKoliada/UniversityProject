using FoodOrderingDB.Repositories.Abstraction;
using System.Collections.Generic;
using System.Linq;

namespace FoodOrderingDB.Repositories.Implemented
{
    class OrderRepository : IRepository<Order>
    {
        private readonly OrderingContext _orderingContext;
        public OrderRepository(OrderingContext context)
        {
            _orderingContext = context;
        }
        public void Add(Order entity)
        {
            if (entity != null)
            {
                _orderingContext.Orders.Add(entity);
                _orderingContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var order = Get(id);
            if (order != null)
            {
                _orderingContext.Orders.Remove(order);
                _orderingContext.SaveChanges();
            }
        }

        public void ChangeStatus(bool statusState, int entityId)
        {
            var order = Get(entityId);
            if (order != null)
            {
                order.Status = statusState;
                _orderingContext.SaveChanges();
            }
        }

        public Order Get(int id)
            => _orderingContext.Orders.FirstOrDefault(o => o.Id == id);

        public List<Order> GetAll()
            => _orderingContext.Orders.ToList();
    }
}
