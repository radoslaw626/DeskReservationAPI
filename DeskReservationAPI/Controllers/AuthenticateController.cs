using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DeskReservationAPI.Dto;
using DeskReservationAPI.Entities;
using DeskReservationAPI.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DeskReservationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            IConfiguration configuration, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            RegisterDtoValidator validator = new();
            var validResult = await validator.ValidateAsync(registerDto);
            if (!validResult.IsValid)
            {
                return BadRequest(validResult.Errors);
            }
            var userExistsEmail = await _userManager.FindByEmailAsync(registerDto.Email);
            if (userExistsEmail != null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDto { Status = "Error", Message = "User with this email already exists" });
            var userExistsName = await _userManager.FindByNameAsync(registerDto.UserName);
            if (userExistsName != null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDto { Status = "Error", Message = "User with this UserName already exists" });

            var user = _mapper.Map<ApplicationUser>(registerDto);
            user.SecurityStamp = Guid.NewGuid().ToString();

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDto { Status = "Error", Message = "UserCreation failed!" });

            return Ok(new ResponseDto { Status = "Success", Message = "User created successfully" });
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDto registerDto)
        {
            RegisterDtoValidator validator = new();
            var validResult = await validator.ValidateAsync(registerDto);
            if (!validResult.IsValid)
            {
                return BadRequest(validResult.Errors);
            }
            var userExistsEmail = await _userManager.FindByEmailAsync(registerDto.Email);
            if (userExistsEmail != null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDto { Status = "Error", Message = "User with this email already exists" });
            var userExistsName = await _userManager.FindByNameAsync(registerDto.UserName);
            if (userExistsName != null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDto { Status = "Error", Message = "User with this UserName already exists" });

            var user = _mapper.Map<ApplicationUser>(registerDto);
            user.SecurityStamp = Guid.NewGuid().ToString();
            
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDto { Status = "Error", Message = "UserCreation failed!" });

            if (!await _roleManager.RoleExistsAsync(UserRolesDto.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRolesDto.Admin));

            if (!await _roleManager.RoleExistsAsync(UserRolesDto.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRolesDto.User));

            if (await _roleManager.RoleExistsAsync(UserRolesDto.Admin))
                await _userManager.AddToRoleAsync(user, UserRolesDto.Admin);

            return Ok(new ResponseDto { Status = "Success", Message = "Admin created successfully" });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            LoginDtoValidator validator = new();
            var validResult = await validator.ValidateAsync(loginDto);
            if (!validResult.IsValid)
            {
                return BadRequest(validResult.Errors);
            }
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password)) return Unauthorized();
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), expiration = token.ValidTo });

        }
    }
}
