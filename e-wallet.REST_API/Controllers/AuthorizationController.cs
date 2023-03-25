using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using e_wallet.Data.Model;
using e_wallet.REST_API.DataContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using e_wallet.REST_API.Services;

namespace e_wallet.REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly SignInManager<User> signInManager;
        private readonly ISigningSecurityKey signingSecurityKey;
        public AuthorizationController(SignInManager<User> signInManager, ISigningSecurityKey signingSecurityKey)
        {
            this.signInManager = signInManager;
            this.signingSecurityKey = signingSecurityKey;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Authorize(AuthenticationModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            if (!result.Succeeded)
                return Unauthorized();

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier,"Saidakbar.Saydakhmedov")
            };

            var token = new JwtSecurityToken(
                issuer: "e_wallet",
                audience: "e_wallet_Audience",
                expires: DateTime.Now.AddMinutes(30),
                claims: claims,
                signingCredentials: new SigningCredentials(
                    signingSecurityKey.GetKey(),
                    signingSecurityKey.SigningAlgorithm));

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }
    }
}
