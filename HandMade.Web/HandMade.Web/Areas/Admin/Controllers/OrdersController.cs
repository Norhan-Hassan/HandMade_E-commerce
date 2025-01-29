
using HandMade.Entities.Repo_Interfaces;
using HandMade.Entities.ViewModels;
using HandMade.Web.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}
