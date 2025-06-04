using Microsoft.EntityFrameworkCore;
using TuProyecto.Models;

namespace TuProyecto.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<DetalleUsuario> DetalleUsuarios { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Notificaciones> Notificaciones { get; set; }
        public DbSet<HistorialEstado> HistorialEstados { get; set; }
        public DbSet<Tecnico> Tecnicos { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Adjuntos> Adjuntos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Comentarios> Comentarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación Usuario → DetalleUsuario (Uno a Uno)
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.DetalleUsuario)
                .WithOne()
                .HasForeignKey<Usuario>(u => u.id_detalle)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Ticket → Notificaciones (Uno a Muchos)
            modelBuilder.Entity<Notificaciones>()
                .HasOne(n => n.Ticket)
                .WithMany(t => t.Notificaciones)
                .HasForeignKey(n => n.id_ticket)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación Usuario → Notificaciones (Uno a Muchos)
            modelBuilder.Entity<Notificaciones>()
                .HasOne(n => n.Usuario)
                .WithMany(u => u.Notificaciones)
                .HasForeignKey(n => n.id_usuario)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación Ticket → Tecnico (Uno a Muchos)
            modelBuilder.Entity<Tecnico>()
                .HasMany(t => t.Tickets)
                .WithOne(t => t.Tecnico)
                .HasForeignKey(t => t.id_tecnico)
                .OnDelete(DeleteBehavior.SetNull); // Si se elimina un técnico, no eliminamos los tickets.

            // Relación Ticket → Comentarios (Uno a Muchos)
            modelBuilder.Entity<Comentarios>()
                .HasOne(c => c.Ticket)
                .WithMany(t => t.Comentarios)
                .HasForeignKey(c => c.id_ticker)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación Tecnico → Comentarios (Uno a Muchos)
            modelBuilder.Entity<Comentarios>()
                .HasOne(c => c.Tecnico)
                .WithMany(t => t.Comentarios)
                .HasForeignKey(c => c.id_tecnico)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación Ticket → HistorialEstado (Uno a Muchos)
            modelBuilder.Entity<HistorialEstado>()
                .HasOne(h => h.Ticket)
                .WithMany(t => t.HistorialEstados)
                .HasForeignKey(h => h.id_ticket)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación Ticket → Adjuntos (Uno a Muchos)
            modelBuilder.Entity<Adjuntos>()
                .HasOne(a => a.Ticket)
                .WithMany(t => t.Adjuntos)
                .HasForeignKey(a => a.id_ticket)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación Ticket → Categoria (Muchos a Uno)
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Categoria)
                .WithMany(c => c.Tickets)
                .HasForeignKey(t => t.id_categoria)
                .OnDelete(DeleteBehavior.SetNull); // Si se elimina una categoría, los tickets no se eliminan.

            // Relación Categoria → Tickets (Uno a Muchos)
            modelBuilder.Entity<Categoria>()
                .HasMany(c => c.Tickets)
                .WithOne(t => t.Categoria)
                .HasForeignKey(t => t.id_categoria)
                .OnDelete(DeleteBehavior.SetNull); // Eliminar una categoría no elimina los tickets asociados
        }
    }
}
