using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.Abstractions
{
    interface IRegistrable
    {
        void Register();
        void SetInfoToDb<T>(T entity);
    }
}
