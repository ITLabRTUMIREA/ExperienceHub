using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VRTeleportator.Models;
using VRTeleportator.ViewModels;

namespace VRTeleportator.Controllers
{
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = "Bearer")]
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
        [Route("{UserId}/lessons")]
        public IActionResult GetLessons(Guid UserId)
        {
            return Json(dbContext.Users.Include(l => l.UserLessons).ThenInclude(u => u.Lesson).ToList());


            //return Json(dbContext
            //    .Users
            //    .FirstOrDefault(u => u.Id == UserId)
            //    .UserLessons);
        }

        [HttpPut]
        [Route("Wallet")]
        public async Task<IActionResult> ReplanishWallet([FromBody] WalletReplanishViewModel model)
        {
            var result = dbContext.Users.FirstOrDefault(u => u.Id == model.UserId).Wallet = model.Wallet;
            await dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("lessons/purchase")]
        public async Task<IActionResult> PurchaseLesson([FromBody] PurchaseViewModel model)
        {
            var result = await dbContext.Users.FindAsync(model.UserId);

            result.UserLessons.Add(new UserLessons { UserId = model.UserId, LessonId = model.LessonId });
            result.Wallet -= model.Price;

            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}