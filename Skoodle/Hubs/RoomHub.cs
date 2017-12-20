using Microsoft.AspNet.SignalR;
using Skoodle.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skoodle.WebSockets
{    
    public class RoomHub: Hub
    {
        /// <summary>
        /// User is assigned to his room and his roomates are acknowledged
        /// </summary>
        /// <param name="roomName">the name of the room</param>
        /// <returns></returns>
        public async Task JoinRoom(string roomName)
        {
            await Groups.Add(Context.ConnectionId, roomName);
            var src = DateTime.Now;
            var timer = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0);

            Clients.Group(roomName).addChatMessage("System", Context.User.Identity.Name + " joined.", timer.ToString());
            Clients.OthersInGroup(roomName).addUser(Context.User.Identity.Name);
        }


        /// <summary>
        /// Users sends his message to this method adn it broadcasts it to the other members of the room
        /// </summary>
        /// <param name="roomName">User's room name</param>
        /// <param name="message">Users's message</param>
        /// <returns></returns>
        public async Task SendMessage(string roomName, string message)
        {
            var src = DateTime.Now;
            var timer = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, 0);
            Clients.Group(roomName).addChatMessage(Context.User.Identity.Name, message, timer.ToString());
        }

        /// <summary>
        /// When user leaves his room he calls this method
        /// and the user is removed from the socket room
        /// </summary>
        /// <param name="roomName">the name of the room the user leaves</param>
        /// <returns></returns>
        public Task LeaveRoom(string roomName)
        {
            Clients.Group(roomName).userLeft(Context.User.Identity.Name);
            return Groups.Remove(Context.ConnectionId, roomName);
        }
    }
}