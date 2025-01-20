using HandMade.Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HandMade.Web.Areas.Identity.Controllers
{
    [Area("Identity")]
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<string> roles = new List<string>();
            if(roleManager.Roles != null)
            {
                foreach (var role in this.roleManager.Roles)
                {
                    roles.Add(role.Name);
                }

            }
            
            return View("Index",roles);
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return View("AddRole");
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleViewModel roleVM)
        {
            if(ModelState.IsValid)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleVM.RoleName);
                if (roleExist == false)
                {
                    IdentityRole role = new IdentityRole();
                    role.Name = roleVM.RoleName;

                    IdentityResult result = await roleManager.CreateAsync(role);
                    if (result == IdentityResult.Success)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View("AddRole",roleVM);
        }
        [HttpGet]
        public IActionResult AssignUserToRole()
        {
            return View("AssignUserToRole");

        }
    }
}
