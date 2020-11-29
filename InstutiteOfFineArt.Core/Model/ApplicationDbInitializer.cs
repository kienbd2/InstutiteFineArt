using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InstutiteOfFineArt.Core.Model
{
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<InstutiteFineArtDbContext>
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
                roleManager = new IdentityRole("Manager");
                role.Create(roleManager);
            }
            var roleAdministrator = role.FindByName("Administrator");
            if (roleAdministrator == null)
            {
                roleAdministrator = new IdentityRole("Administrator");
                role.Create(roleAdministrator);
            }

            var userManager = new UserManager<User>(new UserStore<User>(context));
            const string password = "Admin@123456";

            var user = userManager.FindByName("kienstudent@example.com");
            if (user == null)
            {
                user = new User { UserName = "kienstudent@example.com", Email = "kienstudent@example.com", DateOfBirth = new DateTime(1990, 10, 10), Avartar = "https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg" };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
                userManager.AddToRole(user.Id, roleStudent.Name);
            }
            var user2 = userManager.FindByName("kienstaff@example.com");
            if (user2 == null)
            {
                user2 = new User { UserName = "kienstaff@example.com", Email = "kienstaff@example.com", DateOfBirth = new DateTime(1980, 10, 20), Avartar = "https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg" };
                var result = userManager.Create(user2, password);
                result = userManager.SetLockoutEnabled(user2.Id, false);
                userManager.AddToRole(user2.Id, roleStaff.Name);
            }
            var user3 = userManager.FindByName("kienadmin");
            if (user3 == null)
            {
                user3 = new User { UserName = "kienadmin@example.com", Email = "kienadmin@example.com", DateOfBirth = new DateTime(1970, 02, 10), Avartar = "https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg" };
                userManager.Create(user3, password);
                userManager.SetLockoutEnabled(user3.Id, false);
                userManager.AddToRole(user3.Id, roleAdministrator.Name);
            }
            var user4 = userManager.FindByName("kienmanager");
            if (user4 == null)
            {
                user4 = new User { UserName = "kienmanager@example.com", Email = "kienmanager@example.com", DateOfBirth = new DateTime(1995, 10, 10), Avartar = "https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg" };
                userManager.Create(user4, password);
                userManager.SetLockoutEnabled(user4.Id, false);
                userManager.AddToRole(user4.Id, roleManager.Name);
            }


            Competition competition = new Competition()
            {
                Name = "Instutite Fine Art",
                StartDate = new DateTime(2020, 11, 02),
                EndDate = new DateTime(2020, 12, 04),
                Description = "Description",
                CreatedTime = DateTime.Now,
                Logo = "https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg",
                Posts = new List<Post>
                {
                    new Post
                    {
                        Title = "C# is a simple & powerful object-oriented programming language developed by Microsoft.",
                        PostContent = "C# is a simple & powerful object-oriented programming language developed by Microsoft. C# can be used to create various types of applications, such as web, windows, console applications, or other types of applications using Visual studio.",
                        Published = true,
                        CreatedTime = DateTime.Now,
                        UpdatedTime = DateTime.Now,
                        User= user,
                        Mark=4,
                        Price=1000,
                        Images="https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg",
                        PriceCustomer=800,
                        IsSold=true,
                        IsPaid=true
                    },
                    new Post
                    {
                        Title = "ASP.NET is a free web framework for building websites and web applications on .NET Framework using HTML, CSS, and JavaScript.",
                        PostContent = "ASP.NET MVC 5 is a web framework based on Mode-View-Controller (MVC) architecture. Developers can build dynamic web applications using ASP.NET MVC framework that enables a clean separation of concerns, fast development, and TDD friendly.",
                        Published = true,
                        CreatedTime = DateTime.Now,
                        UpdatedTime = DateTime.Now,
                        User= user,
                        Mark=3,
                        Price=500,
                        Images="https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg",
                        PriceCustomer=500,
                        IsSold=true,
                        IsPaid=true
                    }
                }
            };
            context.Competitions.Add(competition);
            Awards awards = new Awards
            {
                Name = "Awards Instutite Fine Art ",
                Ranking = 1,
                Posts = competition.Posts,
                Competition = competition
            };
            context.Awards.Add(awards);
            UserClass userClass = new UserClass
            {
                Name = "MVC",
                Users = new List<User>
                {
                    user,user2
                }
            };
            UserClass userClass2 = new UserClass
            {
                Name = "Painting",
                Users = new List<User>
                {
                    user3
                }
            };
            UserClass userClass3 = new UserClass
            {
                Name = "C#",
                Users = new List<User>
                {
                    user4
                }
            };
            user.UserClass = userClass;
            user2.UserClass = userClass;
            user3.UserClass = userClass2;
            user4.UserClass = userClass3;
            user.Posts = competition.Posts;

            Comment comment = new Comment
            {
                User = user2,
                Post = competition.Posts[0],
                CommentText = "Good article, beautiful image, try to promote",
                CreateTime = DateTime.Now

            };
            context.Comments.Add(comment);
            Comment comment2 = new Comment
            {
                User = user2,
                Post = competition.Posts[1],
                CommentText = "Good article, beautiful image, try to promote",
                CreateTime = DateTime.Now
            };
            context.Comments.Add(comment2);
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
            UserClass userClass = new UserClass
            {
                Name = "MVC",
            };
            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new User { UserName = name, Email = name, DateOfBirth = new DateTime(1980, 10, 20), Avartar = "https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg",UserClass = userClass };
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
