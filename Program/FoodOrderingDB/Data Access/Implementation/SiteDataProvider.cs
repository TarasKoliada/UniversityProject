using FoodOrderingDB.Data_Access.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.Data_Access.Implementation
{
    class SiteDataProvider : IDataProvider<Site>
    {
        OrderingContext context = new OrderingContext();
        public DbSet<Site> GetContext()
        {
            return context.Site_Info;
        }

        public Site GetEntity()
        {
            throw new NotImplementedException();
        }

        public void SaveData()
        {
            context.SaveChanges();
        }
    }
}
