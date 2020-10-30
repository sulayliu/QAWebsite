using PagedList;
using QAWebsite.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QAWebsite.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        #region
        //public ActionResult ShowQuestions(int? page)
        //{
        //    // List of question
        //    var questions = from q in db.Questions select q;

        //    // Which number it is.
        //    int pageNumber = page ?? 1;

        //    // Each page show 10 questions 
        //    int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);

        //    // Order by date.
        //    questions = questions.OrderBy(x => x.Date);

        //    // Use ToPagedList.
        //    IPagedList<Question> pagedList = questions.ToPagedList(pageNumber, pageSize);

        //    return View(pagedList);
        //}
        #endregion
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}