using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace InstutiteOfFineArt.Core.Model
{
    public class InstutiteFineArtDbContext : IdentityDbContext<User>
    {
        public InstutiteFineArtDbContext()
           : base("InstutiteFineArtConnection", throwIfV1Schema: false)
        {
        }

        static InstutiteFineArtDbContext()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer<InstutiteFineArtDbContext>(new ApplicationDbInitializer());
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Awards> Awards  { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<UserClass> UserClasses { get; set; }
        public DbSet<Comment> Comments { get; set; }


        public static InstutiteFineArtDbContext Create()
        {
            return new InstutiteFineArtDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim");
        }
    }
}
