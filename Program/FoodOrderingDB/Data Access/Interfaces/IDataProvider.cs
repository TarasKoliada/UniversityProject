using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.Data_Access.Interfaces
{
    interface IDataProvider<TEntity> where TEntity : class
    {
        DbSet<TEntity> GetContext();
        TEntity GetEntity();
        void SaveData();
    }
}
