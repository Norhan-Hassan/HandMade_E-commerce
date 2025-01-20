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
            var claims = User.Identity as ClaimsIdentity;
            if (!User.Identity.IsAuthenticated || claims == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            string userId = claims.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartViewModel shoppingCart = new ShoppingCartViewModel();
            shoppingCart.shoppingCarts = unitOfWork.ShoppingCartRepo.GetAll(s => s.userId == userId, include: "product");

            foreach(var cart in shoppingCart.shoppingCarts)
            {
                shoppingCart.TotalPrice += (cart.count * cart.product.Price);
            }


			return View("Index", shoppingCart);
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
    }
}
