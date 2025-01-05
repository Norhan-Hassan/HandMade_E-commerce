using HandMade.Entities.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace HandMade.Entities.ViewModels
{
    public class ProductCategoryListViewModel
    {
        public Product product {  get; set; }
        [ValidateNever]
        public IEnumerable<Category> categories{ get; set; }
    }
}
