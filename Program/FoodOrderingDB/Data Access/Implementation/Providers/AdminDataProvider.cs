using FoodOrderingDB.Data_Access.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.Data_Access.Implementation
{
    class AdminDataProvider : IDataProvider<Administrator>
    {
        OrderingContext context = new OrderingContext();
        public DbSet<Administrator> GetContext()
        {
            return context.Administrator;
        }

        public Administrator GetEntity()
        {
            throw new NotImplementedException();
        }

        public void SaveData()
        {
            context.SaveChanges();
        }
    }
}
