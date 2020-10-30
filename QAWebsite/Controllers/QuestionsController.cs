using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PagedList;
using QAWebsite.Models;

namespace QAWebsite.Controllers
{
    
    public class QuestionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Questions
        public ActionResult Index(int? page,bool? sortByNumber)
        {
            ViewBag.sortByNumber = sortByNumber;
            // List of question
            var questions = db.Questions.Include(q => q.User);

            // Which number it is.
            int pageNumber = page ?? 1;

            // Each page show 10 questions 
            int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);

            if (sortByNumber == true)
            {
                // Order by Number Of Answers.
                ViewBag.Sorted = "Sorted By Number Of Answers.";
                questions = questions.OrderByDescending(q => q.Answers.Count);
            }
            else
            {
                // Order by date.
                ViewBag.Sorted = "Sorted By Date";
                questions = questions.OrderByDescending(q => q.Date);
            }
            

            // Use ToPagedList.
            IPagedList<Question> pagedList = questions.ToPagedList(pageNumber, pageSize);

            return View(pagedList);
        }
        public ActionResult QuestionsInATag(int? page, bool? sortByNumber, int tagId)
        {
            ViewBag.sortByNumber = sortByNumber;
            ViewBag.tagId = tagId;
            ViewBag.tagName = db.Tags.Find(tagId).Name;
            // List of question
            var questions = db.Questions.Include(q => q.User);
            questions = questions.Where(q => q.QuestionTags.Any(qt => qt.TagId == tagId));

            // Which number it is.
            int pageNumber = page ?? 1;

            // Each page show 10 questions 
            int pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);

            if (sortByNumber == true)
            {
                // Order by Number Of Answers.
                ViewBag.Sorted = "Sorted By Number Of Answers.";
                questions = questions.OrderByDescending(q => q.Answers.Count);
            }
            else
            {
                // Order by date.
                ViewBag.Sorted = "Sorted By Date";
                questions = questions.OrderBy(q => q.Date);
            }

            // Use ToPagedList.
            IPagedList<Question> pagedList = questions.ToPagedList(pageNumber, pageSize);

            return View(pagedList);
        }

        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.AcceptedAnswer = question.Answers.Any(a => a.Accepted == true);
            return View(question);
        }
        #region
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Details(int id, string answer_Content)
        //{
        //    Question question = db.Questions.Find(id);
        //    if (question == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        Answer answer = new Answer();
        //        answer.QuestionId = id;
        //        answer.Content = answer_Content;
        //        answer.UserId = User.Identity.GetUserId();
        //        db.Answers.Add(answer);
        //        db.SaveChanges();
        //        return RedirectToAction("Details",new { id });
        //    }

        //    return View(question);
        //}
        #endregion
        [Authorize]
        public ActionResult AddComment()
        {
            return View();
        }

        // POST: QuestionComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(int QuestionId, string Comment)
        {
            QuestionComment questionComment = new QuestionComment();
            questionComment.QuestionId = QuestionId;
            questionComment.Comment = Comment;
            questionComment.UserId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.QuestionComments.Add(questionComment);
                db.SaveChanges();
                return RedirectToAction("Details",new { id = QuestionId });
            }
            return View(questionComment);
        }
        [Authorize]
        public ActionResult AnswerQuestion(int? QuestionId)
        {
            if (QuestionId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(QuestionId);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionTitle = question.Title;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AnswerQuestion(int QuestionId, string Content)
        {
            Question question = db.Questions.Find(QuestionId);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionTitle = question.Title;

            Answer answer = new Answer();
            answer.QuestionId = QuestionId;
            answer.Content = Content;
            answer.UserId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Answers.Add(answer);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = QuestionId });
            }
            return View(answer);
        }
        [Authorize]
        public ActionResult AddAnswerComment()
        {
            return View();
        }

        // POST: QuestionComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAnswerComment(int QuestionId,int AnswerId, string Comment)
        {
            AnswerComment answerComment = new AnswerComment();
            answerComment.AnswerId = AnswerId;
            answerComment.Comment = Comment;
            answerComment.UserId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.AnswerComments.Add(answerComment);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = QuestionId });
            }
            return View(answerComment);
        }
        [Authorize]
        public ActionResult VoteUp(int id)
        {
            var question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            if (User.Identity.GetUserId() != question.UserId)
            {
                question.QuestionVote++;
                db.SaveChanges();
            }
            return RedirectToAction("Details", new { id });
        }
        [Authorize]
        public ActionResult VoteDown(int id)
        {
            var question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            if (User.Identity.GetUserId() != question.UserId)
            {
                question.QuestionVote--;
                db.SaveChanges();
            }
            return RedirectToAction("Details", new { id });
        }
        [Authorize]
        public ActionResult AnswerVoteUp(int id,int answerId)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            Answer answer = db.Answers.Find(answerId);
            if (answer == null)
            {
                return HttpNotFound();
            }
            if (User.Identity.GetUserId() != answer.UserId)
            {
                answer.AnswerVote++;
                db.SaveChanges();
            }
            return RedirectToAction("Details", new { id });
        }
        [Authorize]
        public ActionResult AnswerVoteDown(int id,int answerId)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            Answer answer = db.Answers.Find(answerId);
            if (answer == null)
            {
                return HttpNotFound();
            }
            if (User.Identity.GetUserId() != answer.UserId)
            {
                answer.AnswerVote--;
                db.SaveChanges();
            }
            return RedirectToAction("Details", new { id });
        }
        [Authorize]
        public ActionResult MarkAnswer(int id, int answerId)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            Answer answer = db.Answers.Find(answerId);
            if (answer == null)
            {
                return HttpNotFound();
            }
            if (User.Identity.GetUserId() == question.UserId)
            {
                if(question.Answers.All(a =>a.Accepted == false))
                {
                    answer.Accepted = true;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Details", new { id });
        }
        // GET: Questions/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.TagId = new MultiSelectList(db.Tags, "Id","Name");
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "Id,Title,Content,Date,UserId")] Question question
        public ActionResult Create(string title, string content, int[] tagId)
        {
            Question question = new Question() { Title = title, Content = content};
            question.UserId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                if(tagId != null)
                {
                    foreach (var item in tagId)
                    {
                        db.QuestionTags.Add(new QuestionTag() { QuestionId = question.Id, TagId = item });
                    }
                }

                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TagId = new MultiSelectList(db.Tags, "Id");
            
            return View(question);
        }

        // GET: Questions/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", question.UserId);
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string Title, string Content )
        {
            Question question = db.Questions.Find(id);
            question.Title = Title;
            question.Content = Content;
            question.Date = System.DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", question.UserId);
            return View(question);
        }

        // GET: Questions/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
