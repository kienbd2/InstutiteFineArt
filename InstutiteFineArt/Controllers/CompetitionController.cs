using InstutiteOfFineArt.DAL.Repository;
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
        public CompetitionController()
        {
            _competitionRepository = new CompetitionRepository();
        }
        // GET: Competition
        public ActionResult Index()
        {
            var lst = _competitionRepository.GetAll();
            return View(); 
        }
        public ActionResult Details()
        {
            return View();
        }
    }
}