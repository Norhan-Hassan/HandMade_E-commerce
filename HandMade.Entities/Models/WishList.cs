using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandMade.Entities.Models
{
    public class WishList
    {
        public int ID { get; set; }

        [ForeignKey("product")]
        public int productID { get; set; }
        [ValidateNever]
        public Product product { get; set; }

        [ForeignKey("applicationUser")]

        public string applicationUserID { get; set; }

        [ValidateNever]
        public ApplicationUser applicationUser { get; set; }
    }
}
