using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VRTeleportator.Models;
using VRTeleportator.Services.Interfaces;
using VRTeleportator.ViewModels;

namespace VRTeleportator.Controllers
{
    [Produces("application/json")]
    [Route("api/authorize")]
    public class AuthorizeController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly AppDataBase dbContext;
        private readonly IConfiguration configuration;
        private readonly ISecretKey keyClass;

        public AuthorizeController(UserManager<User> userManager,
            AppDataBase dbContext,
            IConfiguration configuration,
            ISecretKey keyClass)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.configuration = configuration;
            this.keyClass = keyClass;
        }
        [HttpPost]
        public async Task<IActionResult> Authorize([FromBody]LoginViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (await userManager.CheckPasswordAsync(user, model.Password))
            {
                var now = DateTime.UtcNow;
                var jwt = new JwtSecurityToken(
                    issuer: configuration.GetSection("JwtOptions")["Issuer"],
                    audience: configuration.GetSection("JwtOptions")["Audience"],
                    notBefore: now,
                    claims: GetIdentity(model).Claims,
                    expires: now.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: keyClass.GetSecretKey()
                    );

                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                return Json(encodedJwt);
            }
            return BadRequest();
        }
        private ClaimsIdentity GetIdentity(LoginViewModel user)
        {
            var result = dbContext.UserAccounts.FirstOrDefault(u => u.Email == user.Email);
            if (result != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, result.Email),
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                    claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }
            return null;
        }
    }
}