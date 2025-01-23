using HandMade.DataAccess.Data;
using HandMade.Entities.Models;
using HandMade.Entities.Repo_Interfaces;
using HandMade.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandMade.DataAccess.Repo_Implementations
{
    public class OrderSummaryRepo : GenericRepo<OrderSummary>, IOrderSummaryRepo
    {
        private readonly ApplicationDbContext context;

        public OrderSummaryRepo(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public double GetTotalOrderPrice(IEnumerable<ShoppingCart> shoppingCarts)
        {
            var TotalPrice = 0.0;
            foreach (var cart in shoppingCarts)
            {
                TotalPrice += (cart.count * cart.product.Price);
            }
            return TotalPrice;
        }

        public void TrackOrderStatus(OrderSummary orderSummary)
        {
            var ordersInDb = context.OrderSummaries.FirstOrDefault(o=>o.ID==orderSummary.ID);
            if(ordersInDb != null)
            {
                ordersInDb.OrderStatus = orderSummary.OrderStatus;
                if(ordersInDb.PaymentStatus != null)
                {
                    ordersInDb.PaymentStatus = ordersInDb.PaymentStatus;
                }
            }
        }

        public void Update(OrderSummary orderSummary)
        {
            context.OrderSummaries.Update(orderSummary);
        }
    }
}
