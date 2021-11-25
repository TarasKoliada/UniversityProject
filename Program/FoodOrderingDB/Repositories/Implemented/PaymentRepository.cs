using FoodOrderingDB.Repositories.Abstraction;
using System.Collections.Generic;
using System.Linq;

namespace FoodOrderingDB.Repositories.Implemented
{
    class PaymentRepository : IRepository<Payment>
    {
        private readonly OrderingContext _orderingContext;
        public PaymentRepository(OrderingContext context)
        {
            _orderingContext = context;
        }
        public void Add(Payment entity)
        {
            if (entity != null)
            {
                _orderingContext.Payment.Add(entity);
                _orderingContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var payment = Get(id);
            if (payment != null)
            {
                _orderingContext.Payment.Remove(payment);
                _orderingContext.SaveChanges();
            }
        }

        public Payment Get(int id)
             => _orderingContext.Payment.FirstOrDefault(p => p.Id == id);

        public List<Payment> GetAll()
            => _orderingContext.Payment.ToList();
    }
}
