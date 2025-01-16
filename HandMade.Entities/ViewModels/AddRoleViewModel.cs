using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandMade.Entities.ViewModels
{
    public class AddRoleViewModel
    {
        [Required]

        public string RoleName { get; set; }
    }
}
