using HandMade.DataAccess.Data;
using HandMade.DataAccess.Migrations;
using HandMade.Entities.Models;
using HandMade.Entities.Repo_Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public void DecreaseShoppingCartCount(ShoppingCart shoppingCart, int count)
        {
            if (shoppingCart.count > 1) 
            {
                shoppingCart.count -= count;
            }
            else
            {
                context.ShoppingCarts.Remove(shoppingCart);
            }
        }

        public void IncreaseShoppingCartCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.count += count;
        }
    }
}
