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
            ShoppingCartViewModel shoppingCart = new ShoppingCartViewModel()
            {
                shoppingCarts = unitOfWork.ShoppingCartRepo.GetAll(s => s.userId == userId,include:"product")
            };
            return View("Index", shoppingCart);
        }
    }
}
