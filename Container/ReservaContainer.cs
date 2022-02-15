using CinemaAPI.HATEOAS;
using CinemaAPI.Models;

namespace CinemaAPI.Container
{
    public class ReservaContainer
    {
        public Reserva reserva { get; set; }
        public Link[] links { get; set; }
    }
}