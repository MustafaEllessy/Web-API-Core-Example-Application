using Assignment1.DTO;
using Assignment1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Unicode;

namespace Assignment1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Account : ControllerBase
    {
        public Account(UserManager<ApplicationUser> userManager, IConfiguration _configuration)
        {
            UserManager = userManager;
            configuration = _configuration;
        }

        public UserManager<ApplicationUser> UserManager { get; set; }
        public IConfiguration configuration { get; set; }


        [HttpPost("register")]
        public async Task <IActionResult> register(registerDTO register)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.UserName = register.Username;
                applicationUser.Email = register.Email;
                IdentityResult result = await UserManager.CreateAsync(applicationUser, register.Password);
                if(result.Succeeded)
                {
                    return Ok("Created Successfully");
                }
            }
            return BadRequest("there are "+ ModelState.ErrorCount+" Errors");
          
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = await UserManager.FindByNameAsync(login.userName);
                if(applicationUser != null) 
                {
                    bool found = await UserManager.CheckPasswordAsync(applicationUser, login.password);
                 if(found)
                    {
                        JwtSecurityToken token = new JwtSecurityToken(
                            claims: new List<Claim>()
                            {  new Claim(ClaimTypes.Name,applicationUser.UserName)
                            ,new Claim(ClaimTypes.NameIdentifier,applicationUser.Id)
                            ,new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                            },
                            expires: DateTime.Now.AddHours(1),
                            issuer: configuration["JWT:issure"],
                            audience: configuration["JWT:auduence"],
                            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretString"]))
                                                  , SecurityAlgorithms.HmacSha256)
                            ) ;
                        return Ok(
                                 new
                                 {
                                     token = new JwtSecurityTokenHandler().WriteToken(token),
                                     expiration=token.ValidTo
                                 }
                            );
                    }
                }
            }

            return Unauthorized();

        }

    }
}
