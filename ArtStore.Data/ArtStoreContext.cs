using ArtStore.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtStore.Data
{
    public class ArtStoreContext : DbContext
    {
        public ArtStoreContext() : base("ArtCnn")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ArtStoreContext, Migrations.Configuration>("ArtCnn"));
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }
        static ArtStoreContext()
        {
            Database.SetInitializer<ArtStoreContext>(new DbInitializer());
        }
        public static ArtStoreContext Create()
        {
            return new ArtStoreContext();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Art> Arts { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(i => i.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(i => i.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(i => new { i.RoleId, i.UserId });

        }

 
    }
}
