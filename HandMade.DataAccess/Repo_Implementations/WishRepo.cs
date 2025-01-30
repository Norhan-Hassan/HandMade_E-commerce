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
    public class WishRepo : GenericRepo<WishList>, IWishRepo
    {
        private readonly ApplicationDbContext context;
        public WishRepo(ApplicationDbContext context) : base(context)
        { 
            this.context = context;
        }

       
    }
}
