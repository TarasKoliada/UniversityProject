using FoodOrderingDB.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.Repositories.Implemented
{
    class SiteRepository : IRepository<Site>
    {
        private readonly OrderingContext _orderingContext;
        public SiteRepository(OrderingContext context)
        {
            _orderingContext = context;
        }
        public void Add(Site entity)
        {
            if (entity != null)
            {
                _orderingContext.Site_Info.Add(entity);
                _orderingContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var site = Get(id);
            if (site != null)
            {
                _orderingContext.Site_Info.Remove(site);
                _orderingContext.SaveChanges();
            }
        }

        public Site Get(int id)
             => _orderingContext.Site_Info.FirstOrDefault(s => s.Id == id);

        public List<Site> GetAll()
            => _orderingContext.Site_Info.ToList();
    }
}
