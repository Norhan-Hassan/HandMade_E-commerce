using HandMade.Entities.Repo_Interfaces;
using HandMade.Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Details(int id)
        {
            ShoppingCartViewModel shoppingCart= unitOfWork.ProductRepo.PrepareShoppingCart(id);
            if(shoppingCart == null)
            {
                return NotFound();
            }
            return View("Details", shoppingCart);
            
        }
    }
}
