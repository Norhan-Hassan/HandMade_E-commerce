using HandMade.Entities.Models;
using HandMade.Entities.ViewModels;
using HandMade.Web.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HandMade.Web.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager,UserManager<ApplicationUser> userManager )
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();

                user.UserName = userViewModel.Name;
                user.Email = userViewModel.Email;
                user.Address = userViewModel.Address;

                IdentityResult result = await userManager.CreateAsync(user, userViewModel.Password);
                if (result.Succeeded)
                {
                    string role = HttpContext.Request.Form["Role"].ToString();//radio_button
                    if (String.IsNullOrEmpty(role))//ordinary customer
                    {
                        await userManager.AddToRoleAsync(user, Role.CustomerRole); //in utilities folder
                        return RedirectToAction("Login", "Account");
                    }

                    await userManager.AddToRoleAsync(user, role);
                    return RedirectToAction("Index","Users", new { area = "Admin" });
                    
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View("Register", userViewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel loginUser)
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(loginUser.Name);

                if(user!=null)
                {
                    var finedUser= await userManager.CheckPasswordAsync(user, loginUser.Password);
                    if(finedUser==true)
                    {
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim("Address", user.Address));

                        await signInManager.SignInWithClaimsAsync(user, loginUser.RememberMe,claims);
                        return RedirectToAction("Index", "Home", new {area="User"});
                    }
                }

                ModelState.AddModelError("", "UserName or Password is wrong");
            }
            return View("Login", loginUser);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

       

    }
}
