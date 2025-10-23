using Microsoft.EntityFrameworkCore;
using MinhaApi.Models;

namespace MinhaApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<TodoModel> Todos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Server=DESKTOP-56IRMOC\\SQLEXPRESS;Database=app;Integrated Security=True;TrustServerCertificate=True");
    }
}
