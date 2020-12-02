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
            UserClass userClass = new UserClass
            {
                Name = "MVC"
            };
            UserClass userClass2 = new UserClass
            {
                Name = "Painting"
            };
            UserClass userClass3 = new UserClass
            {
                Name = "C#"
            };


            var userManager = new UserManager<User>(new UserStore<User>(context));
            const string password = "Admin@123456";

            var user = userManager.FindByName("kienstudent@example.com");
            if (user == null)
            {
                user = new User { UserName = "kienstudent@example.com", Email = "kienstudent@example.com", DateOfBirth = new DateTime(1990, 10, 10), Avartar = "https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg", UserClass = userClass };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
                userManager.AddToRole(user.Id, roleStudent.Name);
            }
            var userStudent2 = userManager.FindByName("kienstudent2@example.com");
            if (userStudent2 == null)
            {
                userStudent2 = new User { UserName = "kienstudent2@example.com", Email = "kienstudent2@example.com", DateOfBirth = new DateTime(1990, 10, 10), Avartar = "https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg", UserClass = userClass };
                var result = userManager.Create(userStudent2, password);
                result = userManager.SetLockoutEnabled(userStudent2.Id, false);
                userManager.AddToRole(userStudent2.Id, roleStudent.Name);
            }
            var userStudent3 = userManager.FindByName("kienstudent3@example.com");
            if (userStudent3 == null)
            {
                userStudent3 = new User { UserName = "kienstudent3@example.com", Email = "kienstudent3@example.com", DateOfBirth = new DateTime(1990, 10, 10), Avartar = "https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg", UserClass = userClass };
                var result = userManager.Create(userStudent3, password);
                result = userManager.SetLockoutEnabled(userStudent3.Id, false);
                userManager.AddToRole(userStudent3.Id, roleStudent.Name);
            }
            var user2 = userManager.FindByName("kienstaff@example.com");
            if (user2 == null)
            {
                user2 = new User { UserName = "kienstaff@example.com", Email = "kienstaff@example.com", DateOfBirth = new DateTime(1980, 10, 20), Avartar = "https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg", UserClass = userClass2 };
                var result = userManager.Create(user2, password);
                result = userManager.SetLockoutEnabled(user2.Id, false);
                userManager.AddToRole(user2.Id, roleStaff.Name);
            }
            var userStaff2 = userManager.FindByName("kienstaff2@example.com");
            if (userStaff2 == null)
            {
                userStaff2 = new User { UserName = "kienstaff2@example.com", Email = "kienstaff2@example.com", DateOfBirth = new DateTime(1980, 10, 20), Avartar = "https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg", UserClass = userClass2 };
                var result = userManager.Create(userStaff2, password);
                result = userManager.SetLockoutEnabled(userStaff2.Id, false);
                userManager.AddToRole(userStaff2.Id, roleStaff.Name);
            }
            var userStaff3 = userManager.FindByName("kienstaff3@example.com");
            if (userStaff3 == null)
            {
                userStaff3 = new User { UserName = "kienstaff3@example.com", Email = "kienstaff3@example.com", DateOfBirth = new DateTime(1980, 10, 20), Avartar = "https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg", UserClass = userClass2 };
                var result = userManager.Create(userStaff3, password);
                result = userManager.SetLockoutEnabled(userStaff3.Id, false);
                userManager.AddToRole(userStaff3.Id, roleStaff.Name);
            }
            var userStaff4 = userManager.FindByName("kienstaff4@example.com");
            if (userStaff4 == null)
            {
                userStaff4 = new User { UserName = "kienstaff4@example.com", Email = "kienstaff4@example.com", DateOfBirth = new DateTime(1980, 10, 20), Avartar = "https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg", UserClass = userClass2 };
                var result = userManager.Create(userStaff4, password);
                result = userManager.SetLockoutEnabled(userStaff4.Id, false);
                userManager.AddToRole(userStaff4.Id, roleStaff.Name);
            }
            var user3 = userManager.FindByName("kienadmin");
            if (user3 == null)
            {
                user3 = new User { UserName = "kienadmin@example.com", Email = "kienadmin@example.com", DateOfBirth = new DateTime(1970, 02, 10), Avartar = "https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg", UserClass = userClass3 };
                userManager.Create(user3, password);
                userManager.SetLockoutEnabled(user3.Id, false);
                userManager.AddToRole(user3.Id, roleAdministrator.Name);
            }
            var user4 = userManager.FindByName("kienmanager");
            if (user4 == null)
            {
                user4 = new User { UserName = "kienmanager@example.com", Email = "kienmanager@example.com", DateOfBirth = new DateTime(1995, 10, 10), Avartar = "https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg", UserClass = userClass };
                userManager.Create(user4, password);
                userManager.SetLockoutEnabled(user4.Id, false);
                userManager.AddToRole(user4.Id, roleManager.Name);
            }

            Post post1 = new Post
            {
                Title = "C# is a simple & powerful object-oriented programming language developed by Microsoft.",
                PostContent = "C# is a simple & powerful object-oriented programming language developed by Microsoft. C# can be used to create various types of applications, such as web, windows, console applications, or other types of applications using Visual studio.",
                Published = true,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                User = user,
                Mark = 60,
                Price = 1000,
                Images = "https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg",
                PriceCustomer = 800,
                IsSold = true,
                IsPaid = true
            };
            context.Posts.Add(post1);
            Post post2 = new Post
            {
                Title = "ASP.NET is a free web framework for building websites and web applications on .NET Framework using HTML, CSS, and JavaScript.",
                PostContent = "ASP.NET MVC 5 is a web framework based on Mode-View-Controller (MVC) architecture. Developers can build dynamic web applications using ASP.NET MVC framework that enables a clean separation of concerns, fast development, and TDD friendly.",
                Published = true,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                User = user,
                Mark = 60,
                Price = 500,
                Images = "https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg",
                PriceCustomer = 500,
                IsSold = true,
                IsPaid = true
            };
            context.Posts.Add(post2);
            Post post3 = new Post
            {
                Title = "Piet Mondrian - Talking about painting in the most natural works",
                PostContent = "Not everyone realizes that in all visual arts, even in the most natural works, natural form and color are, to a certain extent, always changed. In fact, while this may not be directly observable, the tension of the lines, the pattern, as well as the strength of the color is always increasing",
                Published = true,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                User = userStudent2,
                Mark = 0,
                Price = 500,
                Images = "https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg;https://res.cloudinary.com/dev2020/image/upload/v1606842430/om1cmt388cts8dvaonon.jpg",
                PriceCustomer = 500,
                IsSold = true,
                IsPaid = true
            };
            context.Posts.Add(post3);
            Post post4 = new Post
            {
                Title = "Art events for visitors in Hanoi graphic paintings of ASEAN countrie",
                PostContent = "Participating in photo exhibitions of Hanoi, graphic paintings of ASEAN countries, learning Italian and Israeli cultures through movies are suggestions for visitors.The exhibition displays 130 black and white and color photos taken by German photographer Thomas Billhardt in Hanoi in the period 1967 - 1975. Thomas's photos tell viewers about social injustice, poverty and pain. suffering, about war, but also about people's lives and their smiles. The exhibition lasts until November 15 at Manzi Exhibition Space, No. 2 Ngo Hang Bun and Manzi Art Space, 14 Phan Huy Ich. Free admission.",
                Published = true,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                User = user,
                Mark = 0,
                Price = 500,
                Images = "https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg;https://res.cloudinary.com/dev2020/image/upload/v1606842430/om1cmt388cts8dvaonon.jpg",
                PriceCustomer = 500,
                IsSold = true,
                IsPaid = true
            };
            context.Posts.Add(post4);
            Post post5 = new Post
            {
                Title = "Looking back on outstanding Vietnamese art events in 2019 the Department of Fine Arts",
                PostContent = "The year 2019 marks the success of many outstanding art events. In particular, many international exhibitions demonstrate the important position of the host country of Vietnam. Recently, the Department of Fine Arts, Photography and Exhibition (under the Ministry of Culture, Sports and Tourism) has announced typical art events in 2019.",
                Published = true,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                User = userStudent2,
                Mark = 0,
                Price = 500,
                Images = "https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg;https://res.cloudinary.com/dev2020/image/upload/v1606842430/om1cmt388cts8dvaonon.jpg",
                PriceCustomer = 500,
                IsSold = true,
                IsPaid = true
            };
            context.Posts.Add(post5);
            Post post6 = new Post
            {
                Title = "Leonardo da Vinci - Famous Italian painter a painter, sculptor, architect, musician, doctor",
                PostContent = "Leonardo da Vinci was born on 15 April 1452 to 23 April in the current Gregorian calendar – in Anchiano, Italy, died May 2– 11 May in the current Gregorian calendar – 1519 in Amboise, France. He was a painter, sculptor, architect, musician, doctor, engineer, anatomist, creator and natural philosopher.",
                Published = true,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                User = userStudent3,
                Mark = 0,
                Price = 500,
                Images = "https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg;https://res.cloudinary.com/dev2020/image/upload/v1606842430/om1cmt388cts8dvaonon.jpg",
                PriceCustomer = 500,
                IsSold = true,
                IsPaid = true
            };
            context.Posts.Add(post6);
            Post post7 = new Post
            {
                Title = "Pablo Picasso - Famous Spanish painter considered one of the most prominent artists of the 20th century",
                PostContent = "He is better known as Pablo Picasso or Picasso as a Spanish painter and sculptor.Picasso is considered one of the most prominent artists of the 20th century, along with Georges Braque as two founders of the founding school in painting and sculpture. He is one of the top 10 greatest painters in the top 200 largest artists in the world in the 20th century published by The Times, England.",
                Published = true,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                User = userStudent3,
                Mark = 0,
                Price = 500,
                Images = "https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg;https://res.cloudinary.com/dev2020/image/upload/v1606842430/om1cmt388cts8dvaonon.jpg",
                PriceCustomer = 500,
                IsSold = true,
                IsPaid = true
            };
            context.Posts.Add(post7);
            Post post8 = new Post
            {
                Title = "Pablo Picasso - Famous Spanish painter is a Post-Impression impressionable Dutch painter",
                PostContent = "Vincent van Gogh is a Post-Impression impressionable Dutch painter. Many of his paintings are among the most famous, most beloved and also the most expensive in the world. Van Gogh was a pioneer of the school of expressionism and had a great influence on modern art, particularly in fauvism and expressionism in Germany.",
                Published = true,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                User = user,
                Mark = 0,
                Price = 500,
                Images = "https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg;https://res.cloudinary.com/dev2020/image/upload/v1606842430/om1cmt388cts8dvaonon.jpg",
                PriceCustomer = 500,
                IsSold = true,
                IsPaid = true
            };
            context.Posts.Add(post8);

            Competition competition = new Competition()
            {
                Name = "Instutite Fine Art",
                StartDate = new DateTime(2020, 11, 20),
                EndDate = new DateTime(2020, 12, 01),
                Description = "Instutite Fine Art Description",
                CreatedTime = DateTime.Now,
                Logo = "https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg",
                Posts = new List<Post>
                {
                   post1,post2,post3,post4,post5,post6
                }
            };
            context.Competitions.Add(competition);
            Competition competition2 = new Competition()
            {
                Name = "Instutite Fine Art 2",
                StartDate = new DateTime(2020, 12, 02),
                EndDate = new DateTime(2020, 12, 05),
                Description = " Instutite Fine Art 2 Description",
                CreatedTime = DateTime.Now,
                Logo = "https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg",
                Posts = new List<Post>
                {
                   post7,post8
                }
            };
            Competition competition3 = new Competition()
            {
                Name = "Instutite Fine Art 3",
                StartDate = new DateTime(2020, 12, 02),
                EndDate = new DateTime(2020, 12, 07),
                Description = "Instutite Fine Art 3 Description",
                CreatedTime = DateTime.Now,
                Logo = "https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg",
                Posts = new List<Post>
                {
                }
            };

            context.Competitions.Add(competition2);
            Awards awards = new Awards
            {
                Name = "Awards Instutite Fine Art ",
                Ranking = 1,
                Posts = competition.Posts,
                Competition = competition
            };
            context.Awards.Add(awards);


            Comment comment = new Comment
            {
                User = user2,
                Post = post1,
                CommentText = "Good article, beautiful image, try to promote",
                CreateTime = DateTime.Now,
                Mark = 60

            };
            context.Comments.Add(comment);
            Comment comment2 = new Comment
            {
                User = user2,
                Post = post2,
                CommentText = "Good article, beautiful image, try to promote",
                CreateTime = DateTime.Now,
                Mark = 60
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
                user = new User { UserName = name, Email = name, DateOfBirth = new DateTime(1980, 10, 20), Avartar = "https://res.cloudinary.com/dev2020/image/upload/v1605894758/u4wf6ekwp0m4hbosnuja.jpg", UserClass = userClass };
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
