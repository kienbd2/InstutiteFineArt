using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using IdentitySample.Models;
using InstutiteOfFineArt.Core.Model;
using InstutiteOfFineArt.DAL.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InstutiteFineArt.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        private readonly PostRepository _postRepository;
        private readonly CompetitionRepository _competitionRepository;
        private readonly UserRepository _userRepository;
        public PostController()
        {

            _postRepository = new PostRepository();
            _competitionRepository = new CompetitionRepository();
            _userRepository = new UserRepository();
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        [Authorize(Roles = "Staff,Student")]
        // GET: Admin/Post
        public ActionResult Index()
        {
            if (User.IsInRole("Staff"))
            {
                var role = RoleManager.Roles.FirstOrDefault(x => x.Name == "Student");
                var lstUserId = _userRepository.FindAll(x => x.RoleId == role.Id).Select(x => x.UserId).ToList();
                var lstUser = UserManager.Users.Where(x => lstUserId.Any(p => p.Contains(x.Id))).Select(x=>x.Id);
                //var listPost = _postRepository.FindBy(x => lstUser.Any(p => p.Contains(x.User.Id))).ToList();
                var listPost = new List<Post>();
                foreach (var item in lstUser)
                {
                    listPost.AddRange(_postRepository.FindAll(x => x.User.Id == item));
                }
                return View(listPost);
            }
            var userID = User.Identity.GetUserId();
            var lstPost = _postRepository.FindAll(filter: x => x.User.Id == userID);
            return View(lstPost);
        }
        public ActionResult Create()
        {
            ViewBag.Competitions = _competitionRepository.FindAll(x => x.EndDate >= DateTime.Now).ToList();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Post post, int competition)
        {
            var lstImage = new List<string>();
            HttpFileCollectionBase files = Request.Files;
            if (files.Count > 5)
            {
                return Json(new { result = false, mess = "Do not upload larger than 5 images" });
            }

            Task task = Task.Run(async () =>
            {
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    Account account = new Account("dev2020", "247996535991499", "9jI_5YjJaseBKUrY929sUtt0Fy0");

                    string path = Path.Combine(Server.MapPath("Images"), Path.GetFileName(file.FileName));
                    Cloudinary cloudinary = new Cloudinary(account);
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(path, file.InputStream),
                    };
                    var uploadResult = await cloudinary.UploadAsync(uploadParams);
                    lstImage.Add(uploadResult.SecureUrl.ToString());
                }
            });
            task.Wait();
            var userID = User.Identity.GetUserId();
            post.Id = userID;
            post.CompetitionId = competition;
            //var competitionObj = _competitionRepository.Find(x => x.CompetitionId == competition);
            //if (competitionObj != null)
            //{
            //    post.Competition = competitionObj;
            //}
            post.Images = string.Join(";", lstImage);
            post.CreatedTime = DateTime.Now;
            post.UpdatedTime = DateTime.Now;
            post.Mark = 3;
            post.IsPaid = true;
            post.PriceCustomer = 800;
            post.IsSold = true;
            post.Published = true;

            var result = _postRepository.Add(post);
            if (result > 0)
            {
                return Json(new { result = true, mess = "Create Success", url = "/Admin/Post/Index" });
            }

            return Json(new { result = false, mess = "Create not Success", url = "/Admin/Post/Index" });

        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Competitions = _competitionRepository.FindAll(x => x.EndDate >= DateTime.Now).ToList();
            Post post = _postRepository.Find(x => x.PostId == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Post post, int competition)
        {
            var lstImage = new List<string>();
            HttpFileCollectionBase files = Request.Files;
            if (files.Count > 1)
            {
                if (files.Count > 5)
                {
                    return Json(new { result = false, mess = "Do not upload larger than 5 images" });
                }

                Task task = Task.Run(async () =>
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        Account account = new Account("dev2020", "247996535991499", "9jI_5YjJaseBKUrY929sUtt0Fy0");

                        string path = Path.Combine(Server.MapPath("Images"), Path.GetFileName(file.FileName));
                        Cloudinary cloudinary = new Cloudinary(account);
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(path, file.InputStream),
                        };
                        var uploadResult = await cloudinary.UploadAsync(uploadParams);
                        lstImage.Add(uploadResult.SecureUrl.ToString());
                    }
                });
                task.Wait();
            }


            var userID = User.Identity.GetUserId();
            post.Id = userID;
            post.CompetitionId = competition;
            //var competitionObj = _competitionRepository.Find(x => x.CompetitionId == competition);
            //if (competitionObj != null)
            //{
            //    post.Competition = competitionObj;
            //}
            post.Images = post.Images + ";" + string.Join(";", lstImage);
            post.UpdatedTime = DateTime.Now;
            post.Mark = 3;
            post.IsPaid = true;
            post.PriceCustomer = 800;
            post.IsSold = true;
            post.Published = true;

            var result = _postRepository.Update(post);
            if (result > 0)
            {
                return Json(new { result = true, mess = "Edit Post Success", url = "/Admin/Post/Index" });
            }

            return Json(new { result = false, mess = "Edit Post not Success", url = "/Admin/Post/Index" });
        }
        public JsonResult ChangeStatus(int id, bool active)
        {
            var post = _postRepository.Find(x => x.PostId == id);
            if (post != null)
            {
                post.Published = !active;
                _postRepository.Update(post);
                if (active)
                {
                    return Json(new { result = true, active = false, mess = "Removed articles from successful exhibitions" });
                }
                return Json(new { result = true, active = true, mess = "Moved the article to the exhibition successfully" });
            }
            return Json(new { result = false, mess = "Did not find the article" });
        }
    }
}