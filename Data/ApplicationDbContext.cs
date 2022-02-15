using System;
using AuthenticationPlugin;
using CinemaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Reserva> Reservas { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Filme>().HasData(
                new{Id = 1, Nome = "Titanic", Genero = "Romance", Idioma = "Legendado", Nota = 9.2, ValorIngresso = 22.50, DiaDaExibicao = new DateTime(2021, 12, 20), ImagemUrl = "\\efd8fa90-beef-42fd-92a6-7363d6f2a2da.jpg"},
                new{Id = 2, Nome = "De volta para o futuro", Genero = "Ficção Científica", Idioma = "Dublado", Nota = 8.9 ,ValorIngresso = 20.50, DiaDaExibicao = new DateTime(2021, 12, 21), ImagemUrl = "\\6880916d-3518-4b89-a686-fe885653cad8.jpg"},
                new{Id = 3, Nome = "Psicose", Genero = "Drama", Idioma = "Legendado", Nota = 9.5, ValorIngresso = 22.50, DiaDaExibicao = new DateTime(2021, 12, 19), ImagemUrl = "\\762cce23-8936-4022-9cc9-58e53d3d7d39.jpg"},
                new{Id = 4, Nome = "Laranja mecânica", Genero = "Ficção Científica", Idioma = "Legendado", Nota = 9.1,ValorIngresso = 22.50, DiaDaExibicao = new DateTime(2021, 12, 18), ImagemUrl = "\\1d1087fb-a3e6-4ec4-850a-a01b67043f61.jpg"}
            );

            modelBuilder.Entity<Usuario>().HasData(
                new{Id = 1, Nome = "Admin", Email = "admin@dmin.com", Senha = SecurePasswordHasherHelper.Hash("Admin2021"), Role = "Administrador"},
                new{Id = 2, Nome = "Stefany", Email = "stefany@teste.com", Senha = SecurePasswordHasherHelper.Hash("Usuario2021"), Role = "Usuario"},
                new{Id = 3, Nome = "Thais", Email = "thais@teste.com", Senha = SecurePasswordHasherHelper.Hash("Usuario2021"), Role = "Usuario"}
            );

            modelBuilder.Entity<Reserva>().HasData(
                new{Id = 1, Quantidade = 1, Preco = 22.50, FilmeId = 1, UsuarioId = 2},
                new{Id = 2, Quantidade = 1, Preco = 20.50, FilmeId = 2, UsuarioId = 3},
                new{Id = 3, Quantidade = 2, Preco = 45.00, FilmeId = 3, UsuarioId = 3}
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}