using HandMade.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandMade.Entities.ViewModels
{
    public class OrderViewModel
    {
        public OrderSummary orderSummary { get; set; }
        public IEnumerable<OrderDetails> orderDetails { get; set; }
    }
}
