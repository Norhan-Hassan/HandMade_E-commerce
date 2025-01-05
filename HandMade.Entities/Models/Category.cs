using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandMade.Entities.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage ="Name is Required")]
        [Display(Name = "Category Name")]
        public string Name  { get; set; }

        [MaxLength(150, ErrorMessage ="Maximum Length is 150 character")]
        [Required(ErrorMessage = "Description is Required")]
        public string Description  { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        [ValidateNever]
        public ICollection<Product>  products{ get; set; }
    }
}
