using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using QAWebsite.Models;

namespace QAWebsite.Controllers
{
    public class QuestionCommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: QuestionComments
        public ActionResult Index()
        {
            var questionComments = db.QuestionComments.Include(q => q.Question).Include(q => q.User);
            return View(questionComments.ToList());
        }

        // GET: QuestionComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionComment questionComment = db.QuestionComments.Find(id);
            if (questionComment == null)
            {
                return HttpNotFound();
            }
            return View(questionComment);
        }

        // GET: QuestionComments/Create
        public ActionResult Create()
        {
            //ViewBag.QuestionId = new SelectList(db.Questions, "Id", "Title");
            //ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: QuestionComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int QuestionId, string Comment)
        {
            QuestionComment questionComment = new QuestionComment();
            questionComment.QuestionId = QuestionId;
            questionComment.Comment = Comment;
            questionComment.UserId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.QuestionComments.Add(questionComment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.QuestionId = new SelectList(db.Questions, "Id", "Title", questionComment.QuestionId);
            //ViewBag.UserId = new SelectList(db.Users, "Id", "Email", questionComment.UserId);
            return View(questionComment);
        }

        // GET: QuestionComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionComment questionComment = db.QuestionComments.Find(id);
            if (questionComment == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionId = new SelectList(db.Questions, "Id", "Title", questionComment.QuestionId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", questionComment.UserId);
            return View(questionComment);
        }

        // POST: QuestionComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Comment,Date,QuestionId,UserId")] QuestionComment questionComment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionId = new SelectList(db.Questions, "Id", "Title", questionComment.QuestionId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", questionComment.UserId);
            return View(questionComment);
        }

        // GET: QuestionComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionComment questionComment = db.QuestionComments.Find(id);
            if (questionComment == null)
            {
                return HttpNotFound();
            }
            return View(questionComment);
        }

        // POST: QuestionComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionComment questionComment = db.QuestionComments.Find(id);
            db.QuestionComments.Remove(questionComment);
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
