using FoodOrderingDB.Data_Access.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.Data_Access.Implementation
{
    class DbDataProcessor<TEntity> : IDataProcessor<TEntity> where TEntity : class
    {
        public void ProcessData(IDataProvider<TEntity> dataProvider)
        {
            dataProvider.GetContext().Add(dataProvider.GetEntity());
            dataProvider.SaveData();  
        }

    }
}
