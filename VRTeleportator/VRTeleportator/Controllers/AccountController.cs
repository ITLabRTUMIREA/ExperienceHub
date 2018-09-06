using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VRTeleportator.Models;

namespace VRTeleportator.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly AppDataBase dbContext;

        public AccountController(UserManager<User> userManager, AppDataBase dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;

        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("{UserId}")]
        public IActionResult GetLessons(Guid UserId)
        {
            return Json(dbContext
                .Users
                .FirstOrDefault(u => u.Id == UserId)
                .Lessons);
        }
    }
}