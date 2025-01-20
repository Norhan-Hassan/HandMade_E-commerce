using HandMade.DataAccess.Data;
using HandMade.DataAccess.Migrations;
using HandMade.Entities.Models;
using HandMade.Entities.Repo_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandMade.DataAccess.Repo_Implementations
{
    public class ShoppingCartRepo : GenericRepo<ShoppingCart>,IShoppingCartRepo
    {
        private readonly ApplicationDbContext context;
        public ShoppingCartRepo(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public int DecreaseShoppingCartCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.count -= count;
            return shoppingCart.count;
        }

        public int IncreaseShoppingCartCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.count += count;
            return shoppingCart.count;
        }
    }
}
