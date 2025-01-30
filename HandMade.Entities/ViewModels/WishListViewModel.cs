using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandMade.Entities.ViewModels
{
    public class WishListViewModel
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public double productPrice { get; set; }

        public string Image { get; set; }

    }
}
