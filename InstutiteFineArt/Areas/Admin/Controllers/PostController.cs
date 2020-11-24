using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using InstutiteOfFineArt.Core.Model;
using InstutiteOfFineArt.DAL.Repository;
using Microsoft.AspNet.Identity;
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
        // GET: Admin/Post
        [Authorize(Roles = "Student")]
        public ActionResult Index()
        {
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
            post.Price = 1000;
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
            ViewBag.Images = "";
            foreach (var images in post.Images.Split(';'))
            {
                ViewBag.Images += "\"" + images + "\"" + ",";
            }
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }
    }
}