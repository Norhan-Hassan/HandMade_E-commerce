﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandMade.Entities.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Address { get; set; }
        public IEnumerable<WishList> WishLists { get; set; }
    }
}
