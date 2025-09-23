using API.Models.Domain;
using API.Models.DTO;
using API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthContoller : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITokenRepository tokenRepositories;

        public AuthContoller(UserManager<ApplicationUser> userManager, ITokenRepository tokenRepositories)
        {
            this.userManager = userManager;
            this.tokenRepositories = tokenRepositories;
        }

        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequest)
        {
            var IdentityUser = new ApplicationUser
            {
                UserName = registerRequest.Username,
                Email = registerRequest.Username,
                IsActive = registerRequest.IsActive,
                Signature = registerRequest.Signature
            };

            var result = await userManager.CreateAsync(IdentityUser, registerRequest.Password);
            if (result.Succeeded)
            {
                if (registerRequest.Roles != null && registerRequest.Roles.Any())
                {
                    result = await userManager.AddToRolesAsync(IdentityUser, registerRequest.Roles);
                    if (result.Succeeded)
                    {
                        return Ok("User registered successfully Please Login");
                    }
                }

            }
            return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var user = await userManager.FindByEmailAsync(loginRequest.Username);
            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequest.Password);
                if (checkPasswordResult)
                {
                    if (!user.IsActive)
                    {
                        return BadRequest("User is not active");
                    }

                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        var token = tokenRepositories.CreateJwtToken(user, roles.ToList());
                        var loginResponse = new LoginResponseDto
                        {
                            JwtToken = token
                        };
                        return Ok(loginResponse);
                    }
                  
                }
            }

            return BadRequest("Username or password is incorrect");
        }
 
    }
}
