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
        public HomeController()
        {
            _repoUser = new UserRepository();
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
        [HttpGet]
        public ActionResult Index(int? size, int? page, string searchString)
        {
            
            ViewBag.CurrentFilter = searchString;

            var role = RoleManager.Roles.FirstOrDefault(x => x.Name == "Staff");
            var lstUserId = _repoUser.FindAll(x => x.RoleId == role.Id).Select(x => x.UserId).ToList();
            var lstUser = UserManager.Users.Where(x => lstUserId.Any(p => p.Contains(x.Id))).ToList();
            ViewBag.stt = 1;
            ViewBag.currentSize = size; // tạo biến kích thước trang hiện tại
            ViewBag.total = lstUser.Count;
            page = page ?? 1;
            int pageSize = (size ?? 4);
            int pageNumber = (page ?? 1);
            return View(lstUser.ToPagedList(pageNumber, pageSize));
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
