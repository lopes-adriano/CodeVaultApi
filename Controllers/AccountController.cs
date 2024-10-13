using AutoMapper;
using CodeVaultApi.Dtos.AppUser;
using CodeVaultApi.Interfaces;
using CodeVaultApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeVaultApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController(
        IMapper mapper,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ITokenService tokenService
    ) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly SignInManager<AppUser> _signInManager = signInManager;
        private readonly ITokenService _tokenService = tokenService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var newUser = _mapper.Map<AppUser>(user);

                var result = await _userManager.CreateAsync(newUser, user.Password);
                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                var roleResult = await _userManager.AddToRoleAsync(newUser, "User");

                if (!roleResult.Succeeded)
                    return BadRequest(roleResult.Errors);

                return Ok(
                    new NewUserDto
                    {
                        UserName = newUser.UserName!,
                        Email = newUser.Email!,
                        Token = _tokenService.CreateToken(newUser),
                    }
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUser user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existingUser = await _userManager.FindByNameAsync(user.UserName);

                if (existingUser == null)
                    return Unauthorized("Invalid User or Password");

                var result = await _signInManager.CheckPasswordSignInAsync(
                    existingUser,
                    user.Password,
                    false
                );

                if (!result.Succeeded)
                    return Unauthorized("Invalid User or Password");

                return Ok(
                    new NewUserDto
                    {
                        UserName = existingUser.UserName!,
                        Email = existingUser.Email!,
                        Token = _tokenService.CreateToken(existingUser),
                    }
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
