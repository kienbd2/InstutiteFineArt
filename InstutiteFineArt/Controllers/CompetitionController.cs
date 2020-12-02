using InstutiteOfFineArt.Core.Model;
using InstutiteOfFineArt.DAL.Repository;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InstutiteFineArt.Controllers
{
    public class CompetitionController : Controller
    {
        private readonly CompetitionRepository _competitionRepository;
        private readonly PostRepository _postRepository;
        public CompetitionController()
        {
            _competitionRepository = new CompetitionRepository();
            _postRepository = new PostRepository();
        }
        // GET: Competition
        public ActionResult Index(int? size, int? page, string searchString)
        {
            var lst = _competitionRepository.GetAll();
            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.CurrentFilter = searchString;
                lst = lst.Where(s => s.Name.Contains(searchString));
            }
            ViewBag.stt = 1;
            ViewBag.currentSize = size; // tạo biến kích thước trang hiện tại
            ViewBag.total = lst.ToList().Count;
            page = page ?? 1;
            int pageSize = (size ?? 2);
            int pageNumber = (page ?? 1);
            return View(lst.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Details(int? competitionId, int? size, int? page, string searchString)
        {
            //if (competitionId == null)
            //{
            //    return View();
            //}
            var compettion = _competitionRepository.Find(x => x.CompetitionId == competitionId);
            if (compettion == null)
            {
                return View();
            }
            ViewBag.CompetitionName = compettion.Name;
            var lstPost = _postRepository.FindAll(x => x.CompetitionId == competitionId).OrderByDescending(x=>x.Mark).OrderBy(x=>x.UpdatedTime);
            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.CurrentFilter = searchString;
                lstPost.Where(s => s.User.UserClass.Name.Contains(searchString));
            }
            ViewBag.competitionId = competitionId;
            ViewBag.stt = 1;
            ViewBag.currentSize = size; // tạo biến kích thước trang hiện tại
            ViewBag.total = lstPost.ToList().Count;
            page = page ?? 1;
            int pageSize = (size ?? 4);
            ViewBag.pageSize = pageSize;
            int pageNumber = (page ?? 1);
            ViewBag.Page = page;
            ViewBag.competitionId = competitionId;
            
            return View(lstPost.ToPagedList(pageNumber, pageSize));
        }
    }
}