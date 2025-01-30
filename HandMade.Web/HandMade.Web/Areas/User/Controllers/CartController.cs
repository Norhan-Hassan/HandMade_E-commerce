using HandMade.Entities.Models;
using HandMade.Entities.Repo_Interfaces;
using HandMade.Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe.Checkout;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckOut(ShoppingCartViewModel shoppingCartViewModel)
        {
            var userId = unitOfWork.ApplicationUserRepo.GetCurrentUser();

            shoppingCartViewModel.shoppingCarts = unitOfWork.ShoppingCartRepo.GetAll(u => u.userId == userId, include: "product");
            shoppingCartViewModel.orderSummary.OrderStatus = "pending";
            shoppingCartViewModel.orderSummary.PaymentStatus = "pending";
            shoppingCartViewModel.orderSummary.OrderDate = DateTime.Now;
            shoppingCartViewModel.orderSummary.userId = userId;

            shoppingCartViewModel.orderSummary.TotalPrice =
                unitOfWork.OrderSummaryRepo.GetTotalOrderPrice(shoppingCartViewModel.shoppingCarts);

            unitOfWork.OrderSummaryRepo.Add(shoppingCartViewModel.orderSummary);
            unitOfWork.Save();

            foreach (var item in shoppingCartViewModel.shoppingCarts)
            {
                OrderDetails orderDetails = new OrderDetails()
                {
                   price = item.product.Price,
                   count=item.count,
                   productId=item.productId,
                   orderSummaryId= shoppingCartViewModel.orderSummary.ID
                };
                unitOfWork.OrderDetailsRepo.Add(orderDetails);
                unitOfWork.Save();
            }
            var domain = "https://localhost:44308/";
            var options = new SessionCreateOptions
            {
                        LineItems = new List<SessionLineItemOptions>(),

               
                        Mode = "payment",
                        SuccessUrl = domain+$"User/Cart/OrderConfirm?id={shoppingCartViewModel.orderSummary.ID}",
                        CancelUrl = domain + $"User/Cart/index",
            };
            foreach (var item in shoppingCartViewModel.shoppingCarts)
            {

                var sessionLineItemOptions= new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount =(long)(item.product.Price*100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.product.Name,
                        },
                    },
                    Quantity = item.count,
                };
                options.LineItems.Add(sessionLineItemOptions);
            }
            var service = new SessionService();
            Session session = service.Create(options);
            shoppingCartViewModel.orderSummary.SessionId = session.Id;
            
            unitOfWork.Save();

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);


            
        }

        public IActionResult OrderConfirm(int id)
        {
            OrderSummary orderSummary = unitOfWork.OrderSummaryRepo.GetOne(u => u.ID == id);
            var service = new SessionService();
            Session session = service.Get(orderSummary.SessionId);
            orderSummary.PaymentIntentId = session.PaymentIntentId;
            if (session.PaymentStatus.ToLower() == "paid")
            {
                unitOfWork.OrderSummaryRepo.TrackOrderStatus(id,"approved", "approved");
                unitOfWork.OrderSummaryRepo.GetOne(s=>s.SessionId==session.Id).PaymentIntentId = session.PaymentIntentId;
                unitOfWork.Save();

            }
            List<ShoppingCart> shoppingCarts= unitOfWork.ShoppingCartRepo.GetAll(u=>u.userId==orderSummary.userId).ToList();
            unitOfWork.ShoppingCartRepo.RemoveRange(shoppingCarts);
            unitOfWork.Save();
            return View("OrderConfirm",id);
        }
    }
}
