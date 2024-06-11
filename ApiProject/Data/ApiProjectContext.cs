using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiProject.Model;

namespace ApiProject.Data
{
    public class ApiProjectContext : DbContext
    {
        public ApiProjectContext(DbContextOptions<ApiProjectContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; } = default!;
        public DbSet<Playlist> Playlist { get; set; } = default!;
        public DbSet<Canal> Canal { get; set; } = default!;
        public DbSet<PlaylistCanales> PlaylistCanales { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlaylistCanales>()
                .HasKey(pc => new { pc.PlaylistId, pc.CanalId });

            modelBuilder.Entity<PlaylistCanales>()
                .HasOne(pc => pc.Playlist)
                .WithMany(p => p.PlaylistCanales)
                .HasForeignKey(pc => pc.PlaylistId);

            modelBuilder.Entity<PlaylistCanales>()
                .HasOne(pc => pc.Canal)
                .WithMany(c => c.PlaylistCanales)
                .HasForeignKey(pc => pc.CanalId);
        }
    }
}
