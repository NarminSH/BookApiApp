using AutoMapper;
using Book.BL.DTOs.AppUserDtos;
using Book.BL.Exceptions.CommonExceptions;
using Book.BL.Services.Abstractions;
using Book.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Book.BL.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public AuthService(UserManager<AppUser> userManager, IMapper mapper, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
        }

        public async Task<string> LoginAsync(AppUserLoginDto entityLoginDto)
        {
            AppUser? existingUser = await _userManager.FindByNameAsync(entityLoginDto.UserName);
            if (existingUser == null) { throw new EntityNotFoundException("Not found"); }
            var result = await _signInManager.CheckPasswordSignInAsync(existingUser, entityLoginDto.Password, true);
            if (!result.Succeeded) { throw new Exception("Username or password is wrong"); }
            string issuer = "https://localhost:7209";
            string audience = "https://localhost:7209";

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("d36331c5-90c4-4087-9f17-342813b4ea00"));

            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, existingUser.Id));
            claims.Add(new Claim(ClaimTypes.GivenName, existingUser.FirstName));
            claims.Add(new Claim("Name", existingUser.UserName));

            JwtSecurityToken generateToken = new JwtSecurityToken
                (
                issuer: issuer,
                audience: audience,
                signingCredentials: signingCredentials,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60)
                );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string token = handler.WriteToken(generateToken);
            return token;
        }

        public async Task<bool> RegisterAsync(AppUserCreateDto entityCreateDto)
        {
            AppUser user = _mapper.Map<AppUser>(entityCreateDto);
            var result = await _userManager.CreateAsync(user, entityCreateDto.Password);
            if (!result.Succeeded)
            {
                throw new Exception("Could not create user");
            }
            return true;
  
        }
    }
}
