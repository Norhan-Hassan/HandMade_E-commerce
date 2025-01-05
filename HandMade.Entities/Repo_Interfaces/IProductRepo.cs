using HandMade.Entities.Models;
using HandMade.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandMade.Entities.Repo_Interfaces
{
    public interface IProductRepo:IGenericRepo<Product>
    {
        ProductCategoryListViewModel PrepareViewModel();
        void Update(Product product);
    }
}
