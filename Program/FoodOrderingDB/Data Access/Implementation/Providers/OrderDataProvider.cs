using FoodOrderingDB.Data_Access.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.Data_Access.Implementation
{
    class OrderDataProvider : IDataProvider<Order>
    {
        OrderingContext context = new OrderingContext();
        Order _order;
        public OrderDataProvider(Order order)
        {
            _order = order;
        }
        public DbSet<Order> GetContext()
        {
            return context.Orders;
        }

        public Order GetEntity()
        {
            return _order;
        }

        public void SaveData()
        {
            context.SaveChanges();
        }
    }
}
