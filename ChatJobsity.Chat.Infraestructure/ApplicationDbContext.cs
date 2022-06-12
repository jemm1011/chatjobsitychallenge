using ChatJobsity.Chat.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatJobsity.Chat.Infraestructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomParticipant> Participants { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<RoomParticipant>().ToTable("RoomParticipants");

            modelBuilder.Entity<Room>(entity =>            {
                entity.ToTable("Rooms");
            });

            modelBuilder.Entity<Message>().ToTable("Messages");
        }
    }    
}
