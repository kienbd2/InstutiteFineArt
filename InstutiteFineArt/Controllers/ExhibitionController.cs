using InstutiteOfFineArt.DAL.Repository;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InstutiteFineArt.Controllers
{
    public class ExhibitionController : Controller
    {
        private readonly PostRepository _postRepository;
        public ExhibitionController()
        {
            _postRepository = new PostRepository();
        }
        // GET: Exhibition
        public ActionResult Index(int? size, int? page, string searchString)
        {
            var lstPost = _postRepository.FindAll(x => x.Published == true).OrderByDescending(x => x.UpdatedTime);
            if (!String.IsNullOrEmpty(searchString))
            {
                 lstPost.Where(s => s.Title.Contains(searchString));
            }
            ViewBag.stt = 1;
            ViewBag.currentSize = size; // tạo biến kích thước trang hiện tại
            ViewBag.total = lstPost.ToList().Count;
            page = page ?? 1;
            int pageSize = (size ?? 4);
            int pageNumber = (page ?? 1);
            return View(lstPost.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Details()
        {
            return View();
        }
    }
}