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
    public class OrderDetails
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("orderSummary")]
        public  int orderSummaryId { get; set; }
        [ValidateNever]
        public OrderSummary orderSummary { get; set; }


        [ForeignKey("product")]
        public int productId { get; set; }
        [ValidateNever]
        public Product product { get; set; }

        public int count { get; set; }
        public int price { get; set; }
    }
}
