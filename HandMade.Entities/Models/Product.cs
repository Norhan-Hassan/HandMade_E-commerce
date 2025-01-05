using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace HandMade.Entities.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }


        [Required(ErrorMessage ="Name is Required")]
        public string Name { get; set; }
        public string Description { get; set; }


        [Required(ErrorMessage ="Image is Required")]
        [ValidateNever]
        public string ImgUrl { get; set; }


        [Required(ErrorMessage = "Price is Required")]
        public double Price { get; set; }


        [Required(ErrorMessage = "Quantity is Required")]

        [DisplayName("Available Quantity")]
        public int Quantity { get; set; }

        [DisplayName("Category")]
        [ForeignKey("Category")]
        [ValidateNever]
        public int Category_Id { get; set; }
        [ValidateNever]
        public Category Category { get; set; }

    }
}
