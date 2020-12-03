using IdentitySample.Models;
using InstutiteOfFineArt.DAL.Repository;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentitySample.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserRepository _repoUser;
        private readonly PostRepository _repoPost;
        private readonly CompetitionRepository _repoCompetition;
        public HomeController()
        {
            _repoUser = new UserRepository();
            _repoPost = new PostRepository();
            _repoCompetition = new CompetitionRepository();
        }
        public HomeController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
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
        /// Home page 
        /// Get all staff information
        /// Count staff, count student, count competitions, count posts
        /// </summary>
        /// <param name="size"></param>
        /// <param name="page"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(int? size, int? page, string searchString)
        {

            ViewBag.CurrentFilter = searchString;

            var roleStaff = RoleManager.Roles.FirstOrDefault(x => x.Name == "Staff");
            var lstUserId = _repoUser.FindAll(x => x.RoleId == roleStaff.Id).Select(x => x.UserId).ToList();
            var lstUser = UserManager.Users.Where(x => lstUserId.Any(p => p.Contains(x.Id))).ToList();
            var roleStudent = RoleManager.Roles.FirstOrDefault(x => x.Name == "Student");
            //total staff
            ViewBag.CountStaff = lstUserId.Count();
            //total student
            ViewBag.CountStudent = _repoUser.FindAll(x => x.RoleId == roleStudent.Id).Count();
            //total competition
            ViewBag.CountCompetition = _repoCompetition.Count();
            //total post
            ViewBag.CountPost = _repoPost.Count();
            ViewBag.stt = 1;
            ViewBag.currentSize = size; // tạo biến kích thước trang hiện tại
            ViewBag.total = lstUser.Count;
            page = page ?? 1;
            int pageSize = (size ?? 8);
            int pageNumber = (page ?? 1);
            return View(lstUser.ToPagedList(pageNumber, pageSize));
        }
        /// <summary>
        /// Top 5 Post With The Highest Score
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult MostViewedPost()
        {
            ViewBag.PartialName = "Most viewed posts!";
            var mostMarkedPosts = _repoPost.FindAll(x => x.Published == true).OrderByDescending(x => x.Mark).Take(5);
            //// Map from Post model to PostSummary view model

            return PartialView("_ListPosts", mostMarkedPosts);
        }
        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
