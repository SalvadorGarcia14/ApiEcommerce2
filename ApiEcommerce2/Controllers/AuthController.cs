using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiEcommerce2.DTOs;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Application.Dtos;

namespace ApiEcommerce2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public AuthController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UsuarioRegistroDTO usuarioDto)
        {
            var usuario = await _usuarioService.RegisterAsync(
                usuarioDto.Nombre,
                usuarioDto.Apellido,
                usuarioDto.Email,
                usuarioDto.Password,
                usuarioDto.IsAdminLoggedIn,  // Asegúrate de pasar aquí un bool
                usuarioDto.Rango);
            return Ok(usuario);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO loginDto)
        {
            var token = await _usuarioService.LoginAsync(
                loginDto.Email,
                loginDto.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(new { Token = token });
        }
    }
}
