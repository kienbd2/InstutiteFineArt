using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InstutiteOfFineArt.Core.Model
{
    public class ApplicationDbInitializer : CreateDatabaseIfNotExists<InstutiteFineArtDbContext>
    {
        protected override void Seed(InstutiteFineArtDbContext context)
        {
            var role = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var roleStudent = role.FindByName("Student");
            if (roleStudent == null)
            {
                roleStudent = new IdentityRole("Student");
                role.Create(roleStudent);
            }
            var roleStaff = role.FindByName("Staff");
            if (roleStaff == null)
            {
                roleStaff = new IdentityRole("Staff");
                role.Create(roleStaff);
            }
            var roleManager = role.FindByName("Manager");
            if (roleManager == null)
            {
                roleStaff = new IdentityRole("Manager");
                role.Create(roleManager);
            }
            var roleAdministrator = role.FindByName("Administrator");
            if (role == null)
            {
                roleAdministrator = new IdentityRole("Administrator");
                role.Create(roleAdministrator);
            }

            var userManager = new UserManager<User>(new UserStore<User>(context));
            const string password = "Admin@123456";

            var user = userManager.FindByName("kienstudent@example.com");
            if (user == null)
            {
                user = new User { UserName = "kienstudent@example.com", Email = "kienstudent@example.com" };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
                userManager.AddToRole(user.Id, roleStudent.Name);
            }
            var user2 = userManager.FindByName("kienstaff@example.com");
            if (user2 == null)
            {
                user2 = new User { UserName = "kienstaff@example.com", Email = "kienstaff@example.com" };
                var result = userManager.Create(user2, password);
                result = userManager.SetLockoutEnabled(user2.Id, false);
                userManager.AddToRole(user2.Id, roleStaff.Name);
            }
            var user3 = userManager.FindByName("kienadmin@example.com");
            if (user3 == null)
            {
                user3 = new User { UserName = "kienadmin@example.com", Email = "kienadmin@example.com" };
                userManager.Create(user3, password);
                userManager.SetLockoutEnabled(user3.Id, false);
                userManager.AddToRole(user3.Id, roleStaff.Name);
            }
            var user4 = userManager.FindByName("kienmanager@example.com");
            if (user4 == null)
            {
                user4 = new User { UserName = "kienmanager@example.com", Email = "kienmanager@example.com" };
                userManager.Create(user4, password);
                userManager.SetLockoutEnabled(user4.Id, false);
                userManager.AddToRole(user4.Id, roleStaff.Name);
            }


            Competition competition = new Competition() { Name = "Instutite Fine Art", StartDate = new DateTime(2020, 11, 02), EndDate = new DateTime(2020, 12, 04) };

            Post post = new Post() { 
            
                
            };

            InitializeIdentityForEF(context);
            context.SaveChanges();
            base.Seed(context);
        }

        //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
        public static void InitializeIdentityForEF(InstutiteFineArtDbContext db)
        {
            var userManager = new UserManager<User>(new UserStore<User>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            const string name = "admin@example.com";
            const string password = "Admin@123456";
            const string roleName = "Admin";

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new User { UserName = name, Email = name };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }
    }
}
