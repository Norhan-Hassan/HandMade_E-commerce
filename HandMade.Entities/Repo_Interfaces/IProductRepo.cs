using HandMade.Entities.Models;
using HandMade.Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandMade.Entities.Repo_Interfaces
{
    public interface IProductRepo:IGenericRepo<Product>
    {
        ProductCategoryListViewModel PrepareProdCatViewModel(Product ? product=null);
        void Update(Product product);

    }
}
