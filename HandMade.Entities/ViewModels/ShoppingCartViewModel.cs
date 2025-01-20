using HandMade.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandMade.Entities.ViewModels
{
    public class ShoppingCartViewModel
    {
        public IEnumerable<ShoppingCart> shoppingCarts { get; set; }
        public double TotalPrice { get; set; }
    }
}
