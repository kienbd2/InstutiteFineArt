using IdentitySample.Models;
using InstutiteOfFineArt.Core.Model;
using InstutiteOfFineArt.DAL.Repository;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InstutiteFineArt.Controllers
{
    public class CommentController : Controller
    {
        private readonly PostRepository _repoPost;
        private readonly CommentRepository _commentRepository;
        private readonly UserRepository _userRepository;
        private InstutiteFineArtDbContext _context = new InstutiteFineArtDbContext();
        public CommentController()
        {
            _commentRepository = new CommentRepository();
            _userRepository = new UserRepository();
            _repoPost = new PostRepository();
        }
        public CommentController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
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
        public async Task<JsonResult> List(int? id)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            IList<Comment> listComment = _context.Comments.Where(x => x.Post.PostId == id).ToList();
            foreach (var item in listComment)
            {
                var user = await UserManager.FindByIdAsync(item.Id);
                item.Avartar = user.Avartar;
                item.UserName = user.UserName;
            }

            return Json(listComment, JsonRequestBehavior.AllowGet);
        }
        // GET: Comment
        public async Task<JsonResult> Create(Comment comment)
        {
            if (comment != null)
            {
                var user = await UserManager.FindAsync(comment.UserName, comment.PassWord);
                if (user != null)
                {
                    comment.Id = user.Id;
                    comment.CreateTime = DateTime.Now;
                    _context.Comments.Add(comment);
                    _context.SaveChanges();
                    var post = _repoPost.Find(x => x.PostId == comment.PostId);
                    if (post != null)
                    {
                        post.Mark = (post.Mark + comment.Mark) / 2;
                        _repoPost.Update(post);
                    }
                }
            }

            return Json(new { msg = "You have not completed the information. Please try again!" }, JsonRequestBehavior.AllowGet);
        }
    }
}