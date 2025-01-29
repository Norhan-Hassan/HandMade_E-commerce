using HandMade.DataAccess.Data;
using HandMade.Entities.Models;
using HandMade.Entities.Repo_Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HandMade.DataAccess.Repo_Implementations
{
    public class ApplicationUserRepo : GenericRepo<ApplicationUser>, IApplicationUserRepo
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        public ApplicationUserRepo(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public void Update(ApplicationUser applicationUser)
        {
            context.Update(applicationUser);
        }
        public string GetCurrentUser()
        {
            var user = httpContextAccessor.HttpContext.User;

            var claims = user.Identity as ClaimsIdentity;

            if (!user.Identity.IsAuthenticated || claims == null)
            {
                return null;
            }

            string id = claims.FindFirst(ClaimTypes.NameIdentifier).Value;
            return id;
        }
    }
}
