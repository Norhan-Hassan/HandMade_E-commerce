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
        public double TotalPrice { get; set; }
        public DateTime ShippingDate { get; set; }

        public string? PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public string ? Carrier { get; set; }
        public string? TrackingNumber { get; set; }
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }


        [ForeignKey("applicationUser")]
        public string userId { get; set; }
        [ValidateNever]
        public ApplicationUser applicationUser { get; set; }
    }
}
