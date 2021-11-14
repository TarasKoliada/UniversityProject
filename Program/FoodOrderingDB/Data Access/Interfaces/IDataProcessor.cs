using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.Data_Access.Interfaces
{
    interface IDataProcessor<TEntity> where TEntity : class
    {
        void ProcessData(IDataProvider<TEntity> dataProvider);
    }
}
