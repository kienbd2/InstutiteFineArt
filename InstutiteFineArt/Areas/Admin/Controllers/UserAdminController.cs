using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using IdentitySample.Models;
using InstutiteOfFineArt.Core.Model;
using InstutiteOfFineArt.DAL.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InstutiteFineArt.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UsersAdminController : Controller
    {

        private readonly UserRepository _repoUser;
        private readonly UserClassRepository _repoUserClass;
        public UsersAdminController()
        {
            _repoUser = new UserRepository();
            _repoUserClass = new UserClassRepository();
        }

        public UsersAdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
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

        //
        // GET: /Users/
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await UserManager.Users.ToListAsync());
        }
        public ActionResult StaffIndex()
        {
            var role = RoleManager.Roles.FirstOrDefault(x => x.Name == "Staff");
            var lstUserId = _repoUser.FindAll(x => x.RoleId == role.Id).Select(x => x.UserId).ToList();
            var lstUser = UserManager.Users.Where(x => lstUserId.Any(p => p.Contains(x.Id)));
            return View(lstUser);
        }
        public ActionResult StudentIndex()
        {
            var role = RoleManager.Roles.FirstOrDefault(x => x.Name == "Student");
            var lstUserId = _repoUser.FindAll(x => x.RoleId == role.Id).Select(x => x.UserId).ToList();
            var lstUser = UserManager.Users.Where(x => lstUserId.Any(p => p.Contains(x.Id)));
            return View(lstUser);
        }

        //
        // GET: /Users/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);

            ViewBag.RoleNames = await UserManager.GetRolesAsync(user.Id);

            return View(user);
        }

        //
        // GET: /Users/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            //Get the list of Roles
            ViewBag.UserClassId = new SelectList(_repoUserClass.GetAll().ToList(), "ClassId", "Name");
            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
            return View();
        }

        //
        // POST: /Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, HttpPostedFileBase file, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var images = "";
                if (file != null)
                {
                    Task task = Task.Run(async () =>
                    {

                        Account account = new Account("dev2020", "247996535991499", "9jI_5YjJaseBKUrY929sUtt0Fy0");

                        string path = Path.Combine(Server.MapPath("Images"), Path.GetFileName(file.FileName));
                        Cloudinary cloudinary = new Cloudinary(account);
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(path, file.InputStream),
                        };
                        var uploadResult = await cloudinary.UploadAsync(uploadParams);
                        images = uploadResult.SecureUrl.ToString();

                    });
                    task.Wait();
                }

                var user = new User { UserName = userViewModel.Email, Email = userViewModel.Email, DateOfBirth = userViewModel.DateOfBirth, Avartar = images, ClassId = userViewModel.ClassId };
                var adminresult = await UserManager.CreateAsync(user, userViewModel.Password);

                //Add User to the selected Roles 
                if (adminresult.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        var result = await UserManager.AddToRolesAsync(user.Id, selectedRoles);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First());
                            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
                            ViewBag.UserClassId = new SelectList(_repoUserClass.GetAll().ToList(), "ClassId", "Name");
                            return View();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", adminresult.Errors.First());
                    ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
                    ViewBag.UserClassId = new SelectList(_repoUserClass.GetAll().ToList(), "ClassId", "Name");
                    return View();

                }
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
            ViewBag.UserClassId = new SelectList(_repoUserClass.GetAll().ToList(), "ClassId", "Name");
            return View();
        }

        //
        // GET: /Users/Edit/1
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserClassId = new SelectList(_repoUserClass.GetAll().ToList(), "ClassId", "Name");
            var userRoles = await UserManager.GetRolesAsync(user.Id);

            return View(new EditUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                Avatar = user.Avartar,
                DateOfBirth = user.DateOfBirth,
                ClassIdSelected = user.ClassId,
                RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserViewModel editUser, HttpPostedFileBase file, params string[] selectedRole)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                if (file != null)
                {
                    Task task = Task.Run(async () =>
                    {

                        Account account = new Account("dev2020", "247996535991499", "9jI_5YjJaseBKUrY929sUtt0Fy0");

                        string path = Path.Combine(Server.MapPath("Images"), Path.GetFileName(file.FileName));
                        Cloudinary cloudinary = new Cloudinary(account);
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(path, file.InputStream),
                        };
                        var uploadResult = await cloudinary.UploadAsync(uploadParams);
                        user.Avartar = uploadResult.SecureUrl.ToString();

                    });
                    task.Wait();
                }
                user.UserName = editUser.Email;
                user.Email = editUser.Email;
                user.ClassId = editUser.ClassId;
                var userRoles = await UserManager.GetRolesAsync(user.Id);

                selectedRole = selectedRole ?? new string[] { };

                var result = await UserManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }

        //
        // GET: /Users/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await UserManager.FindByIdAsync(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                var result = await UserManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
