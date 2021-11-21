using FoodOrderingDB.Data_Access.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.Data_Access.Implementation.Providers
{
    class PaymentDataProvider : IDataProvider<Payment>
    {
        OrderingContext context = new OrderingContext();
        Payment _payment;
        public PaymentDataProvider(Payment payment)
        {
            _payment = payment;
        }
        public DbSet<Payment> GetContext()
        {
            return context.Payment;
        }

        public Payment GetEntity()
        {
            return _payment;
        }

        public void SaveData()
        {
            context.SaveChanges();
        }
    }
}
