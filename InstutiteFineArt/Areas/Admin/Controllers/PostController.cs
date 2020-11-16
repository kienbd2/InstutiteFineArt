using InstutiteOfFineArt.DAL.Repository;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InstutiteFineArt.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        private readonly PostRepository _postRepository;
        public PostController()
        {
            
            _postRepository = new PostRepository();
        }
        // GET: Admin/Post
        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();
            var a = _postRepository.FindAll(filter:x=>x.User.Id==userID).ToList();
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
    }
}