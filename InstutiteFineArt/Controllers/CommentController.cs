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
        private readonly CommentRepository _commentRepository;
        private readonly UserRepository _userRepository;
        private InstutiteFineArtDbContext _context = new InstutiteFineArtDbContext();
        public CommentController()
        {
            _commentRepository = new CommentRepository();
            _userRepository = new UserRepository();
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
            var user = await UserManager.FindAsync(comment.UserName,comment.PassWord);
            if (user != null)
            {
                comment.Id = user.Id;
                comment.CreateTime = DateTime.Now;
                _context.Comments.Add(comment);
                _context.SaveChanges();
            }

            return Json(new { msg = "Bạn không có quyền commnent hoặc sai mật khẩu. Vui lòng thử lại!" }, JsonRequestBehavior.AllowGet);
        }
    }
}