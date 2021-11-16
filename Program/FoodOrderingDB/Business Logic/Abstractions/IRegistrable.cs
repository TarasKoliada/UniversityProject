using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.Abstractions
{
    interface IRegistrable<T>
    {
        T Register();
        void SetInfoToDb(T entity);
    }
}
