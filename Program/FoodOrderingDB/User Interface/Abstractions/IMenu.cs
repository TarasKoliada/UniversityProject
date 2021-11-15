using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingDB.User_Interface.Abstractions
{
    interface IMenu<TMenu>
    {
         string Title { get; set; }
         Action Action { get; set; }
         List<TMenu> MenuItems { get; set; }
         List<TMenu> SubItems { get; set; }
        void Navigate();
    }
}
