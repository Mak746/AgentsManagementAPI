
using System.Threading.Tasks;
using API.Dtos;
using System.Linq;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using API.Identity;
using Core.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Identity;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly AppIdentityDbContext _context;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signinManager,

        ITokenService tokenService, RoleManager<IdentityRole> roleManager, IMapper mapper, AppIdentityDbContext context)
        {
            _context = context;
            _roleManager = roleManager;
            _mapper = mapper;
            _tokenService = tokenService;
            _signinManager = signinManager;

            _userManager = userManager;
        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            try
            {
                var user = await _userManager.FindByEmailFromClaimsPrinciple(User);

                var userInRoles = (from u in _context.UserRoles
                                   where u.UserId == user.Id
                                   select u.RoleId).FirstOrDefault();
                var role = await _roleManager.FindByIdAsync(userInRoles);

                return new UserDto
                {
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user),
                    DisplayName = user.DisplayName,
                    Role = role.Name

                };
            }
            catch (Exception ex)
            {
                return StatusCode(HttpContext.Response.StatusCode, "Internal server error");
            }

        }
        [HttpGet("getAllUser")]
        public ActionResult<IEnumerable<UserDto>> GetAsync()
        {
            try
            {
                var users = _userManager.Users
                    .ToList();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpContext.Response.StatusCode, "Internal server error");
            }

        }

        [HttpGet("deleteUser")]
        public async Task<ActionResult<bool>> deleteUser([FromQuery] string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                var userget = _mapper.Map<AppUser>(user);
                var deletUser = await _userManager.DeleteAsync(userget);
                if (!deletUser.Succeeded) return false;
                return true;
            }
            catch (Exception ex)
            {
                return StatusCode(HttpContext.Response.StatusCode, "Internal server error");
            }

        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            try
            {
                return await _userManager.FindByEmailAsync(email) != null;
            }
            catch (Exception ex)
            {
                return StatusCode(HttpContext.Response.StatusCode, "Internal server error");
            }
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email);

                if (user == null) return Unauthorized(new ApiResponse(401));

                var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

                if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

                return new UserDto
                {
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user),
                    DisplayName = user.DisplayName,
                    Role = ""
                };
            }
            catch (Exception ex)
            {
                return StatusCode(HttpContext.Response.StatusCode, "Internal server error");
            }

        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            try
            {
                if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
                {
                    return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Email address is in use" } });
                }
                var user = new AppUser
                {
                    DisplayName = registerDto.DisplayName,
                    Email = registerDto.Email,
                    UserName = registerDto.DisplayName
                };
                var result = await _userManager.CreateAsync(user, registerDto.Password);
                if (!result.Succeeded) return BadRequest(new ApiResponse(400));
                return new UserDto
                {
                    DisplayName = user.DisplayName,
                    Token = _tokenService.CreateToken(user),
                    Email = user.Email
                };
            }
            catch (Exception ex)
            {
                return StatusCode(HttpContext.Response.StatusCode, "Internal server error");
            }

        }

    }
}