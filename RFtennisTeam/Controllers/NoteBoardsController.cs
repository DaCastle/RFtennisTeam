using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RFtennisTeam.Models;

namespace RFtennisTeam.Controllers
{
    [Authorize]
    public class NoteBoardsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NoteBoards
        public ActionResult Index()
        {
            return View(db.NoteBoard.ToList());
        }

        // GET: NoteBoards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoteBoard noteBoard = db.NoteBoard.Find(id);
            if (noteBoard == null)
            {
                return HttpNotFound();
            }
            return View(noteBoard);
        }

        // GET: NoteBoards/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NoteBoards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,note,userName,date")] NoteBoard noteBoard)
        {
            if (ModelState.IsValid)
            {
                noteBoard.userName = HttpContext.User.Identity.Name;
                noteBoard.date = DateTime.Now;

                if (noteBoard.note == null)
                {
                    noteBoard.note = noteBoard.userName + ", actually enter a note. . .";
                }

                db.NoteBoard.Add(noteBoard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(noteBoard);
        }

        // GET: NoteBoards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoteBoard noteBoard = db.NoteBoard.Find(id);
            if (noteBoard == null)
            {
                return HttpNotFound();
            }
            return View(noteBoard);
        }

        // POST: NoteBoards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,note,userName,date")] NoteBoard noteBoard)
        {
            if (ModelState.IsValid)
            {
                noteBoard.date = DateTime.Now;
                noteBoard.userName = HttpContext.User.Identity.Name;
                db.Entry(noteBoard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(noteBoard);
        }

        // GET: NoteBoards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoteBoard noteBoard = db.NoteBoard.Find(id);
            if (noteBoard == null)
            {
                return HttpNotFound();
            }
            return View(noteBoard);
        }

        // POST: NoteBoards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NoteBoard noteBoard = db.NoteBoard.Find(id);
            db.NoteBoard.Remove(noteBoard);
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
