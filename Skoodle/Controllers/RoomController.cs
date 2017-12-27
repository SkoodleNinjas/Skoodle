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
using System.IO;
using Skoodle.BusinessLogic;

namespace Skoodle.Controllers
{
    public class RoomController : Controller
    {

        UserLogic userLogic = new UserLogic();
        RoomLogic roomLogic = new RoomLogic();

        // GET: Room
        public ActionResult Index()
        {
            return PartialView("Index", roomLogic.GetRooms());
        }

        public ActionResult Join(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = roomLogic.GetRoomById(id);
            if (room == null)
            {
                return HttpNotFound();
            }

            var loggedUserId = User.Identity.GetUserId();
            var roomViewModel = roomLogic.JoinUserToRoomById(loggedUserId, id);

            return PartialView("Room", roomViewModel);
        }

        [HttpGet]
        public ActionResult LeaveRoom(int roomId)
        {
            var loggedUserId = User.Identity.GetUserId();
            var loggedUser = userLogic.GetUserById(loggedUserId);
            roomLogic.LeaveRoomById(loggedUserId, roomId);
            return Index();
        }

        // GET: Room/Details/5
        public ActionResult Details(int id)
        {
            Room room = roomLogic.GetRoomById(id);
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
                roomLogic.CreateRoom(room);
                return Index();
            }

            return PartialView(room);
        }

        // GET: Room/Edit/5
        public ActionResult Edit(int id)
        {
            Room room = roomLogic.GetRoomById(id);
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
                roomLogic.EditRoom(room);
                return RedirectToAction("Index");
            }
            return PartialView(room);
        }

        // GET: Room/Delete/5
        public ActionResult Delete(int id)
        {
            Room room = roomLogic.GetRoomById(id);
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
            roomLogic.DeleteRoomById(id);
            return RedirectToAction("Index");
        }

        // Change the image name to be associated with Room, Topic and UserName
        [HttpPost]
        public void SendDrawing(string image)
        {
            var src = DateTime.Now;
            var timer = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, src.Second);
            string fileName = timer.ToString() + ".png";
            string fileNameWithPath = Path.Combine(Server.MapPath("~/UserImages"), Path.GetFileNameWithoutExtension(fileName));
            using (FileStream fs = new FileStream(fileNameWithPath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(image);
                    bw.Write(data);
                    bw.Close();
                }
                fs.Close();
            }
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

    }
}
