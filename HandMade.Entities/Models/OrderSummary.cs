using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandMade.Entities.Models
{
    public class OrderSummary
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Total Price")]
        public double TotalPrice { get; set; }


        [Display(Name = "Shipping Date")]
        public DateTime ShippingDate { get; set; }


        [Display(Name= "Payment Status")]
        public string? PaymentStatus { get; set; }

        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [Display(Name = "Order Status")]
        public string? OrderStatus { get; set; }

        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }
        public string ? Carrier { get; set; }

        [Display(Name = "Tracking Number")]
        public string? TrackingNumber { get; set; }
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }
        public  string Address { get; set; }
        public  string Name { get; set; }

        public  string Email { get; set; }
        [ForeignKey("applicationUser")]
        public string userId { get; set; }
        [ValidateNever]
        public ApplicationUser applicationUser { get; set; }
    }
}
