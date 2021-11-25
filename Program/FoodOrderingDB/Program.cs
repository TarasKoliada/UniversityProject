using FoodOrderingDB.User_Interface;

namespace FoodOrderingDB
{
    class Program
    {
        static void Main(string[] args)
        {
            var menu = new SiteNavigation();
            menu.ShowMenu(); 
        }
        
    }
}
