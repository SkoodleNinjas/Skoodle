using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Skoodle.Models;
using Skoodle.ViewModels;
using Microsoft.AspNet.Identity;

namespace Skoodle.Controllers
{
    public class RoomController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Room
        public ActionResult Index()
        {
            return PartialView("Index", db.Rooms.ToList());
        }

        public ActionResult Join(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }

            var loggedUserId = User.Identity.GetUserId();
            ApplicationUser loggedUser = db.Users.FirstOrDefault(x => x.Id == loggedUserId);
            room.Users.Add(loggedUser);
            db.SaveChanges();

            var userNamesInRoom = new List<string>();
            
            foreach(var user in room.Users)
            {
                userNamesInRoom.Add(user.UserName);
            }

            var roomViewModel = new RoomViewModel
            {
                RoomId = room.RoomId,
                Name = room.RoomName,
                CurrentUserNames = userNamesInRoom
            };

            return PartialView("Room", roomViewModel);
        }

        [HttpGet]
        public ActionResult LeaveRoom(int? roomId)
        {
            var loggedUserId = User.Identity.GetUserId();
            ApplicationUser loggedUser = db.Users.FirstOrDefault(x => x.Id == loggedUserId);
            Room room = db.Rooms.Find(roomId);

            room.Users.Remove(loggedUser);
            db.SaveChanges();
            return Index();
        }

        // GET: Room/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return PartialView(room);
        }

        // GET: Room/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Room/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoomId,RoomName,MaxPlayers,MaxRounds")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return PartialView(room);
        }

        // GET: Room/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: Room/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoomId,RoomName,MaxPlayers,MaxRounds")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return PartialView(room);
        }

        // GET: Room/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return PartialView(room);
        }

        // POST: Room/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Room room = db.Rooms.Find(id);
            db.Rooms.Remove(room);
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
