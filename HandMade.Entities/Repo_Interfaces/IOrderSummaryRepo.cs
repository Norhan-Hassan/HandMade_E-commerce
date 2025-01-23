using HandMade.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandMade.Entities.Repo_Interfaces
{
    public interface IOrderSummaryRepo:IGenericRepo<OrderSummary>
    {
        void Update(OrderSummary orderSummary);
        void TrackOrderStatus(OrderSummary orderSummary);
        double GetTotalOrderPrice(IEnumerable<ShoppingCart> shoppingCarts);
    }
}
