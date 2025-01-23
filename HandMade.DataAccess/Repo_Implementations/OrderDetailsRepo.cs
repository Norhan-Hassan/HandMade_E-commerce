using HandMade.DataAccess.Data;
using HandMade.Entities.Models;
using HandMade.Entities.Repo_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandMade.DataAccess.Repo_Implementations
{
    public class OrderDetailsRepo : GenericRepo<OrderDetails>, IOrderDetailsRepo
    {
        private readonly ApplicationDbContext context;

        public OrderDetailsRepo(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(OrderDetails orderDetails)
        {
           context.OrderDetails.Update(orderDetails);
        }
    }
}
