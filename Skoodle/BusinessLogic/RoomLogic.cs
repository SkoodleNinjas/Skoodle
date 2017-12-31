using Skoodle.Models;
using Skoodle.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace Skoodle.BusinessLogic
{
    public class RoomLogic
    {
        private ApplicationDbContext db;

        public RoomLogic()
        {
            db = new ApplicationDbContext();
        }

        public ICollection<Room> GetRooms()
        {
            return db.Rooms.ToList();
        }

        public RoomViewModel JoinUserToRoomById(string userId, int? roomId)
        {
            Room room = db.Rooms.Find(roomId);
            ApplicationUser loggedUser = db.Users.FirstOrDefault(x => x.Id == userId);
            room.Users.Add(loggedUser);
            db.SaveChanges();

            string gameId = "";

            if(db.Games.Any(gm => gm.Room.RoomId == roomId && gm.IsGameAlive == true))
            {
                gameId = db.Games.First(gm => gm.Room.RoomId == roomId && gm.IsGameAlive == true).GameId.ToString();
            }

            var userNamesInRoom = new List<string>();

            foreach (var user in room.Users)
            {
                userNamesInRoom.Add(user.UserName);
            }

            return new RoomViewModel
            {
                RoomId = room.RoomId,
                Name = room.RoomName,
                CurrentUserNames = userNamesInRoom,
                GameId = gameId
            };
        }

        public void LeaveRoomById(string userId, int roomId)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == userId);
            Room room = db.Rooms.Find(roomId);

            room.Users.Remove(user);
            db.SaveChanges();
        }

        public Room GetRoomById(int? id)
        {
            return db.Rooms.Find(id);
        }

        public void CreateRoom(Room room)
        {
            db.Rooms.Add(room);
            db.SaveChanges();
        }

        public void EditRoom(Room room)
        {
            db.Entry(room).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteRoomById(int id)
        {
            Room room = db.Rooms.Find(id);
            db.Rooms.Remove(room);
            db.SaveChanges();
        }
    }
}