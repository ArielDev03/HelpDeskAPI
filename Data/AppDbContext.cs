using HelpDeskAPI.Models.TicketComments;
using HelpDeskAPI.Models.Tickets;
using HelpDeskAPI.Models.Users;
using HelpDeskAPI.Models.TicketsPriority;
using HelpDeskAPI.Models.TicketsStatus;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) 
        { 
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<TicketComment> TicketComments { get; set; }

        public DbSet<TicketStatus> TicketStatuses { get; set; }

        public DbSet<TicketPriority> TicketPriorities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Usuario)
                .WithMany(u => u.TicketsCreados)
                .HasForeignKey(t => t.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.UsuarioAsignado)
                .WithMany(u => u.TicketsAsignados)
                .HasForeignKey(t => t.UsuarioAsignadoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.TicketStatus)
                .WithMany()
                .HasForeignKey(t => t.TicketStatusId);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.TicketPriority)
                .WithMany()
                .HasForeignKey(t => t.TicketPriorityId);

            modelBuilder.Entity<TicketComment>()
                .HasOne(c => c.Ticket)
                .WithMany(t => t.Comentarios)
                .HasForeignKey(c => c.TicketId);

            modelBuilder.Entity<TicketStatus>()
                .HasData(
                    new TicketStatus { Id = 1, Nombre = "Abierto" },
                    new TicketStatus { Id = 2, Nombre = "En Proceso" },
                    new TicketStatus { Id = 3, Nombre = "Resuelto" },
                    new TicketStatus { Id = 4, Nombre = "Cerrado" }
                );

            modelBuilder.Entity<TicketPriority>()
                .HasData(
                    new TicketPriority { Id = 1, Nombre = "Baja" },
                    new TicketPriority { Id = 2, Nombre = "Media" },
                    new TicketPriority { Id = 3, Nombre = "Alta" },
                    new TicketPriority { Id = 4, Nombre = "Critica" }
                );
        }

    }
}
