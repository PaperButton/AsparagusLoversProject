using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AsparagusLoversProject.Models;

namespace AsparagusLoversProject.Domain
{
    public class AppDbContext : IdentityDbContext<IdentityUser>  
    {
        public DbSet<AsparagusLover> Lovers { get; set; }
        public DbSet<FoodIntakeCounter> FoodIntakeCounters { get; set; }

        /*public DbSet<AsparagusLover> Lovers { get; set; }
        public DbSet<FoodIntakeCounter> FoodIntakeCounters { get; set; }*/

        /*public virtual DbSet<FoodIntakeCounter> FoodIntakeCounters { get; set; } = null!;
        public virtual DbSet<Lover> Lovers { get; set; } = null!;*/

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            

            builder.Entity<AsparagusLover>()
                 .HasOne(o => (FoodIntakeCounter)o.FoodIntakeCounter)
                 .WithOne(r => (AsparagusLover)r.Lover)
                 .HasForeignKey<FoodIntakeCounter>(y=>y.LoverID);
            


            builder.Entity<FoodIntakeCounter>()
                .HasOne(f => (AsparagusLover)f.Lover)
                .WithOne(l => (FoodIntakeCounter)l.FoodIntakeCounter)
                .HasForeignKey<FoodIntakeCounter>(w => w.LoverID);
            

            base.OnModelCreating(builder);

            
        }
        }
}
