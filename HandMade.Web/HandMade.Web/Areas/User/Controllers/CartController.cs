using HandMade.Entities.Models;
using HandMade.Entities.Repo_Interfaces;
using HandMade.Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HandMade.Web.Areas.User.Controllers
{
    [Area("User")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CartController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            if(unitOfWork.ApplicationUserRepo.GetCurrentUser() != null)
            {
                var userId = unitOfWork.ApplicationUserRepo.GetCurrentUser();
                ShoppingCartViewModel shoppingCart = new ShoppingCartViewModel();
                shoppingCart.shoppingCarts = unitOfWork.ShoppingCartRepo.GetAll(s => s.userId == userId, include: "product");

                shoppingCart.TotalPrice =unitOfWork.OrderSummaryRepo.GetTotalOrderPrice(shoppingCart.shoppingCarts);


                return View("Index", shoppingCart);
            }
            else
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

           
        }

        public IActionResult IncreaseCart(int cartId)
        {
            var cart= unitOfWork.ShoppingCartRepo.GetOne(c=>c.ID == cartId);
            unitOfWork.ShoppingCartRepo.IncreaseShoppingCartCount(cart, 1);
            unitOfWork.Save();
            return RedirectToAction("Index");

        }
        public IActionResult DecreaseCart(int cartId)
        {
            var cart = unitOfWork.ShoppingCartRepo.GetOne(c => c.ID == cartId);
            unitOfWork.ShoppingCartRepo.DecreaseShoppingCartCount(cart, 1);
            unitOfWork.Save();
            return RedirectToAction("Index");

        }
        public IActionResult DeleteCart(int cartId)
        {
            var cart = unitOfWork.ShoppingCartRepo.GetOne(c => c.ID == cartId);
            unitOfWork.ShoppingCartRepo.Remove(cart);
            unitOfWork.Save();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Summary()
        {
            if (unitOfWork.ApplicationUserRepo.GetCurrentUser() != null)
            {
                var id = unitOfWork.ApplicationUserRepo.GetCurrentUser();
                var shoppingCartViewModel = new ShoppingCartViewModel()
                {
                    shoppingCarts = unitOfWork.ShoppingCartRepo.GetAll(u => u.userId == id, include: "product"),
                    orderSummary = new(),

                };
                shoppingCartViewModel.orderSummary.applicationUser = unitOfWork.ApplicationUserRepo.GetOne(u => u.Id == id);
                shoppingCartViewModel.orderSummary.Address = shoppingCartViewModel.orderSummary.applicationUser.Address;
                shoppingCartViewModel.orderSummary.Name = shoppingCartViewModel.orderSummary.applicationUser.UserName;
                shoppingCartViewModel.orderSummary.Email = shoppingCartViewModel.orderSummary.applicationUser.Email;


                shoppingCartViewModel.TotalPrice = unitOfWork.OrderSummaryRepo.GetTotalOrderPrice(shoppingCartViewModel.shoppingCarts); 

                return View("Summary", shoppingCartViewModel);
            }
            else
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult CheckOut()
        //{

        //}
    }
}
