using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AuthenticationPlugin;
using CinemaAPI.Data;
using CinemaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CinemaAPI.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext _database;

        public UsuarioController(ApplicationDbContext database)
        {
            _database = database;
        }

        [HttpPost]
        public IActionResult Registro([FromBody] Usuario usuario)
        {
            var usuarioComEmailIgual = _database.Usuarios.Where(user => user.Email == usuario.Email).SingleOrDefault();
            if (usuarioComEmailIgual != null)
            {
                return BadRequest("Este email já está cadastrado");
            }
            var usuarioObj = new Usuario
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = SecurePasswordHasherHelper.Hash(usuario.Senha),
                Role = "Usuario"
            };
            _database.Usuarios.Add(usuarioObj);
            _database.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPost]
        public IActionResult Login([FromBody]Usuario usuario)
        {
            try
            {
                Usuario usuarioEmail = _database.Usuarios.First(user => user.Email.Equals(usuario.Email));
                if (usuarioEmail != null)
                {
                    if (SecurePasswordHasherHelper.Verify(usuario.Senha, usuarioEmail.Senha))
                    {
                        string chaveDeSeguranca = "cinemaGFT_gft_2021";
                        var chaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveDeSeguranca));
                        var credenciaisDeAcesso = new SigningCredentials(chaveSimetrica, SecurityAlgorithms.HmacSha256Signature);

                        var claims = new List<Claim>();
                        claims.Add(new Claim("id", usuarioEmail.Id.ToString()));
                        claims.Add(new Claim("email", usuarioEmail.Email));
                        claims.Add(new Claim(ClaimTypes.Role, usuarioEmail.Role));

                        var JWT = new JwtSecurityToken(
                            issuer: "GFT",
                            expires: DateTime.Now.AddHours(1),
                            audience: "usuario_comum",
                            signingCredentials: credenciaisDeAcesso,
                            claims: claims
                        );

                        return Ok(new JwtSecurityTokenHandler().WriteToken(JWT));
                    }
                    else
                    {
                        return Unauthorized("Senha inválida");
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
        }
    }
}