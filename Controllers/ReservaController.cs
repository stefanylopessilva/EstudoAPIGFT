using System;
using System.Collections.Generic;
using System.Linq;
using CinemaAPI.Data;
using CinemaAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly ApplicationDbContext _database;
        private HATEOAS.HATEOAS HATEOAS;
        public ReservaController(ApplicationDbContext database)
        {
            _database = database;
            HATEOAS = new HATEOAS.HATEOAS("localhost:5001/api/v1/Reserva");
            HATEOAS.AddAction("GET_INFO", "GET");
            HATEOAS.AddAction("DELETE_PRODUCT", "DELETE");
        }
        [Authorize]
        [HttpPost]
        public IActionResult Post(Reserva reservaObj)
        {
            _database.Reservas.Add(reservaObj);
            _database.SaveChanges();

            return StatusCode(StatusCodes.Status201Created);
        }
        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Get()
        {
            var reservaDoBanco = _database.Reservas.ToList();
            List<object> listareserva = new List<object>();
            foreach (var reserva in reservaDoBanco)
            {
                var usuario = _database.Usuarios.Where(user => user.Id == reserva.UsuarioId).First();
                var filme = _database.Filmes.Where(filme => filme.Id == reserva.FilmeId).First();
                var reservaObj = new
                {
                    Id = reserva.Id,
                    NomeUsuario = usuario.Nome,
                    NomeFilme = filme.Nome,
                    Links = HATEOAS.GetActions(reserva.Id.ToString())
                };
                listareserva.Add(reservaObj);
            }
            return Ok(listareserva);
        }
        [Authorize(Roles = "Administrador")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var reservaDoBanco = _database.Reservas.First(reserva => reserva.Id == id);;
            var usuario = _database.Usuarios.Where(user => user.Id == reservaDoBanco.UsuarioId).First();
            var filme = _database.Filmes.Where(filme => filme.Id == reservaDoBanco.FilmeId).First();
            var reservaObj = new
            {
                Id = reservaDoBanco.Id,
                Preco = reservaDoBanco.Preco,
                Quantidade = reservaDoBanco.Quantidade,
                NomeUsuario = usuario.Nome,
                EmailUsuario = usuario.Email,
                NomeFilme = filme.Nome,
                Links = HATEOAS.GetActions(reservaDoBanco.Id.ToString())
            };
            return Ok(reservaObj);
        }
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var reserva = _database.Reservas.First(reserva => reserva.Id == id);
                _database.Reservas.Remove(reserva);
                _database.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return NotFound("Reserva não encontrado.");
            }
        }
    }
}