using System;

namespace CinemaAPI.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public double Preco { get; set; }
        public int FilmeId { get; set; }
        public int UsuarioId { get; set; }
    }
}