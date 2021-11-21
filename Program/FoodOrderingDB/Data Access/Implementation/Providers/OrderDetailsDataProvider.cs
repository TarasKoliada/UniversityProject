using FoodOrderingDB.Data_Access.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.Data_Access.Implementation.Providers
{
    class OrderDetailsDataProvider : IDataProvider<OrderDetails>
    {
        OrderDetails _details;
        OrderingContext context = new OrderingContext();
        public OrderDetailsDataProvider(OrderDetails details)
        {
            _details = details;
        }
        public DbSet<OrderDetails> GetContext()
        {
            return context.Order_details;
        }

        public OrderDetails GetEntity()
        {
            return _details;
        }

        public void SaveData()
        {
            context.SaveChanges();
        }
    }
}
