using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace InstutiteOfFineArt.Core.Model
{
    class InstutiteFineArtDbContext : IdentityDbContext<User>
    {
        public InstutiteFineArtDbContext()
           : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        static InstutiteFineArtDbContext()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer<InstutiteFineArtDbContext>(new ApplicationDbInitializer());
        }

        public static InstutiteFineArtDbContext Create()
        {
            return new InstutiteFineArtDbContext();
        }
    }
}
