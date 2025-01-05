using HandMade.DataAccess.Data;
using HandMade.DataAccess.Migrations;
using HandMade.Entities.Models;
using HandMade.Entities.Repo_Interfaces;
using HandMade.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;



namespace HandMade.DataAccess.Repo_Implementations
{
    public class ProductRepo : GenericRepo<Product>,IProductRepo 
    {
        private readonly ApplicationDbContext context;
        public ProductRepo(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public ProductCategoryListViewModel PrepareViewModel()
        {
            var categories = context.Categories.ToList();
            Product product = new Product();
            ProductCategoryListViewModel viewModel = new ProductCategoryListViewModel()
            {
                product = product,
                categories= categories,
            };
            return viewModel;
        }

        public void Update(Product product)
        {
            var productInDb = base.GetOne(c => c.ID == product.ID);
            if (productInDb != null)
            {
                productInDb.Name= product.Name;
                productInDb.Description= product.Description;
                productInDb.Price= product.Price;
                productInDb.Quantity= product.Quantity;
                productInDb.ImgUrl= product.ImgUrl;
                productInDb.Category_Id= product.Category_Id;
            }
        }

       
    }
}
