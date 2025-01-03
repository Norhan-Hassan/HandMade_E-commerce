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
    public class CategoryRepo:GenericRepo<Category>,ICategoryRepo
    {
        private readonly ApplicationDbContext context;

        public CategoryRepo(ApplicationDbContext context):base(context)
        {
            this.context = context;
        }
        public void Update(Category category)
        {
            var categoryInDb= base.GetOne(c => c.ID == category.ID);
            if (categoryInDb!=null)
            {
                categoryInDb.Name = category.Name;
                categoryInDb.CreatedTime= DateTime.Now;
                categoryInDb.Description = category.Description;
            }

        }
    }
}
