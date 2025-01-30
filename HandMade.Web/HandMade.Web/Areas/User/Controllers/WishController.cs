using HandMade.DataAccess.Migrations;
using HandMade.Entities.Models;
using HandMade.Entities.Repo_Interfaces;
using HandMade.Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HandMade.Web.Areas.User.Controllers
{
    [Area("User")]
    public class WishController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public WishController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = unitOfWork.ApplicationUserRepo.GetCurrentUser();
            if (userId == null)
            {
                return NotFound();
            }
            var wishList = unitOfWork.WishRepo.GetAll(u => u.applicationUserID == userId,include:"product");

            var WishlistViewModel = new List<WishListViewModel>();

            foreach (var item in wishList)
            {
                WishlistViewModel.Add(new WishListViewModel
                {
                    productName = item.product.Name,
                    productPrice = item.product.Price,
                    productId = item.product.ID,
                    Image = item.product.ImgUrl
                });
            }
            return View("Index", WishlistViewModel);
        }

        public IActionResult AddToWishList(int id)
        {
            var productInDb = unitOfWork.ProductRepo.GetOne(p => p.ID == id);
            var userId = unitOfWork.ApplicationUserRepo.GetCurrentUser();
            if (userId == null)
            {
                return NotFound();
            }
            var wishListInDb = unitOfWork.WishRepo.GetOne(w => w.productID == id && w.applicationUserID == userId);
            if (wishListInDb != null)
            {
                return RedirectToAction("Index","Home");
            }
            WishList wishList = new WishList()
            {
              productID= id,
              applicationUserID= userId
            };

            unitOfWork.WishRepo.Add(wishList);
            unitOfWork.Save();
            return RedirectToAction("Index", "Wish");
        }

 
        public IActionResult RemoveFromWishList(int id)
        {
            var userId = unitOfWork.ApplicationUserRepo.GetCurrentUser();
            if (userId == null)
            {
                return NotFound();
            }
            var wishListInDb = unitOfWork.WishRepo.GetOne(w => w.productID == id && w.applicationUserID == userId);

            if (wishListInDb == null)
            {
                return NotFound();
            }
        
            unitOfWork.WishRepo.Remove(wishListInDb);
            unitOfWork.Save();

            return RedirectToAction("Index", "Wish");
        }

    }
}
