using HandMade.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandMade.Entities.Repo_Interfaces
{
    public interface IShoppingCartRepo:IGenericRepo<ShoppingCart>
    {
        int IncreaseShoppingCartCount(ShoppingCart shoppingCart,int count);
        int DecreaseShoppingCartCount(ShoppingCart shoppingCart,int count);

    }
}
