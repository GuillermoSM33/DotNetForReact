using ApplicationCore.Interfaces;
using Domain.Entities;
using Finbuckle.MultiTenant;
using Infraestructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence
{
    public class ApplicationDbContext : BaseDbContext
    {
        public ApplicationDbContext(ITenantInfo currentTenant, DbContextOptions options, ICurrentUserService currentUser)
            : base(currentTenant, options, currentUser)
        {
        }

        public DbSet<persona> persona { get; set; }
        public DbSet<logs> logs { get; set; }
        public DbSet<jugador> jugador { get; set; }
        public DbSet<Estudiantes> Estudiantes { get; set; }
        public DbSet<Colaboradores> Colaboradores { get; set; }
        public DbSet<Profesor> Profesor { get; set; }
        public DbSet<Administrativo> Administrativo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Profesor>()
                .HasOne(p => p.Colaborador)
                .WithMany() 
                .HasForeignKey(p => p.FKColaborador)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Administrativo>()
                .HasOne(a => a.Colaborador)
                .WithMany() 
                .HasForeignKey(a => a.FKColaborador)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
