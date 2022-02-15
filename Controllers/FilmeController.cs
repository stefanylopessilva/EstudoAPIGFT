using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CinemaAPI.Container;
using CinemaAPI.Data;
using CinemaAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FilmeController : ControllerBase
    {
        private readonly ApplicationDbContext _database;
        private HATEOAS.HATEOAS HATEOAS;

        public FilmeController(ApplicationDbContext database)
        {
            _database = database;
            HATEOAS = new HATEOAS.HATEOAS("localhost:5001/api/v1/Filme");
            HATEOAS.AddAction("GET_INFO", "GET");
            HATEOAS.AddAction("DELETE_PRODUCT", "DELETE");
        }
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var listaDeFilmes = _database.Filmes.ToList();
                List<object> filmesHATEOAS =  new List<object>();
                    foreach (var filme in listaDeFilmes)
                    {
                        var filmeHATEOAS = new 
                        {
                        Id = filme.Id,
                        Nome = filme.Nome,
                        Genero = filme.Genero,
                        Idioma = filme.Idioma,
                        Nota = filme.Nota,
                        ValorIngresso = filme.ValorIngresso,
                        DiaDaExibicao = filme.DiaDaExibicao.ToShortDateString(),
                        ImagemUrl = filme.ImagemUrl,
                        links = HATEOAS.GetActions(filme.Id.ToString())
                        };
                        filmesHATEOAS.Add(filmeHATEOAS);
                    }
                return Ok(filmesHATEOAS);
            }
            catch (Exception)
            {
                return NotFound();
            }

        }
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var filme = _database.Filmes.First(filme => filme.Id == id); 
                var filmeHATEOAS = new 
                {
                Id = filme.Id,
                    Nome = filme.Nome,
                    Genero = filme.Genero,
                    Idioma = filme.Idioma,
                    Nota = filme.Nota,
                    ValorIngresso = filme.ValorIngresso,
                    DiaDaExibicao = filme.DiaDaExibicao.ToShortDateString(),
                    ImagemUrl = filme.ImagemUrl,
                    links = HATEOAS.GetActions(filme.Id.ToString())
                };
                return Ok(filmeHATEOAS);    
            }
            catch (Exception)
            {
                return NotFound("Filme não encontrado.");
            }
             
        }
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public IActionResult Post([FromForm] Filme filmeObj)
        {
            if (filmeObj.Nome == null && filmeObj.Idioma == null && filmeObj.Genero == null && filmeObj.ValorIngresso == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var guid = Guid.NewGuid();
            var caminhoDoArquivo = Path.Combine("wwwroot", guid + ".jpg");
            if (filmeObj.Imagem != null)
            {
                var fileStream = new FileStream(caminhoDoArquivo, FileMode.Create);
                filmeObj.Imagem.CopyTo(fileStream);
            }

            filmeObj.ImagemUrl = caminhoDoArquivo.Remove(0,7);
            _database.Filmes.Add(filmeObj);
            _database.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }
        [Authorize(Roles = "Administrador")]
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromForm] Filme filmeObj)
        {
            if (filmeObj.Id > 0)
            {
                try
                {
                    var filme =  _database.Filmes.First(filme => filme.Id == id);   
                    if (filme != null)
                    {
                        var guid = Guid.NewGuid();
                        var caminhoDoArquivo = Path.Combine("wwwroot", guid + ".jpg");
                        if (filmeObj.Imagem != null)
                        {
                            var fileStream = new FileStream(caminhoDoArquivo, FileMode.Create);
                            filmeObj.Imagem.CopyTo(fileStream);
                            filme.ImagemUrl = caminhoDoArquivo.Remove(0,7);
                        }

                        filme.Nome = filmeObj.Nome != null ? filmeObj.Nome : filme.Nome;
                        filme.Genero = filmeObj.Genero != null ? filmeObj.Genero : filme.Genero;
                        filme.Idioma = filmeObj.Idioma != null ? filmeObj.Idioma : filme.Idioma;
                        filme.Nota = filmeObj.Nota != 0 ? filmeObj.Nota : filme.Nota;
                        filme.ValorIngresso = filmeObj.ValorIngresso != 0 ? filmeObj.ValorIngresso : filme.ValorIngresso;
                        filme.DiaDaExibicao = filmeObj.DiaDaExibicao;
                        filme.Idioma = filmeObj.Idioma != null ? filmeObj.Idioma : filme.Idioma;
                        _database.SaveChanges();

                        return Ok();
                    }
                    else
                    {
                        return NotFound("Filme não encontrado.");
                    }
                }
                catch
                {
                    return NotFound("Filme não encontrado.");
                }
                
            }
            else
            {
                return BadRequest("Id do filme inválido.");
            }
        }
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var filme = _database.Filmes.First(filme => filme.Id == id);
                _database.Filmes.Remove(filme);
                _database.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return NotFound("Filme não encontrado.");
            }
        }
    }
}