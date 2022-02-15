using CinemaAPI.HATEOAS;
using CinemaAPI.Models;

namespace CinemaAPI.Container
{
    public class FilmeContainer
    {
        public Filme filme { get; set; }
        public Link[] links { get; set; }

    }
}