using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VRTeleportator.Models;
using VRTeleportator.ViewModels;

namespace VRTeleportator.Controllers
{
    [Produces("application/json")]
    [Route("api/registration")]
    public class RegistrationController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly AppDataBase dbContext;

        public RegistrationController(UserManager<User> userManager, AppDataBase dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Registration([FromBody] RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password
                };
                var result = await userManager.CreateAsync(user, model.Password);
            }

            return Ok("Registration is done");
        }
    }
}