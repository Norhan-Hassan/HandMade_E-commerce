
using HandMade.Entities.Models;
using HandMade.Entities.Repo_Interfaces;
using HandMade.Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HandMade.Web.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        
        public HomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
           
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products=unitOfWork.ProductRepo.GetAll();
            return View("Index",products);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var product= unitOfWork.ProductRepo.GetOne(p=>p.ID==id,include:"Category");
            if (product == null)
            {
                return NotFound();
            }
            ShoppingCart shoppingCart = new ShoppingCart()
            {
                productId = product.ID,
                product = product,
                count=1
            };
            return View("Details", shoppingCart);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DetailsInAction(ShoppingCart cart)
        {
            var claims = User.Identity as ClaimsIdentity;
            if (!User.Identity.IsAuthenticated || claims == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
            cart.userId = claims.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCart ExistingCart = unitOfWork.ShoppingCartRepo.GetOne(
                s => s.userId == cart.userId & s.productId == cart.productId
                );

            if (ExistingCart == null)
            {
                unitOfWork.ShoppingCartRepo.Add(cart);
            }

            else
            {
                unitOfWork.ShoppingCartRepo.IncreaseShoppingCartCount(ExistingCart, cart.count);
            }
            unitOfWork.Save();
            return RedirectToAction("Index");
        }
    

    }
}
