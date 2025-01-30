
using HandMade.Entities.Repo_Interfaces;
using HandMade.Entities.ViewModels;
using HandMade.Web.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace HandMade.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class OrdersController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public OrdersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        public IActionResult GetOrdersJson()
        {
            var orders= unitOfWork.OrderSummaryRepo.GetAll(include: "applicationUser");
            return Json(new {data=orders});
        }

        [HttpGet]
        public IActionResult Details(int orderId)
        {
            OrderViewModel orderViewModel = new OrderViewModel()
            {
                orderSummary = unitOfWork.OrderSummaryRepo.GetOne(o => o.ID == orderId, include: "applicationUser"),
                orderDetails=unitOfWork.OrderDetailsRepo.GetAll(o=>o.orderSummary.ID==orderId, include: "product")
            };
            return View("Details", orderViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateDetails(OrderViewModel orderViewModel)
        {
            int orderid = orderViewModel.orderSummary.ID;
            var orderInDb = unitOfWork.OrderSummaryRepo.GetOne(o => o.ID== orderid);
          
            orderInDb.Name=orderViewModel.orderSummary.Name;
            orderInDb.Email=orderViewModel.orderSummary.Email;
            orderInDb.Address=orderViewModel.orderSummary.Address;

            if (string.IsNullOrEmpty(orderViewModel.orderSummary.Carrier))
            {
                orderInDb.Carrier=orderViewModel.orderSummary.Carrier;
            }
            if (string.IsNullOrEmpty(orderViewModel.orderSummary.TrackingNumber))
            {
                orderInDb.TrackingNumber = orderViewModel.orderSummary.TrackingNumber;
            }
            unitOfWork.OrderSummaryRepo.Update(orderInDb);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        
        public IActionResult ProccessOrder(OrderViewModel orderViewModel)
        {
            unitOfWork.OrderSummaryRepo.TrackOrderStatus(orderViewModel.orderSummary.ID,"proccessing", "completed");
            unitOfWork.Save();

            return RedirectToAction("Details", "orders" , new {orderId=orderViewModel.orderSummary.ID});
            
        }


        public IActionResult OrderShipping(OrderViewModel orderViewModel)
        {
            //if (string.IsNullOrWhiteSpace(orderViewModel.orderSummary.Carrier) ||
            //    string.IsNullOrWhiteSpace(orderViewModel.orderSummary.TrackingNumber))
            //{
            //    ModelState.AddModelError("", "Carrier and Tracking Number must be provided before shipping");
                
            //}
            int orderid = orderViewModel.orderSummary.ID;
  
            var orderInDb = unitOfWork.OrderSummaryRepo.GetOne(o => o.ID == orderid);

            orderInDb.TrackingNumber = orderViewModel.orderSummary.TrackingNumber;
            orderInDb.Carrier = orderViewModel.orderSummary.Carrier;
            orderInDb.OrderStatus ="shipping";
            orderInDb.ShippingDate = DateTime.Now;

            unitOfWork.OrderSummaryRepo.Update(orderInDb);

            unitOfWork.Save();

            
            return RedirectToAction("Details", "orders", new { orderId = orderViewModel.orderSummary.ID });
            
        }
        public async Task<IActionResult> CancelOrder(OrderViewModel orderViewModel)
        {
            var orderInDb = unitOfWork.OrderSummaryRepo.GetOne(o => o.ID == orderViewModel.orderSummary.ID);

           if(orderInDb.PaymentStatus=="completed") 
            {
                var options = new RefundCreateOptions {
                    Reason = RefundReasons.RequestedByCustomer,
                    PaymentIntent = orderInDb.PaymentIntentId
                };

                var service = new RefundService();
                var refund = await service.CreateAsync(options);
                unitOfWork.OrderSummaryRepo.TrackOrderStatus(orderViewModel.orderSummary.ID, "canceled", "refunded");
            }
            else
            {
                unitOfWork.OrderSummaryRepo.TrackOrderStatus(orderViewModel.orderSummary.ID, "canceled", "canceled");
            }
            unitOfWork.Save();
            return RedirectToAction("Details", "orders", new { orderId = orderViewModel.orderSummary.ID });
        }
    }
}
