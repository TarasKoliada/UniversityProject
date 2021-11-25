namespace FoodOrderingDB.Abstractions
{
    interface IRegistrable<T>
    {
        T Register();
        void SetInfoToDb(T entity);
    }
}
