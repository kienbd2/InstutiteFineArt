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
        /// <summary>
        /// Get list comment follow post id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JsonResult> List(int? id)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            IList<Comment> listComment = _context.Comments.Where(x => x.Post.PostId == id).ToList();
            foreach (var item in listComment)
            {
                var user = await UserManager.FindByIdAsync(item.Id);
                item.Create = GenerateRelativeString(item.CreateTime);
                item.Avartar = user.Avartar;
                item.UserName = user.UserName;
            }

            return Json(listComment, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Create new comment
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        // GET: Comment
        public async Task<JsonResult> Create(Comment comment)
        {
            if (comment != null)
            {
                var user = await UserManager.FindAsync(comment.UserName, comment.PassWord);
                if (user != null)
                {
                    var userRoles = await UserManager.GetRolesAsync(user.Id);
                    // Staff or Manager can comment
                    if (userRoles.Contains("Staff")||userRoles.Contains("Manager"))
                    {
                        var commentObj = _commentRepository.Find(x => x.Id == user.Id&&x.PostId==comment.PostId);
                        if (commentObj == null)
                        {
                            comment.Id = user.Id;
                            comment.CreateTime = DateTime.Now;
                            _context.Comments.Add(comment);
                            _context.SaveChanges();
                            var post = _repoPost.Find(x => x.PostId == comment.PostId);
                            if (post != null)
                            {
                                //update mark - the decisive factor to the ranking
                                if (post.Mark == 0)
                                {
                                    post.Mark = comment.Mark;
                                }
                                else
                                {
                                    post.Mark = (post.Mark + comment.Mark) / 2;
                                }
                                _repoPost.Update(post);
                            }
                            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                        }

                        return Json(new { result = false, msg = "You have done comment.Please try again!" }, JsonRequestBehavior.AllowGet);
                    }

                    return Json(new { result = false, msg = "You do not have the right to comment" }, JsonRequestBehavior.AllowGet);

                }
                return Json(new { result = false, msg = "Incorrect username or password.Please try again!" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = false, msg = "You have not entered comment information. Please try again!" }, JsonRequestBehavior.AllowGet);
        }
        //get date
        private string GenerateRelativeString(DateTime dateTime)
        {
            TimeSpan span = (DateTime.Now - dateTime);

            // Normalize time span
            bool future = false;
            if (span.TotalSeconds < 0)
            {
                // In the future
                span = -span;
                future = true;
            }

            // Test for Now
            double totalSeconds = span.TotalSeconds;
            if (totalSeconds < 0.9)
            {
                return "Now";
            }

            // Date/time near current date/time
            string format = (future) ? "in {0} {1}" : "{0} {1} ago";
            if (totalSeconds < 55)
            {
                // Seconds
                int seconds = Math.Max(1, span.Seconds);
                return String.Format(format, seconds,
                    (seconds == 1) ? "second" : "seconds");
            }

            if (totalSeconds < (55 * 60))
            {
                // Minutes
                int minutes = Math.Max(1, span.Minutes);
                return String.Format(format, minutes,
                    (minutes == 1) ? "minute" : "minutes");
            }
            if (totalSeconds < (24 * 60 * 60))
            {
                // Hours
                int hours = Math.Max(1, span.Hours);
                return String.Format(format, hours,
                    (hours == 1) ? "hour" : "hours");
            }

            // Format both date and time
            if (totalSeconds < (48 * 60 * 60))
            {
                // 1 Day
                format = (future) ? "tomorrow" : "yesterday";
            }
            else if (totalSeconds < (3 * 24 * 60 * 60))
            {
                // 2 Days
                format = String.Format(format, 2, "days");
            }
            else
            {
                // Absolute date
                if (dateTime.Year == DateTime.Now.Year)
                    format = dateTime.ToString(@"MMM d");
                else
                    format = dateTime.ToString(@"MMM d, yyyy");
            }

            // Add time
            return String.Format("{0} at {1:h:mm tt}", format, dateTime);
        }
    }
}