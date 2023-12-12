
using System.Threading.Tasks;
using API.Dtos;
using System.Linq;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Infrastructure.Identity;
using Core.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("getAllUser")]
        public ActionResult<IEnumerable<UserDto>> GetAsync()
        {
            var users = _userManager.Users
                .ToList();

            return Ok(users);
        }
        [HttpGet("getAllRole")]
        public ActionResult<IEnumerable<RoleDto>> GetRoleAsync()
        {
            var users = _roleManager.Roles
                .ToList();

            return Ok(users);
        }
        [HttpGet("deleteUser")]
        public async Task<ActionResult<bool>> deleteUser([FromQuery] string id)
        {

            var user = await _userManager.FindByIdAsync(id);

            var userget = _mapper.Map<AppUser>(user);
            var deletUser = await _userManager.DeleteAsync(userget);
            if (!deletUser.Succeeded) return false;
            return true;
        }

        [HttpGet("deleteRole")]
        public async Task<ActionResult<bool>> deleteRole([FromQuery] string id)
        {

            var user = await _roleManager.FindByIdAsync(id);

            //var userget=  _mapper.Map<AppUser>(user);
            var deletUser = await _roleManager.DeleteAsync(user);
            if (!deletUser.Succeeded) return false;
            return true;
        }
        [HttpGet("deleteUserRole")]
        public async Task<ActionResult<bool>> deleteUserRole([FromQuery] string id)
        {

            var user = await _userManager.FindByIdAsync(id);

            var userInRoles = (from u in _context.UserRoles
                               where u.UserId == id
                               select u.RoleId).FirstOrDefault();
            var role = await _roleManager.FindByIdAsync(userInRoles);
            var deletionResult = await _userManager.RemoveFromRoleAsync(user, role.Name);
            //var userget=  _mapper.Map<AppUser>(user);
            //var deletUser=await _roleManager.DeleteAsync(user);
            if (!deletionResult.Succeeded) return false;
            return true;
        }

        [HttpPost("updateRole")]
        public async Task<ActionResult<bool>> updateRole(RoleDto roleDto)
        {

            var role = await _roleManager.FindByIdAsync(roleDto.id);
            role.Name = roleDto.displayName;

            var idResult = await _roleManager.UpdateAsync(role);

            if (!idResult.Succeeded) return false;
            return true;
        }
        //     [HttpGet("getAllUser")]
        // public async Task<ActionResult<IReadOnlyList<UserDto>>> GetAllUser()
        // {
        //     var userLists = await _userManager.GetAllUser().ToList();
        //     IReadOnlyList<UserDto> UserList=new IReadOnlyList<UserDto>();
        //     if(userLists!=null){
        //    foreach (var item in userLists)
        //     {
        //         var user=new UserDto{
        //            user.Email=item.Email,
        //            user.DisplayName=item.DisplayName,
        //         };
        //         UserList.add(user);
        //     }
        //     }



        //     return UserList;
        // }

        [HttpGet("getText")]
        public string getText()
        {
            return "Thsi is the value";
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await _userManager.FindByEmailWithAddressAsync(User);

            return _mapper.Map<AddressDto>(user.Address);
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {
            var user = await _userManager.FindByEmailWithAddressAsync(User);

            user.Address = _mapper.Map<Address>(address);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) return Ok(_mapper.Map<AddressDto>(user.Address));

            return BadRequest("Problem updating the user");
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));
            //var rolename = await _roleManager.Roles.GetRolesForUser().FirstOrDefault();
            //      var roleIds =await _userManager.UserRoles. ;
            //    var role =await _roleManager.FindByIdAsync(roleIds);
            //var rolename = await _userManager.GetRolesAsync(user).FirstOrDefault();
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

        [HttpPost("userrole")]
        public async Task<ActionResult<UserRoleDto>> UserRole(UserRoleDto userRoleDto)
        {
            var role = await _roleManager.FindByIdAsync(userRoleDto.RoleId);
            var user = await _userManager.FindByIdAsync(userRoleDto.UserId);


            var result = await _userManager.AddToRoleAsync(user, role.Name);


            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return Ok(new UserRoleDto());
        }
        [HttpGet("userroleget")]
        public async Task<ActionResult<List<UserRoleDto>>> UserRolGet()
        {
            var userRoleList = new List<UserRoleDto>();
            var userInRoles = (from u in _context.UserRoles
                               select u).ToList();
            foreach (var item in userInRoles)
            {
                var userRole = new UserRoleDto();
                var role = await _roleManager.FindByIdAsync(item.RoleId);
                var user = await _userManager.FindByIdAsync(item.UserId);
                userRole.RoleId = item.RoleId;
                userRole.UserId = item.UserId;
                userRole.UserName = user.DisplayName;
                userRole.RoleName = role.Name;

                userRoleList.Add(userRole);

            }




            //   var result=await  _userManager.AddToRoleAsync(user, role.Name);


            // if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return Ok(userRoleList);
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
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
            // var r = new AppRole
            // {
            //     Name = "Admin"
            // };
            // var roled = await _roleManager.CreateAsync(r);
            // if (!roled.Succeeded) return BadRequest(new ApiResponse(400));
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email
            };
        }
        [HttpPost("registerrole")]
        public async Task<ActionResult<RoleDto>> RegisterRole(RoleDto roleDto)
        {

            var role = new IdentityRole
            {
                Name = roleDto.name,

            };


            var roled = await _roleManager.CreateAsync(role);
            if (!roled.Succeeded) return BadRequest(new ApiResponse(400));
            return new RoleDto
            {
                displayName = roleDto.displayName,
            };
        }
    }
}