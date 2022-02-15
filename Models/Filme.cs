using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace CinemaAPI.Models
{
    public class Filme
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Genero { get; set; }
        public string Idioma { get; set; }
        public double Nota { get; set; }
        public double ValorIngresso { get; set; }
        public DateTime DiaDaExibicao { get; set; }
        [NotMapped]
        public IFormFile Imagem { get; set; }
        public string ImagemUrl { get; set; }
        public ICollection<Reserva> Reservas { get; set; }
    }
}