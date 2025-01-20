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
    public class ShoppingCart
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Product")]
        public int productId { get; set; }


        [ValidateNever]
        public Product product { get; set; }


        [Range(1,30,ErrorMessage ="Please , provide quantity from 1 to 30 max")]
        public int count { get; set; }


        [ForeignKey("applicationUser")]
        public string userId { get; set; }

        [ValidateNever]
        public ApplicationUser applicationUser { get; set; }
    }
}
