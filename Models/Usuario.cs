using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [Required(ErrorMessage = "Email não pode ser nulo ou vazio")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Senha não pode ser nulo ou vazio")]
        public string Senha { get; set; }
        public string Role { get; set; }
        public ICollection<Reserva> Reservas { get; set; }
    }
}