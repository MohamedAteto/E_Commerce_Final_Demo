using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using E_Commerce_Final_Demo.DTOs.AuthDTOs;
using E_Commerce_Final_Demo.Mapping;
using E_Commerce_Final_Demo.Models;
using E_Commerce_Final_Demo.Reposatories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace E_Commerce_Final_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentcationController : ControllerBase
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;
        private readonly IConfiguration config;

        public AuthentcationController(IUnitOfWork unit, IConfiguration config)
        {
            this.unit = unit;
            this.mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();
            this.config = config;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO DTO)
        {
            if (DTO is null)
                return BadRequest("Invalid Data");

            var item = await unit.UserRepo.GetUserByUserNameAsync(DTO.UserFullName);
            if (item != null)
                return BadRequest("User Already Exists");

            var user = mapper.Map<User>(DTO);

            //user.UserPasswordHash = Microsoft.AspNetCore.Identity.PasswordHasher<User>.HashPassword(user, DTO.UserPasswordHash);

            await unit.UserRepo.AddAsync(user);
            await unit.SaveChangesAsync();
            return Ok("User Registered Successfully");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO DTO)
        {
            if (DTO is null)
                return BadRequest("Invalid Data");


            var user = await unit.UserRepo.GetUserByUserNameAsync(DTO.UserFullName);
            if (user == null)
                return BadRequest("User Not Found");


            if (user.UserPasswordHash != DTO.UserPasswordHash)
                return BadRequest("Invalid Password");

            var token = CreateToken(user);


            return Ok(token);
        }

        private string CreateToken(User user)
        {
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]));
            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserFullName),
                new Claim(ClaimTypes.Role, user.UserRole)
            };
       
            var token = new JwtSecurityToken(
                issuer: config["JWT:Issuer"],
                audience: config["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: Creds
                );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}