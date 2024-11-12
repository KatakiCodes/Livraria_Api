using LivrariaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LivrariaAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
        }
        public DbSet<CategoriaModel>Categorias { get; set; }
        public DbSet<LivroModel>Livros { get; set; }
        public DbSet<PerfilModel>Perfis { get; set; }
        public DbSet<AutorModel> Autores { get; set; }
        public DbSet<UserModel> Utilizadores { get; set; }
    }
}
