using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VRTeleportator.Models;
using VRTeleportator.ViewModels;

namespace VRTeleportator.Controllers
{
    [Produces("application/json")]
    [Route("api/authorize")]
    public class AuthorizeController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly AppDataBase dbContext;

        public AuthorizeController(UserManager<User> userManager, AppDataBase dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
        }
        [HttpPost]
        public async Task<IActionResult> Authorize([FromBody]LoginViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (!await userManager.CheckPasswordAsync(user, model.Password))
            {


            }

            return Json();
        }

        private ClaimsIdentity GetIdentity()
        {

        }

    }
}