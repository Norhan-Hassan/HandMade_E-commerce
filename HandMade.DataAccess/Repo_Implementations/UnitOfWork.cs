using HandMade.DataAccess.Data;
using HandMade.Entities.Repo_Interfaces;
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
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            this.CategoryRepo = new CategoryRepo(context);
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
