using HandMade.DataAccess.Data;
using HandMade.Entities.Repo_Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandMade.DataAccess.Repo_Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        public ICategoryRepo CategoryRepo { get; private set; }
        public IProductRepo ProductRepo { get; private set; }
        public IShoppingCartRepo ShoppingCartRepo { get; private set; }

        public IApplicationUserRepo ApplicationUserRepo { get; private set; }

        public IOrderSummaryRepo OrderSummaryRepo { get; private set; }

        public IOrderDetailsRepo OrderDetailsRepo { get; private set; }
        public IWishRepo WishRepo { get; private set; }



        private readonly IHttpContextAccessor httpContextAccessor;

        public UnitOfWork(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor= httpContextAccessor;
            this.CategoryRepo = new CategoryRepo(context);
            this.ProductRepo = new ProductRepo(context);
            this.ShoppingCartRepo= new ShoppingCartRepo(context);
            this.OrderSummaryRepo = new OrderSummaryRepo(context);
            this.OrderDetailsRepo = new OrderDetailsRepo(context);
            this.ApplicationUserRepo = new ApplicationUserRepo(context,httpContextAccessor);
            this.WishRepo = new WishRepo(context);
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public int Save()
        {
           return context.SaveChanges();
        }
    }
}
