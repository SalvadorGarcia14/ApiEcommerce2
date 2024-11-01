﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Application.Service
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _config;

        public JwtTokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(Usuario user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Claims del usuario
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // Identificador del usuario
                new Claim(ClaimTypes.Name, user.Email),                    // Nombre principal, usado en User.Identity.Name
                new Claim("sub", user.Id.ToString()),
                new Claim("given_name", user.Nombre),
                new Claim("family_name", user.Apellido),
                new Claim("role", user.Role), // Añadimos el rol directamente desde el usuario
                new Claim(ClaimTypes.Role, user.Role)

            };

            var token = new JwtSecurityToken(
                issuer: "ApiEcommerce2",
                audience: "StandardUsers",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // Expiración en una hora
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
