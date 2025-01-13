using HandMade.DataAccess.Data;
using HandMade.DataAccess.Migrations;
using HandMade.Entities.Models;
using HandMade.Entities.Repo_Interfaces;
using HandMade.Entities.ViewModels;
using Microsoft.AspNetCore.Http;
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

        public ProductCategoryListViewModel PrepareProdCatViewModel(Product? product = null)
        {
            var categories = context.Categories.ToList();
            if (product == null)
            {
                ProductCategoryListViewModel viewModel = new ProductCategoryListViewModel()
                {
                    product = new Product(),
                    categories = categories,
                };
                return viewModel;
            }
            else if (product != null)
            {
                var productInDb= context.Products.FirstOrDefault(p=>p.ID==product.ID);
                ProductCategoryListViewModel viewModel = new ProductCategoryListViewModel()
                {
                    product = productInDb,
                    categories = categories,
                };
                return viewModel;
            }
            return null;
        }
        public ShoppingCartViewModel PrepareShoppingCart(int id)
        {
            var product=base.GetOne(p=>p.ID==id,include: "Category");
            ShoppingCartViewModel shoppingCart = new ShoppingCartViewModel()
            {
                product = product,
                count = 1
            };
            return shoppingCart;

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
