using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using InstutiteOfFineArt.Core.Model;
using InstutiteOfFineArt.DAL.Repository;

namespace InstutiteFineArt.Areas.Admin.Controllers
{
    [Authorize(Roles = "Staff")]
    public class CompetitionsController : Controller
    {
        private readonly CompetitionRepository _competitionRepository;
        public CompetitionsController()
        {
            _competitionRepository = new CompetitionRepository();
        }

        // GET: Admin/Competitions
        public ActionResult Index()
        {
            var lstCompetition = _competitionRepository.GetAll().ToList() ;
            return View(lstCompetition);
        }

        // GET: Admin/Competitions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competition competition = _competitionRepository.Find(x => x.CompetitionId == id);
            if (competition == null)
            {
                return HttpNotFound();
            }
            return View(competition);
        }

        // GET: Admin/Competitions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Competitions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Competition competition, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (competition.StartDate < DateTime.Now)
                {
                    return Json(new { result = true, mess = "Start time should not be less than current date" });
                }
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
                competition.Logo = images;
                competition.CreatedTime=DateTime.Now;
                _competitionRepository.Add(competition);
                return Json(new { result = true, mess = "Create Success", url = "/Admin/Competitions/Index" });
            }

            return Json(new { result = false, mess = "Create not Success", url = "/Admin/Competitions/Index" });
        }

        // GET: Admin/Competitions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competition competition = _competitionRepository.Find(x => x.CompetitionId == id);
            if (competition == null)
            {
                return HttpNotFound();
            }
            return View(competition);
        }

        // POST: Admin/Competitions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(Competition competition, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (competition.StartDate < DateTime.Now)
                {
                    return Json(new { result = true, mess = "Start time should not be less than current date" });
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
                        competition.Logo = uploadResult.SecureUrl.ToString();

                    });
                    task.Wait();
                }
                _competitionRepository.Update(competition);
                return Json(new { result = true, mess = "Edit Success", url = "/Admin/Competitions/Index" });
            }

            return Json(new { result = false, mess = "Edit not Success", url = "/Admin/Competitions/Index" });
        }

        // GET: Admin/Competitions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competition competition = _competitionRepository.Find(x => x.CompetitionId == id);
            if (competition == null)
            {
                return HttpNotFound();
            }
            return View(competition);
        }

        // POST: Admin/Competitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Competition competition = _competitionRepository.Find(x => x.CompetitionId == id);
            if (competition == null)
            {
                return HttpNotFound();
            }
            _competitionRepository.Detete(competition);
            return RedirectToAction("Index");
        }

    }
}
