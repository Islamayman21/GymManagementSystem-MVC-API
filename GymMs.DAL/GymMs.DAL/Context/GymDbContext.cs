using GymMs.DAL.GymMs.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GymMs.DAL.GymMs.DAL.Context
{
    public class GymDbContext : DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options) { }

        public DbSet<Member> Members { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<GymClass> Classes { get; set; }
        public DbSet<MemberClass> MemberClasses { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Member>().HasData(
        new Member { Id = 1, FullName = "Islam Ayman", Email = "Islam@gmail.com", JoinDate = DateTime.Now.AddMonths(-2), SubscriptionEnd = DateTime.Now.AddMonths(1) },
        new Member { Id = 2, FullName = "Ahmed Ayman", Email = "Ahmed@hotmail.com", JoinDate = DateTime.Now.AddMonths(-5), SubscriptionEnd = DateTime.Now.AddDays(-3) }
    );
            modelBuilder.Entity<MemberClass>().HasKey(mc => new { mc.MemberId, mc.GymClassId });

            
            modelBuilder.Entity<GymClass>()
                .HasOne(c => c.Trainer)
                .WithMany(t => t.Classes)
                .HasForeignKey(c => c.TrainerId);



            base.OnModelCreating(modelBuilder);
        }
    }
}
