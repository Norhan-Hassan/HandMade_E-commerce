﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandMade.Entities.Repo_Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        public ICategoryRepo CategoryRepo { get; }
        public IProductRepo ProductRepo{ get; }
        public IShoppingCartRepo ShoppingCartRepo { get; }
        public IApplicationUserRepo ApplicationUserRepo { get; }
        public IOrderSummaryRepo OrderSummaryRepo { get;  }
        public IOrderDetailsRepo OrderDetailsRepo { get; }
        public IWishRepo WishRepo { get; }

        
        public int Save();
    }
}
