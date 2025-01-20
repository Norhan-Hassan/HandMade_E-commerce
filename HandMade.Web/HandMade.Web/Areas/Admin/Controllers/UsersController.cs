using HandMade.DataAccess.Data;
using HandMade.Web.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HandMade.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext context;

        public UsersController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var claims = User.Identity as ClaimsIdentity;
            if(claims==null)
            {
               return NotFound();
            }
            var UserId = claims.FindFirst(ClaimTypes.NameIdentifier).Value;
            var users = context.ApplicationUsers.Where(i => i.Id != UserId).ToList();
            return View("Index",users);
        }


        [HttpGet]
        public IActionResult Delete(string id)
        {
            var user = context.ApplicationUsers.FirstOrDefault(i=>i.Id==id);
            if(user==null)
            {
                return NotFound();
            }
            return View("Delete",user);
        }
        public IActionResult DeleteInAction(string id)
        {
            var user = context.ApplicationUsers.FirstOrDefault(i => i.Id == id);
            context.Remove(user);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //public IActionResult LockUnLock(string id)
        //{

        //}

    }
}
