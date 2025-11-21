using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;
using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.Banco
{
    public class ScreenSoundContext : DbContext
    {
        public DbSet<Artista> Artistas { get; set; }
        public DbSet<Musica> Musicas { get; set; }
        public DbSet<Genero> Generos { get; set; }

        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ScreenSoundv0;Integrated Security=True;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString) // REMOVA as aspas!
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                    .UseLazyLoadingProxies(false);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Musica>()
                .HasMany(m => m.Generos)
                .WithMany(g => g.Musicas)
                .UsingEntity<Dictionary<string, object>>(
                    "MusicaGenero",
                    j => j.HasOne<Genero>().WithMany().HasForeignKey("GeneroId"),
                    j => j.HasOne<Musica>().WithMany().HasForeignKey("MusicaId"));
        }
    }
}
