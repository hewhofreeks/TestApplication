using Microsoft.EntityFrameworkCore;
using System;
using TestApplication.DataModel.Models;

namespace TestApplication.DataModel
{
    public class MHContext : DbContext
    {
        public MHContext(DbContextOptions<MHContext> options)
            : base(options) { }
        public MHContext()
        {

        }

        public DbSet<Hunter> Hunters { get; set; }
        public DbSet<Monster> Monsters { get; set; }

        public DbSet<Weapon> Weapons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var greatSword = new Weapon {ID = 1, Name = "Great Sword", DamagePerHit = 100 };
            var huntingHorn = new Weapon { ID = 2, Name = "Hunting Horn", DamagePerHit = 200 };

            var hunter = new Hunter { ID = 1, Name = "Dan", EquippedWeaponID = 1 };
            var hunter2 = new Hunter { ID = 2, Name = "Phill", EquippedWeaponID = 2};

            var monster = new Monster { ID = 1, Name = "Baroth", Health = 1000 };
            var monster2 = new Monster { ID = 2, Name = "Nergigante", Health = 1500 };

            modelBuilder.Entity<Weapon>().HasData(greatSword, huntingHorn);
            modelBuilder.Entity<Hunter>().HasData(hunter, hunter2);
            modelBuilder.Entity<Monster>().HasData(monster, monster2);
            
        }
    }
}
