using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VRTeleportator.Models;
using VRTeleportator.ViewModels;

namespace VRTeleportator.Controllers
{
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/lesson")]
    public class LessonController : Controller
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> userManager;
        private readonly AppDataBase dbContext;

        public LessonController(UserManager<User> userManager, AppDataBase dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddLesson([FromBody]LessonAddViewModel model)
        {
            var result = await dbContext.Users.FindAsync(model.CreatorId);

            Lesson lesson = new Lesson
            {
                Name = model.Name,
                Price = model.Price,
                ReleaseDate = model.RecordTime,
                Description = model.Description,
                Creator = $"{result.FirstName} {result.LastName}",
            };

            return Ok();
        }
    }
}